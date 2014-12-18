using System;
using System.Collections.Generic;
using Vega.USiteBuilder.Types.Activation;

namespace USiteBuilderContrib.Types.Activation
{
    /// <summary>
    /// Caches the created instances
    /// </summary>
    public class CachedActivator : IActivator
    {
        private readonly IActivator _parent;
        private readonly Dictionary<Type, object> _instanceMap;

        public CachedActivator(IActivator parent)
        {
            if (parent == null)
                throw new ArgumentNullException();
            _parent = parent;
            _instanceMap = new Dictionary<Type, object>();
        }

        public object GetInstance(Type type)
        {
            if (type != null && _instanceMap.ContainsKey(type))
                return _instanceMap[type];

            object instance = _parent.GetInstance(type);
            if (type != null && instance != null && ShouldCache(type, instance))
                _instanceMap.Add(type, instance);

            return instance;
        }

        /// <summary>
        /// Custom filter for whether to cache a type/instance pair
        /// </summary>
        /// <param name="type">the type</param>
        /// <param name="instance">the created instance</param>
        /// <returns></returns>
        protected virtual bool ShouldCache(Type type, object instance)
        {
            return true;
        }
    }
}
