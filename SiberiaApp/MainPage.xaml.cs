using SiberiaApp.Classes.ViewModels;
using System.Diagnostics;

namespace SiberiaApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainMenuViewModels mv)  // ✅ Параметр для DI
        {
            InitializeComponent();
            BindingContext = mv;
            Debug.WriteLine("Визуальная модель создана");
            Debug.WriteLine($"{((MainMenuViewModels)BindingContext).TitlePage}");
            Debug.WriteLine($"{((MainMenuViewModels)BindingContext).CollectionsButton.Count}");
        }
    }
}
