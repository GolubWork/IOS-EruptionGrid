using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Code.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Extension-методы для DiContainer: Instantiate<T>(params object[] args)
    /// Создаёт объект типа T, подставляя переданные args по типу,
    /// резолвит недостающие параметры через контейнер,
    /// затем выполняет [Inject] для полей/свойств/методов.
    /// </summary>
    public static class DiContainerInstantiateExtensions
    {
        /// <summary>Создать T с подстановкой args и DI-инъекцией.</summary>
        public static T Instantiate<T>(this DiContainer c, params object[] args)
            => (T)Instantiate(c, typeof(T), args);

        /// <summary>Создать объект указанного типа с подстановкой args и DI-инъекцией.</summary>
        public static object Instantiate(this DiContainer c, Type type, params object[] args)
        {
            if (c == null) throw new ArgumentNullException(nameof(c));
            if (type == null) throw new ArgumentNullException(nameof(type));

            // Не поддерживаем MonoBehaviour/Component через этот путь — используйте IInstantiator
            if (typeof(UnityEngine.Component).IsAssignableFrom(type))
                throw new InvalidOperationException(
                    $"[{nameof(DiContainerInstantiateExtensions)}] Can't Instantiate Component '{type.Name}'. " +
                    $"Use IInstantiator.InstantiatePrefab/InstantiatePrefabForComponent for Unity components.");

            var ctor = ChooseConstructor(type);
            var ctorArgs = BuildConstructorArgs(c, ctor, args);
            var instance = ctor.Invoke(ctorArgs);

            // Инъекция полей/свойств
            InjectMembers(c, instance);

            // Инъекция методов ([Inject]-методы, с порядком по Order)
            InjectMethods(c, instance);

            return instance;
        }

        // ---------- helpers ----------

        private static ConstructorInfo ChooseConstructor(Type implType)
        {
            var ctors = implType.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            // приоритет: [Inject]-ctor (ровно один) -> «самый толстый» public ctor
            var injectCtors = ctors.Where(c => c.GetCustomAttribute<InjectAttribute>() != null).ToArray();
            if (injectCtors.Length > 1)
                throw new InvalidOperationException($"Type {implType} has multiple [Inject] constructors.");
            if (injectCtors.Length == 1)
                return injectCtors[0];

            var chosen = ctors.Where(c => c.IsPublic)
                              .OrderByDescending(c => c.GetParameters().Length)
                              .FirstOrDefault();
            if (chosen == null)
                throw new InvalidOperationException($"Type {implType} has no accessible constructor.");
            return chosen;
        }

        private static object[] BuildConstructorArgs(DiContainer c, ConstructorInfo ctor, object[] extraArgs)
        {
            var pars = ctor.GetParameters();
            var result = new object[pars.Length];

            // копия списка аргументов, которые ещё не «сопоставлены»
            var pool = new List<object>(extraArgs ?? Array.Empty<object>());

            var injectAttr = ctor.GetCustomAttribute<InjectAttribute>();
            bool ctorOptional = injectAttr?.Optional ?? false;

            for (int i = 0; i < pars.Length; i++)
            {
                var p = pars[i];

                // 1) Поищем подходящий extraArg по assignable-типу (первый попавшийся)
                int idx = pool.FindIndex(a => a != null && p.ParameterType.IsInstanceOfType(a));
                if (idx >= 0)
                {
                    result[i] = pool[idx];
                    pool.RemoveAt(idx);
                    continue;
                }

                // 2) Попробуем Resolve из контейнера
                try
                {
                    result[i] = c.Resolve(p.ParameterType);
                    continue;
                }
                catch
                {
                    // 3) Если ctor помечен Optional или есть default у параметра — подставим default
                    if (ctorOptional || p.HasDefaultValue)
                    {
                        result[i] = p.HasDefaultValue
                            ? p.DefaultValue
                            : (p.ParameterType.IsValueType ? Activator.CreateInstance(p.ParameterType) : null);
                        continue;
                    }
                    throw; // ни extraArg, ни Resolve — бросаем дальше
                }
            }

            return result;
        }

        private static void InjectMembers(DiContainer c, object instance)
        {
            var t = instance.GetType();

            // Поля
            var fields = t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var f in fields)
            {
                var attr = f.GetCustomAttribute<InjectAttribute>();
                if (attr == null) continue;

                try
                {
                    var dep = c.Resolve(f.FieldType);
                    f.SetValue(instance, dep);
                }
                catch
                {
                    if (!attr.Optional) throw;
                }
            }

            // Свойства
            var props = t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var p in props)
            {
                if (!p.CanWrite) continue;
                var attr = p.GetCustomAttribute<InjectAttribute>();
                if (attr == null) continue;

                try
                {
                    var dep = c.Resolve(p.PropertyType);
                    p.SetValue(instance, dep, null);
                }
                catch
                {
                    if (!attr.Optional) throw;
                }
            }
        }

        private static void InjectMethods(DiContainer c, object instance)
        {
            var methods = instance.GetType()
                                  .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                  .Select(m => new { Method = m, Attr = m.GetCustomAttribute<InjectAttribute>() })
                                  .Where(x => x.Attr != null)
                                  .OrderBy(x => x.Attr.Order)
                                  .ToArray();

            foreach (var x in methods)
            {
                var pars = x.Method.GetParameters();
                var args = new object[pars.Length];
                bool methodOptional = x.Attr.Optional;

                for (int i = 0; i < pars.Length; i++)
                {
                    try
                    {
                        args[i] = c.Resolve(pars[i].ParameterType);
                    }
                    catch
                    {
                        if (methodOptional || pars[i].HasDefaultValue)
                            args[i] = pars[i].HasDefaultValue
                                ? pars[i].DefaultValue
                                : (pars[i].ParameterType.IsValueType ? Activator.CreateInstance(pars[i].ParameterType) : null);
                        else
                            throw;
                    }
                }

                x.Method.Invoke(instance, args);
            }
        }
    }
}