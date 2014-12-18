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
        private readonly UmbracoContext _umbracoContext;

        public ContentPickerToIPublishedContentConvertor()
            : this(UmbracoContext.Current)
        {
        }

        public ContentPickerToIPublishedContentConvertor(UmbracoContext umbracoContext)
        {
            if (umbracoContext == null)
                throw new ArgumentNullException();
            _umbracoContext = umbracoContext;
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
                _umbracoContext.ContentCache.GetById(nodeId) : null;
        }

        public object ConvertValueWhenWrite(object inputValue)
        {
            var content = inputValue as IPublishedContent;
            return content != null ? content.Id.ToString(CultureInfo.InvariantCulture) : inputValue;
        }
    }
}
