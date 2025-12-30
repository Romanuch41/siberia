using SiberiaApp.Classes.ViewModels;
namespace SiberiaApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()  // ✅ Параметр для DI
        {
            InitializeComponent();
            BindingContext = new MainMenu();
        }
    }
}
