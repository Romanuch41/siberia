using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Google.Apis.Drive.v3;
using Google.Apis.Sheets.v4;
using Microsoft.Extensions.Logging;
using SiberiaApp.Classes.Elements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiberiaApp.Classes.ViewModels
{
    public partial class MainMenu : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<ButtonModel> buttons = new();

        [ObservableProperty]
        private string currentText;   // для наглядности, что навигация работает
        [ObservableProperty]
        private string mainTitle = "Главное меню";

        public MainMenu()
        {
            Buttons = new()
        {
            new() { Title = "Заказы",        BackgroundColor = "#008676", CommandId = "orders" },
            new() { Title = "Главное меню", BackgroundColor = "#008676", CommandId = "mainMenu" }
        };
        }

        [RelayCommand]
        private async Task Navigate(string route)
        {
            System.Diagnostics.Debug.WriteLine($"🔥 Navigate вызван: {route}");


            // Если хочешь ещё менять заголовок:
            MainTitle = route switch
            {
                "mainMenu" => "Главное меню",
                "reports" => "Отчеты",
                "SettingsPage" => "Настройки",
                _ => $"Неизвестный маршрут: {route}"
            };

            System.Diagnostics.Debug.WriteLine($"MainTitle изменён на: '{MainTitle}'");
        }

    }
}
