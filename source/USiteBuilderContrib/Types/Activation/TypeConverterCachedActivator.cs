using System;
using Vega.USiteBuilder.Types;
using Vega.USiteBuilder.Types.Activation;

namespace USiteBuilderContrib.Types.Activation
{
    /// <summary>
    /// Caches the created instances of ICustomTypeConvertor
    /// </summary>
    public class TypeConverterCachedActivator : CachedActivator
    {
        public TypeConverterCachedActivator(IActivator parent)
            : base(parent)
        {
        }

        protected override bool ShouldCache(Type type, object instance)
        {
            return typeof(ICustomTypeConvertor).IsAssignableFrom(type);
        }
    }
}
