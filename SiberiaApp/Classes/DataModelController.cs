using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiberiaApp.Classes.Elements;
using System.Collections.ObjectModel;

namespace SiberiaApp.Classes
{
    public partial class DataModelController
    {
        public readonly ObservableCollection<ButtonModel> buttons = new();
        public readonly ObservableCollection<CardModel> cards = new();
        private ServiceGoogleDrive googledrive;
        private GoogleSheetsService googleService;
        private GoogleSheetReader googlereader;
        private TableManager tableManager;

        public DataModelController() 
        {
            googledrive = new ServiceGoogleDrive();
            googleService = new GoogleSheetsService();
            googleService.InitializeSheetsServiceAsync();
            googlereader = new GoogleSheetReader(googleService.GetSheetsService());
            tableManager = new TableManager();
        }

        public void MainMenu()
        {
            buttons.Clear();
            cards.Clear();
            buttons.Add(new ButtonModel { Title = "Заказы", CommandId = "order" });
            buttons.Add(new ButtonModel { Title = "Журналы", CommandId = "jornal" });
            buttons.Add(new ButtonModel { Title = "Номенклатура", CommandId = "products" });
        }

        public void Reports()
        {
            buttons.Clear();
            cards.Clear();
            tableManager.GetTables("Reports");
            googlereader.ReadLast30DaysAsync(tableManager.tables.sheets[0].name, "Лист1", 4);
        }
    }
}
