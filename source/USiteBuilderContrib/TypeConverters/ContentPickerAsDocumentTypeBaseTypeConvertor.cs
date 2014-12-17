using System;
using System.Globalization;
using Vega.USiteBuilder;
using Vega.USiteBuilder.Types;

namespace USiteBuilderContrib.TypeConverters
{
    /// <summary>
    /// Converts the output from a content picker to a DocumentTypeBase model
    /// </summary>
    public class ContentPickerAsDocumentTypeBaseTypeConvertor : ContentPickerAsDocumentTypeBaseTypeConvertor<DocumentTypeBase>
    {
    }

    /// <summary>
    /// Converts the output from a content picker to a DocumentTypeBase-derived model
    /// </summary>
    /// <typeparam name="TDocumentType"></typeparam>
    public class ContentPickerAsDocumentTypeBaseTypeConvertor<TDocumentType> : ICustomTypeConvertor
        where TDocumentType : DocumentTypeBase
    {
        public Type ConvertType
        {
            get { return typeof(TDocumentType); }
        }

        public object ConvertValueWhenRead(object inputValue)
        {
            if (inputValue == null)
                return null;

            string nodeIdStr = inputValue.ToString();
            return string.IsNullOrEmpty(nodeIdStr) ? null : DocumentTypeResolver.Instance.GetTyped<TDocumentType>(int.Parse(nodeIdStr));
        }

        public object ConvertValueWhenWrite(object inputValue)
        {
            if (inputValue == null)
                return null;

            DocumentTypeBase docType = inputValue as DocumentTypeBase;
            return docType != null ? docType.Id.ToString(CultureInfo.InvariantCulture) : inputValue;
        }
    }
}
