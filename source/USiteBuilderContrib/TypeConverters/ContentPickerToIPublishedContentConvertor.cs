using System;
using System.Globalization;
using Umbraco.Core.Models;
using Umbraco.Web;
using Vega.USiteBuilder.Types;

namespace USiteBuilderContrib.TypeConverters
{
    /// <summary>
    /// Converts the output from a content picker to an IPublishedContent model
    /// </summary>
    public class ContentPickerToIPublishedContentConvertor : ICustomTypeConvertor
    {
        private readonly Func<UmbracoContext> _getUmbracoContext;

        public ContentPickerToIPublishedContentConvertor()
            : this(() => UmbracoContext.Current)
        {
        }

        public ContentPickerToIPublishedContentConvertor(Func<UmbracoContext> getUmbracoContext)
        {
            if (getUmbracoContext == null)
                throw new ArgumentNullException();
            _getUmbracoContext = getUmbracoContext;
        }

        public Type ConvertType
        {
            get
            {
                // As IPublishedContent can also be used for images, we cannot be sure that a document
                // is requested. Thefore, this converter can only be used via the CustomTypeConverter property
                return GetType();
            }
        }

        public object ConvertValueWhenRead(object inputValue)
        {
            if (inputValue == null)
                return null;

            int nodeId;
            return Int32.TryParse(inputValue.ToString(), out nodeId) ?
                _getUmbracoContext().ContentCache.GetById(nodeId) : null;
        }

        public object ConvertValueWhenWrite(object inputValue)
        {
            var content = inputValue as IPublishedContent;
            return content != null ? content.Id.ToString(CultureInfo.InvariantCulture) : inputValue;
        }
    }
}
