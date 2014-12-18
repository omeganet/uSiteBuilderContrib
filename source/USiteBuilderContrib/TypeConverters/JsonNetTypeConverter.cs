using System;
using Newtonsoft.Json;
using Vega.USiteBuilder.Types;

namespace USiteBuilderContrib.TypeConverters
{
    public class JsonNetTypeConverter : ICustomTypeConvertor
    {
        public Type ConvertType
        {
            get { return typeof(JsonNetTypeConverter); }
        }

        public object ConvertValueWhenRead(object inputValue)
        {
            return inputValue != null ? JsonConvert.DeserializeObject(inputValue.ToString()) : null;
        }

        public object ConvertValueWhenWrite(object inputValue)
        {
            return JsonConvert.SerializeObject(inputValue);
        }
    }
}
