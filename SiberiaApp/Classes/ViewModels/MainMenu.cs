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
        private string currentText = "Главное меню";   // для наглядности, что навигация работает

        public MainMenu()
        {
            Buttons = new()
        {
            new() { Title = "Заказы",        BackgroundColor = "#008676", CommandId = "orders" },
            new() { Title = "Главное меню", BackgroundColor = "#008676", CommandId = "mainMenu" }
        };
        }

        [RelayCommand]
        private void Navigate(string commandId)
        {
            CurrentText = commandId switch
            {
                "mainMenu" => "Мы в главном меню",
                "orders" => "Мы в заказах",
                _ => $"Неизвестная команда: {commandId}"
            };
        }
    }
}
