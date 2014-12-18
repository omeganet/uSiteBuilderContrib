using System;
using System.Globalization;
using Vega.USiteBuilder;
using Vega.USiteBuilder.Types;

namespace USiteBuilderContrib.TypeConverters
{
    /// <summary>
    /// Converts the output from a content picker to a DocumentTypeBase model
    /// </summary>
    public class ContentPickerToDocumentTypeBaseTypeConvertor : ContentPickerToDocumentTypeBaseTypeConvertor<DocumentTypeBase>
    {
        public ContentPickerToDocumentTypeBaseTypeConvertor()
        {
        }

        public ContentPickerToDocumentTypeBaseTypeConvertor(DocumentTypeResolver resolver)
            : base(resolver)
        {
        }
    }

    /// <summary>
    /// Converts the output from a content picker to a DocumentTypeBase-derived model
    /// </summary>
    /// <typeparam name="TDocumentType"></typeparam>
    public class ContentPickerToDocumentTypeBaseTypeConvertor<TDocumentType> : ICustomTypeConvertor
        where TDocumentType : DocumentTypeBase
    {
        private readonly DocumentTypeResolver _resolver;

        public ContentPickerToDocumentTypeBaseTypeConvertor()
            : this(DocumentTypeResolver.Instance)
        {
        }

        public ContentPickerToDocumentTypeBaseTypeConvertor(DocumentTypeResolver resolver)
        {
            if (resolver == null)
                throw new ArgumentNullException();
            _resolver = resolver;
        }

        public Type ConvertType
        {
            get { return typeof(TDocumentType); }
        }

        public object ConvertValueWhenRead(object inputValue)
        {
            if (inputValue == null)
                return null;

            int nodeId;
            return Int32.TryParse(inputValue.ToString(), out nodeId) ?
                _resolver.GetTyped<TDocumentType>(nodeId) : null;
        }

        public object ConvertValueWhenWrite(object inputValue)
        {
            var documentType = inputValue as DocumentTypeBase;
            return documentType != null ? documentType.Id.ToString(CultureInfo.InvariantCulture) : inputValue;
        }
    }
}
