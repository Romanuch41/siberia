//using Javax.Security.Auth;

namespace SiberiaApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            FlyoutBehavior = FlyoutBehavior.Disabled;

            Navigated += OnShellFirstNavigate;
        }

        private async void OnShellFirstNavigate(object sender, EventArgs e)
        {
            Navigated -= OnShellFirstNavigate;
            await Shell.Current.GoToAsync("//authorization");
        }

        public void ShowMainFlyOut()
        {
            FlyoutBehavior = FlyoutBehavior.Flyout;
        }
    }
}
