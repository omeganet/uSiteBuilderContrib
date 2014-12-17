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
    public class ContentPickerAsIPublishedContentConvertor : ICustomTypeConvertor
    {
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

            string nodeIdStr = inputValue.ToString();
            return string.IsNullOrEmpty(nodeIdStr) ? null : UmbracoContext.Current.ContentCache.GetById(int.Parse(nodeIdStr));
        }

        public object ConvertValueWhenWrite(object inputValue)
        {
            if (inputValue == null)
                return null;

            var content = inputValue as IPublishedContent;
            return content != null ? content.Id.ToString(CultureInfo.InvariantCulture) : inputValue;
        }
    }
}
