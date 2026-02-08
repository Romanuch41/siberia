using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SiberiaApp.Classes.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SiberiaApp.Classes.ViewModels
{
    public partial class MainMenuViewModels : ObservableObject
    {
        public ObservableCollection<ButtonViewModel> CollectionsButton { get; set; }
        public ObservableCollection<CardViuweModel> CollectionsCards { get; set; }
        [ObservableProperty]
        private ButtonViewModel addButton;
        [ObservableProperty]
        private string titlePage;
        public ICommand ChangeSectionCommand { get; set; }
        [ObservableProperty]
        private MainButtonId currentSelection;

        private GoogleDriveService googleDriveService;
        private GoogleSheetsService googleSheetsService;
        private TableManager tableManager;
        public ICommand Navigate { get; }


        public MainMenuViewModels()
        {
            currentSelection = MainButtonId.MainMenu;
            titlePage = "Главное меню";

            Debug.WriteLine($"Title = {TitlePage} Debug.WriteLine");
            ChangeSectionCommand = new RelayCommand<MainButtonId>(OnChangeSelection);
            CollectionsButton = new ObservableCollection<ButtonViewModel>{
                ButtonViewModel.ButtonMainMenu("Раздел заказов", MainButtonId.Orders),
                ButtonViewModel.ButtonMainMenu("Заказы в производство", MainButtonId.CreateForOrders),
                ButtonViewModel.ButtonMainMenu("Журналы", MainButtonId.Jornals),
                ButtonViewModel.ButtonMainMenu("Номенклатура", MainButtonId.DataProducts)
            };
            Debug.WriteLine($"Количество созданных кнопок {CollectionsButton.Count}");
            CollectionsCards = new ObservableCollection<CardViuweModel>();
            Navigate = new RelayCommand<string>(CommandNavigate);
        }

        private void OnChangeSelection(MainButtonId id)
        {
            CurrentSelection = id;
        }

        private async Task ShowReports(string commandId)
        {
            CollectionsButton.Clear();
            CollectionsCards.Clear();
            TitlePage = "Отчеты";
            AddButton = ButtonViewModel.ButtonAddReport("+", MainButtonId.AddReport);
            using var googleDriveStream = await FileSystem.OpenAppPackageFileAsync("service.json");
            using var googleSheetsStream = await FileSystem.OpenAppPackageFileAsync("service.json");
            using var tableStream = await FileSystem.OpenAppPackageFileAsync("tablecontent.json");
            googleDriveService ??= new GoogleDriveService(googleDriveStream);
            googleSheetsService ??= new GoogleSheetsService(googleSheetsStream);
            tableManager ??= new TableManager(googleSheetsService, tableStream);
            var table = await tableManager.ReadTableAsync(commandId);
            Debug.WriteLine("Получил таблицу");
            for (int i = 1; i < 31; i++)
            {
                var row = table[i];
                if (row.Count < 4)
                    continue;

                CardDataModel dataCards = new CardDataModel
                (
                    row[1]?.ToString() ?? "not data",
                    row[2]?.ToString() ?? "not data",
                    row[3]?.ToString() ?? "not data"
                );
                CollectionsCards.Add(new CardViuweModel(dataCards));
            }
        }

        private void ShowMainMenu()
        {
            TitlePage = "Главное меню";
            AddButton = null;
            CollectionsButton.Clear();
            CollectionsCards.Clear();
            CollectionsButton.Add(ButtonViewModel.ButtonMainMenu("Раздел заказов", MainButtonId.Orders));
            CollectionsButton.Add(ButtonViewModel.ButtonMainMenu("Заказы в производство", MainButtonId.CreateForOrders));
            CollectionsButton.Add(ButtonViewModel.ButtonMainMenu("Журналы", MainButtonId.Jornals));
            CollectionsButton.Add(ButtonViewModel.ButtonMainMenu("Номенклатура", MainButtonId.DataProducts));
        }

        private async Task AddReport()
        {

        }

        private void CommandNavigate(string key)
        {
            switch (key)
            {
                case "Reports":
                    Debug.WriteLine("Получена команда с кнопки отчеты");
                    ShowReports(key);
                    break;
                case "MainMenu":
                    Debug.WriteLine("Получена команда перехода на главное меню");
                    ShowMainMenu();
                    break;
            }
        }
    }
}
