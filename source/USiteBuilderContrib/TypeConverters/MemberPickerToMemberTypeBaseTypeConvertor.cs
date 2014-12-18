using System;
using System.Globalization;
using Vega.USiteBuilder;
using Vega.USiteBuilder.Types;

namespace USiteBuilderContrib.TypeConverters
{
    /// <summary>
    /// Converts the output from a content picker to a DocumentTypeBase model
    /// </summary>
    public class MemberPickerToMemberTypeBaseTypeConvertor : MemberPickerToMemberTypeBaseTypeConvertor<MemberTypeBase>
    {
    }

    /// <summary>
    /// Converts the output from a content picker to a DocumentTypeBase-derived model
    /// </summary>
    /// <typeparam name="TMemberType"></typeparam>
    public class MemberPickerToMemberTypeBaseTypeConvertor<TMemberType> : ICustomTypeConvertor
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

            int memberId;
            return Int32.TryParse(inputValue.ToString(), out memberId) ?
                MemberHelper.GetMemberById<TMemberType>(memberId) : null;
        }

        public object ConvertValueWhenWrite(object inputValue)
        {
            var memberType = inputValue as MemberTypeBase;
            return memberType != null ? memberType.Id.ToString(CultureInfo.InvariantCulture) : inputValue;
        }
    }
}
