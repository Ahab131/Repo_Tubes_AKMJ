using System;

namespace RegistrationLogic.Helpers
{
    public static class ValidationHelper
    {
        // Validasi panjang string
        public static bool ValidateLength(string input, int minLength, int maxLength)
        {
            return input != null && input.Length >= minLength && input.Length <= maxLength;
        }

        // Validasi format email
        public static bool ValidateEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Validasi umur minimum
        public static bool ValidateMinimumAge(DateTime birthDate, int minAge)
        {
            var age = DateTime.Today.Year - birthDate.Year;
            if (birthDate.Date > DateTime.Today.AddYears(-age)) age--;
            return age >= minAge;
        }
    }
}