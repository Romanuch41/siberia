using SiberiaApp.Classes.ViewModels;
namespace SiberiaApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainMenu();
        }
    }
}
