using System.Text.RegularExpressions;

namespace EntechTestService.Services.Validation
{
    public class PhoneValidator
    {
        private static readonly Regex PhoneRegex = new Regex("^\\+(?=.*\\d{3})[\\d\\s]+$", RegexOptions.Compiled);

        public bool IsPhoneCorrect(string phone)
        {
            return PhoneRegex.IsMatch(phone);
        }
    }
}
