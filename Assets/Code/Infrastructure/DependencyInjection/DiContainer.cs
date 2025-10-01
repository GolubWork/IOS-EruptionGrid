using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace Code.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Простой DI-контейнер с поддержкой:
    /// Bind/To/ToSelf, FromInstance, FromFactory, AsSingle/AsTransient,
    /// Resolve, [Inject] по полям/свойствам/методам/конструктору, Dispose lifecycle.
    /// </summary>
    public sealed class DiContainer : IDisposable
    {
        private readonly Dictionary<Type, Binding> _bindings = new Dictionary<Type, Binding>();
        private readonly Dictionary<Type, object> _singletons = new Dictionary<Type, object>();

        // порядок владимых контейнером singleton'ов — для обратного Dispose()
        private readonly List<object> _ownedSingletonsInOrder = new List<object>();

        private bool _disposed;

        // ------------------ Fluent API ------------------
        public Binder<TService> Bind<TService>() { return new Binder<TService>(this); }

        public sealed class Binder<TService>
        {
            private readonly DiContainer _c;
            private Type _impl = typeof(TService);
            private object _instance; // FromInstance
            private Func<DiContainer, object> _factory; // FromFactory
            private Lifetime _lifetime = Lifetime.Transient;

            internal Binder(DiContainer c) { _c = c; }

            public Binder<TService> To<TImpl>() { _impl = typeof(TImpl); return this; }
            public Binder<TService> ToSelf()     { _impl = typeof(TService); return this; }
            public Binder<TService> FromInstance(TService instance) { _instance = instance; return this; }
            public Binder<TService> FromFactory(Func<DiContainer, TService> factory) { _factory = (DiContainer cc) => factory(cc); return this; }

            public void AsSingle()   { _lifetime = Lifetime.Singleton;  _c.Register(typeof(TService), _impl, _lifetime, _instance, _factory); }
            public void AsTransient(){ _lifetime = Lifetime.Transient; _c.Register(typeof(TService), _impl, _lifetime, _instance, _factory); }
        }

        public sealed class MultiBinder
        {
            private readonly DiContainer _c;
            private readonly Type[] _services;
            private readonly Type _impl;
            private object _instance;
            private Func<DiContainer, object> _factory;

            internal MultiBinder(DiContainer c, IEnumerable<Type> services, Type impl)
            {
                _c = c;
                _services = services.Distinct().ToArray();
                _impl = impl;
            }

            public MultiBinder FromInstance(object instance) { _instance = instance; return this; }
            public MultiBinder FromFactory(Func<DiContainer, object> factory) { _factory = factory; return this; }

            public void AsSingle()
            {
                for (int i = 0; i < _services.Length; i++)
                    _c.Register(_services[i], _impl, Lifetime.Singleton, _instance, _factory);
            }

            public void AsTransient()
            {
                for (int i = 0; i < _services.Length; i++)
                    _c.Register(_services[i], _impl, Lifetime.Transient, _instance, _factory);
            }
        }

        public MultiBinder BindInterfacesAndSelfTo<TImpl>()
        {
            Type impl = typeof(TImpl);
            IEnumerable<Type> services = impl.GetInterfaces().Concat(new[] { impl });
            return new MultiBinder(this, services, impl);
        }

        public MultiBinder BindInterfacesTo<TImpl>()
        {
            Type impl = typeof(TImpl);
            IEnumerable<Type> services = impl.GetInterfaces();
            return new MultiBinder(this, services, impl);
        }

        // ------------------ Registration core ------------------
        public void Register(Type service, Type impl, Lifetime lifetime, object instance, Func<DiContainer, object> factory)
        {
            if (service == null) throw new ArgumentNullException("service");
            if (impl == null) throw new ArgumentNullException("impl");

            // impl должен соответствовать service, либо быть тем же типом
            if (!service.IsAssignableFrom(impl) && impl != service)
                throw new InvalidOperationException("Type " + impl + " is not assignable to " + service);

            var b = new Binding(service, impl, lifetime);
            b.Instance = instance;
            b.Factory = factory;
            b.OwnsSingleton = false;

            if (instance != null)
            {
                // FromInstance всегда трактуем как Singleton и не владеем им
                if (!service.IsInstanceOfType(instance))
                    throw new InvalidOperationException("FromInstance object is not assignable to " + service);

                _singletons[service] = instance;
                b.Lifetime.Equals(Lifetime.Singleton); // просто фикс: намерение — singleton
                b.OwnsSingleton = false;
            }

            _bindings[service] = b;
        }

        // ------------------ Resolve ------------------
        public T Resolve<T>() { return (T)Resolve(typeof(T)); }

        public object Resolve(Type serviceType)
        {
            if (_disposed) throw new ObjectDisposedException(nameof(DiContainer));
            if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));

            if (serviceType == typeof(DiContainer))
                return this;
            
            object existing;
            if (_singletons.TryGetValue(serviceType, out existing))
                return existing;

            Binding binding;
            if (_bindings.TryGetValue(serviceType, out binding))
                return CreateAccordingTo(binding);

            // если просят конкретный класс без биндинга — пытаемся сконструировать напрямую
            if (!serviceType.IsAbstract && !serviceType.IsInterface)
                return CreateInstance(serviceType);

            // попытка найти совместимый биндинг (редкий кейс)
            foreach (var kv in _bindings)
            {
                if (serviceType.IsAssignableFrom(kv.Key))
                    return CreateAccordingTo(kv.Value);
            }

            throw new InvalidOperationException("No binding found for " + serviceType);
        }

        private object CreateAccordingTo(Binding b)
        {
            // 1) Factory
            if (b.Factory != null)
            {
                if (b.Lifetime == Lifetime.Singleton)
                {
                    object cached;
                    if (_singletons.TryGetValue(b.Service, out cached))
                        return cached;

                    var created = b.Factory(this);
                    _singletons[b.Service] = created;
                    // Раз создал контейнер — он владеет
                    b.OwnsSingleton = true;
                    _ownedSingletonsInOrder.Add(created);
                    return created;
                }
                // Transient
                return b.Factory(this);
            }

            // 2) FromInstance (уже в _singletons), контейнер не владеет
            if (b.Instance != null)
                return _singletons[b.Service];

            // 3) Обычное создание
            if (b.Lifetime == Lifetime.Singleton)
            {
                object cached2;
                if (_singletons.TryGetValue(b.Service, out cached2))
                    return cached2;

                var created2 = CreateInstance(b.Impl);
                _singletons[b.Service] = created2;
                b.OwnsSingleton = true;
                _ownedSingletonsInOrder.Add(created2);
                return created2;
            }

            // Transient
            return CreateInstance(b.Impl);
        }

        // ------------------ Creation + [Inject] ------------------
        private object CreateInstance(Type implType)
        {
            var instance = ConstructViaConstructorInjection(implType);
            RunMemberInjection(instance);
            RunMethodInjection(instance);
            return instance;
        }

        private object ConstructViaConstructorInjection(Type implType)
        {
            var ctors = implType.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var injectCtors = new List<ConstructorInfo>();
            for (int i = 0; i < ctors.Length; i++)
            {
                if (ctors[i].GetCustomAttribute(typeof(InjectAttribute)) != null)
                    injectCtors.Add(ctors[i]);
            }

            ConstructorInfo chosen = null;

            if (injectCtors.Count > 1)
                throw new InvalidOperationException("Type " + implType + " has multiple [Inject] constructors.");

            if (injectCtors.Count == 1)
            {
                chosen = injectCtors[0];
            }
            else
            {
                // «Самый толстый» публичный
                int max = -1;
                for (int i = 0; i < ctors.Length; i++)
                {
                    if (!ctors[i].IsPublic) continue;
                    int count = ctors[i].GetParameters().Length;
                    if (count > max) { max = count; chosen = ctors[i]; }
                }
                if (chosen == null)
                    throw new InvalidOperationException("Type " + implType + " has no accessible constructor.");
            }

            var args = ResolveParameterList(chosen);
            return chosen.Invoke(args);
        }

        private object[] ResolveParameterList(MethodBase methodBase)
        {
            var pars = methodBase.GetParameters();
            var result = new object[pars.Length];

            var injectAttr = (InjectAttribute)Attribute.GetCustomAttribute(methodBase, typeof(InjectAttribute));
            bool methodOptional = (injectAttr != null) && injectAttr.Optional;

            for (int i = 0; i < pars.Length; i++)
            {
                var p = pars[i];
                try
                {
                    result[i] = Resolve(p.ParameterType);
                }
                catch
                {
                    if (methodOptional || p.HasDefaultValue)
                        result[i] = p.HasDefaultValue ? p.DefaultValue : GetDefault(p.ParameterType);
                    else
                        throw;
                }
            }
            return result;
        }

        private static object GetDefault(Type t)
        {
            return t.IsValueType ? Activator.CreateInstance(t) : null;
        }

        private void RunMemberInjection(object instance)
        {
            var type = instance.GetType();

            // поля
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            for (int i = 0; i < fields.Length; i++)
            {
                var f = fields[i];
                if (Attribute.GetCustomAttribute(f, typeof(InjectAttribute)) == null) continue;

                var attr = (InjectAttribute)Attribute.GetCustomAttribute(f, typeof(InjectAttribute));
                try
                {
                    var dep = Resolve(f.FieldType);
                    f.SetValue(instance, dep);
                }
                catch
                {
                    if (attr == null || !attr.Optional) throw;
                }
            }

            // свойства
            var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            for (int i = 0; i < props.Length; i++)
            {
                var p = props[i];
                if (!p.CanWrite) continue;
                if (Attribute.GetCustomAttribute(p, typeof(InjectAttribute)) == null) continue;

                var attr = (InjectAttribute)Attribute.GetCustomAttribute(p, typeof(InjectAttribute));
                try
                {
                    var dep = Resolve(p.PropertyType);
                    p.SetValue(instance, dep, null);
                }
                catch
                {
                    if (attr == null || !attr.Optional) throw;
                }
            }
        }

        private void RunMethodInjection(object instance)
        {
            var methods = instance.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            // Соберём [Inject]-методы с их Order
            var list = new List<Tuple<MethodInfo, int, bool>>();
            for (int i = 0; i < methods.Length; i++)
            {
                var m = methods[i];
                var attr = (InjectAttribute)Attribute.GetCustomAttribute(m, typeof(InjectAttribute));
                if (attr == null) continue;
                list.Add(Tuple.Create(m, attr.Order, attr.Optional));
            }
            // сортировка по Order
            list.Sort((a, b) => a.Item2.CompareTo(b.Item2));

            for (int i = 0; i < list.Count; i++)
            {
                var method = list[i].Item1;
                var args = ResolveParameterList(method);
                method.Invoke(instance, args);
            }
        }

        // ------------------ Lifecycle ------------------
        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            // Освобождаем только те singleton'ы, которыми владеет контейнер, и в обратном порядке
            for (int i = _ownedSingletonsInOrder.Count - 1; i >= 0; i--)
            {
                var obj = _ownedSingletonsInOrder[i] as IDisposable;
                if (obj == null) continue;

                try { obj.Dispose(); }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("[DiContainer] Dispose error: " + e);
                }
            }

            _ownedSingletonsInOrder.Clear();
            _singletons.Clear();
            _bindings.Clear();
        }
    }
}
