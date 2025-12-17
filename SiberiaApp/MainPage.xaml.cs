using SiberiaApp.Classes.ViewModels;
namespace SiberiaApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainMenu mm)
        {
            InitializeComponent();
            BindingContext = mm;
        }
    }
}
