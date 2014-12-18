using System;
using System.Globalization;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Vega.USiteBuilder.Types;

namespace USiteBuilderContrib.TypeConverters
{
    public class MemberPickerToMemberTypeConvertor : ICustomTypeConvertor
    {
        private readonly IMemberService _memberService;

        public MemberPickerToMemberTypeConvertor()
            : this(ApplicationContext.Current.Services.MemberService)
        {
        }

        public MemberPickerToMemberTypeConvertor(IMemberService memberService)
        {
            if (memberService == null)
                throw new ArgumentNullException();
            _memberService = memberService;
        }

        public Type ConvertType
        {
            get { return typeof(IMember); }
        }

        public object ConvertValueWhenRead(object inputValue)
        {
            if (inputValue == null)
                return null;

            int memberId;
            return Int32.TryParse(inputValue.ToString(), out memberId) ? 
                _memberService.GetById(memberId) : null;
        }

        public object ConvertValueWhenWrite(object inputValue)
        {
            var member = inputValue as IMember;
            return member != null ? member.Id.ToString(CultureInfo.InvariantCulture) : inputValue;
        }
    }
}
