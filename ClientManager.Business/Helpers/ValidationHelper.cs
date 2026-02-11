using System.Text.RegularExpressions;

namespace ClientManager.Business.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            var cleaned = Regex.Replace(phone, @"[\s\-\(\)]", "");
            return cleaned.Length >= 10 && cleaned.Length <= 15;
        }

        public static string SanitizeInput(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return input.Trim();
        }
    }
}
