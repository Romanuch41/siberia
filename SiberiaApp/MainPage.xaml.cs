using SiberiaApp.Classes.ViewModels;
using System.Diagnostics;

namespace SiberiaApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()  // ✅ Параметр для DI
        {
            InitializeComponent();
            BindingContext = new MainMenuViewModels();
            Debug.WriteLine("Визуальная модель создана");
            Debug.WriteLine($"{((MainMenuViewModels)BindingContext).TitlePage}");
            Debug.WriteLine($"{((MainMenuViewModels)BindingContext).CollectionsButton.Count}");
        }
    }
}
