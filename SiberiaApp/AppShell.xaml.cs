using Javax.Security.Auth;

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
            await Navigation.PushAsync(new Authorization());
        }
    }
}
