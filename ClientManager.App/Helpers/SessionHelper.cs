using System;

namespace ClientManager.App.Helpers
{
    // Static session state — global mutable state, classic legacy
    public static class SessionHelper
    {
        public static string CurrentUser { get; set; }
        public static DateTime LoginTime { get; set; }
        public static bool IsLoggedIn { get; set; }

        public static void Login(string username)
        {
            CurrentUser = username;
            LoginTime = DateTime.Now;
            IsLoggedIn = true;
        }

        public static void Logout()
        {
            CurrentUser = null;
            IsLoggedIn = false;
        }
    }
}
