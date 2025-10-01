using System;
using System.Collections.Generic;
using System.Linq;

namespace Code.Infrastructure.DependencyInjection
{
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

        public MultiBinder FromInstance(object instance)
        {
            _instance = instance;
            return this;
        }

        public MultiBinder FromFactory(Func<DiContainer, object> factory)
        {
            _factory = factory;
            return this;
        }

        public void AsSingle()
        {
            foreach (var s in _services)
                _c.Register(s, _impl, Lifetime.Singleton, _instance, _factory);
        }

        public void AsTransient()
        {
            foreach (var s in _services)
                _c.Register(s, _impl, Lifetime.Transient, _instance, _factory);
        }
    }
}