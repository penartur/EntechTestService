using System;
using System.Net.Mail;

namespace EntechTestService.Services.Validation
{
    public class EmailValidator
    {
        public bool IsEmailCorrect(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email is null or empty", "email");
            }

            try
            {
                var address = new MailAddress(email);
                return address.Host.Contains(".");
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
