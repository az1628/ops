using System;

namespace ClientManager.Business.Helpers
{
    public static class FormatHelper
    {
        public static string FormatCurrency(decimal amount)
        {
            return string.Format("£{0:N2}", amount);
        }

        public static string FormatDate(DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }

        public static string FormatAccountNumber(string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber)) return "N/A";
            return accountNumber.ToUpper();
        }

        // Legacy smell: UI concern leaked into business layer
        public static string GetStatusColor(string status)
        {
            switch (status?.ToLower())
            {
                case "active": return "#27AE60";
                case "inactive": return "#E74C3C";
                case "prospect": return "#F39C12";
                case "open": return "#27AE60";
                case "closed": return "#E74C3C";
                case "frozen": return "#3498DB";
                default: return "#95A5A6";
            }
        }

        public static string TruncateText(string text, int maxLength = 50)
        {
            if (string.IsNullOrEmpty(text)) return "";
            return text.Length <= maxLength ? text : text.Substring(0, maxLength) + "...";
        }
    }
}
