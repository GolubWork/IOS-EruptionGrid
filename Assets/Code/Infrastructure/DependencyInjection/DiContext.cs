using System;
using UnityEngine;

namespace Code.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Разворачивает DiContainer и инъектит MonoBehaviour-компоненты.
    /// Переопредели InstallBindings(...) для своих регистраций.
    /// </summary>
    public class DiContext : MonoBehaviour
    {
        public static DiContext Instance { get; private set; }
        public static bool HasInstance => Instance != null;
        public DiContainer Container { get; private set; }

        protected virtual void Awake()
        {
            // singleton-guard
            if (Instance != null && Instance != this)
            {
                Debug.LogWarning("[DiContext] Duplicate instance found. Destroying this one.");
                Destroy(gameObject);
                return;
            }

            Instance = this;
            Container = new DiContainer();

            // Регистрации пользователя
            InstallBindings(Container);

            // По желанию можно включить автоинъекцию всего дерева под этим GO:
            // var injector = GetComponent<SceneInjector>();
            // if (injector == null) InjectGameObject(gameObject, includeChildren: true);
        }

        /// <summary>
        /// Зарегистрируй свои сервисы: c.Bind<IService>().To<Service>().AsSingle();
        /// </summary>
        protected virtual void InstallBindings(DiContainer c) { }

        // --- Вспомогательные методы инъекции для MonoBehaviour (остались как были) ---

        public void Inject(object instance)
        {
            if (instance == null || Container == null) return;
            // Используем внутренние методы контейнера через публичные точки:
            // Контейнер сам умеет [Inject] по полям/свойствам/методам при создании,
            // а для уже созданных объектов — вызовем приватные рантайм-инъекции:
            // Эти методы у нас инкапсулированы, поэтому здесь вызовем «публичный» путь —
            // просто попросим контейнер разрешить зависимости в метод-инъекции.
            // Для простоты — используем вспомогатель ниже:
            RunFieldPropertyAndMethodInjection(instance);
        }

        public void InjectGameObject(GameObject root, bool includeChildren = true)
        {
            if (root == null || Container == null) return;

            if (includeChildren)
            {
                var all = root.GetComponentsInChildren<Component>(includeInactive: true);
                foreach (var comp in all) SafeInject(comp);
            }
            else
            {
                var comps = root.GetComponents<Component>();
                foreach (var comp in comps) SafeInject(comp);
            }
        }

        private void SafeInject(Component comp)
        {
            if (comp == null) return;
            try { RunFieldPropertyAndMethodInjection(comp); }
            catch (Exception e)
            {
                Debug.LogError($"[DiContext] Injection failed for {comp.GetType().Name}: {e}");
                throw;
            }
        }

        // Мини-хелпер: повторяем поведение контейнера для уже созданных объектов
        private void RunFieldPropertyAndMethodInjection(object instance)
        {
            // Используем внутренний API контейнера посредством «создания» без конструктора:
            // трюк: контейнер уже умеет инъектить члены/методы в CreateInstance(...),
            // но нам нужен только этап post-construction.
            // Чтобы не раскрывать приватные методы, выделим отдельный Utility при желании.
            // Здесь — компактная локальная реализация:
            var t = instance.GetType();

            // поля
            var fields = t.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
            foreach (var f in fields)
            {
                var attr = (InjectAttribute)Attribute.GetCustomAttribute(f, typeof(InjectAttribute));
                if (attr == null) continue;
                try { f.SetValue(instance, Container.Resolve(f.FieldType)); }
                catch { if (!attr.Optional) throw; }
            }

            // свойства
            var props = t.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
            foreach (var p in props)
            {
                if (!p.CanWrite) continue;
                var attr = (InjectAttribute)Attribute.GetCustomAttribute(p, typeof(InjectAttribute));
                if (attr == null) continue;
                try { p.SetValue(instance, Container.Resolve(p.PropertyType), null); }
                catch { if (!attr.Optional) throw; }
            }

            // методы (по Order)
            var methods = t.GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
            var list = new System.Collections.Generic.List<Tuple<System.Reflection.MethodInfo, int, bool>>();
            foreach (var m in methods)
            {
                var attr = (InjectAttribute)Attribute.GetCustomAttribute(m, typeof(InjectAttribute));
                if (attr == null) continue;
                list.Add(Tuple.Create(m, attr.Order, attr.Optional));
            }
            list.Sort((a, b) => a.Item2.CompareTo(b.Item2));

            foreach (var item in list)
            {
                var mb = item.Item1;
                var pars = mb.GetParameters();
                var args = new object[pars.Length];
                for (int i = 0; i < pars.Length; i++)
                {
                    try { args[i] = Container.Resolve(pars[i].ParameterType); }
                    catch
                    {
                        if (item.Item3 || pars[i].HasDefaultValue)
                            args[i] = pars[i].HasDefaultValue ? pars[i].DefaultValue : (pars[i].ParameterType.IsValueType ? Activator.CreateInstance(pars[i].ParameterType) : null);
                        else
                            throw;
                    }
                }
                mb.Invoke(instance, args);
            }
        }

        // ---- ВАЖНО: корректная утилизация контейнера ----
        protected virtual void OnDestroy()
        {
            if (Container != null)
            {
                try { Container.Dispose(); }
                catch (Exception e) { Debug.LogWarning($"[DiContext] Container.Dispose() threw: {e}"); }
                finally { Container = null; }
            }

            if (Instance == this) Instance = null;
        }
    }
}
