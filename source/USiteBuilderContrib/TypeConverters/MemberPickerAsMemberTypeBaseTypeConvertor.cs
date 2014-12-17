using System;
using System.Globalization;
using Vega.USiteBuilder;
using Vega.USiteBuilder.Types;

namespace USiteBuilderContrib.TypeConverters
{
    /// <summary>
    /// Converts the output from a content picker to a DocumentTypeBase model
    /// </summary>
    public class MemberPickerAsMemberTypeBaseTypeConvertor : MemberPickerAsMemberTypeBaseTypeConvertor<MemberTypeBase>
    {
    }

    /// <summary>
    /// Converts the output from a content picker to a DocumentTypeBase-derived model
    /// </summary>
    /// <typeparam name="TMemberType"></typeparam>
    public class MemberPickerAsMemberTypeBaseTypeConvertor<TMemberType> : ICustomTypeConvertor
        where TMemberType : MemberTypeBase
    {
        public Type ConvertType
        {
            get { return typeof(TMemberType); }
        }

        public object ConvertValueWhenRead(object inputValue)
        {
            if (inputValue == null)
                return null;
            
            string memberIdStr = inputValue.ToString();
            return string.IsNullOrEmpty(memberIdStr) ? null : MemberHelper.GetMemberById<TMemberType>(int.Parse(memberIdStr));
        }

        public object ConvertValueWhenWrite(object inputValue)
        {
            if (inputValue == null)
                return null;

            MemberTypeBase memberType = inputValue as MemberTypeBase;
            return memberType != null ? memberType.Id.ToString(CultureInfo.InvariantCulture) : inputValue;
        }
    }
}
