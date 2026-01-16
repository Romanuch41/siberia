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
        public string TitlePage { get; set; }
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
            TitlePage = "Главное меню";

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

        private void ShowReports(string commandId)
        {
            CollectionsButton.Clear();
            googleDriveService = new GoogleDriveService(Path.Combine(AppContext.BaseDirectory, "json", "auth", "client.json"));
            googleSheetsService = new GoogleSheetsService(Path.Combine(AppContext.BaseDirectory, "json", "auth", "client.json"));
            tableManager = new TableManager(googleSheetsService);
            var table = tableManager.ReadTableAsync(commandId).Result;
            List<(object, object, object)> result = new List<(object, object, object)> ();
            CardDataModel[] dataCards = new CardDataModel[table.Count];
            for (int i = 0; i < table.Count; i++)
            {
                var row = table[i];
                if (row.Count < 4)
                    continue;

                dataCards[i] = new CardDataModel
                (
                    row[1]?.ToString() ?? "not data",
                    row[2]?.ToString() ?? "not data",
                    row[3]?.ToString() ?? "not data"
                );
                CollectionsCards.Add(new CardViuweModel(dataCards[i]));
            }
        }

        private void CommandNavigate(string key)
        {
            switch (key)
            {
                case "Reports":
                    Debug.WriteLine("Получена команда с кнопки отчеты");
                    ShowReports(key);
                    break;
            }
        }
    }
}
