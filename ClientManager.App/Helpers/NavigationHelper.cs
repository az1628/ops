using System.Windows.Controls;

namespace ClientManager.App.Helpers
{
    // Static navigation — global state, untestable
    public static class NavigationHelper
    {
        public static Frame MainFrame { get; set; }

        public static void NavigateTo(Page page)
        {
            MainFrame?.Navigate(page);
        }

        public static void GoBack()
        {
            if (MainFrame != null && MainFrame.CanGoBack)
                MainFrame.GoBack();
        }
    }
}
