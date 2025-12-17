using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using SiberiaApp.Classes.Elements;

namespace SiberiaApp.Classes.ViewModels
{
    class MenuDisplayModel : DisplayModel
    {
        public ObservableCollection<ButtonModel> ItemsButton { get; } = new();
        public ObservableCollection<CardModel> ItemsCard { get; } = new();
        public override DisplayType DisplayType { get; set; }
        public override int Itemcount => ItemsButton.Count;

        private MenuDisplayModel() { }

        public static MenuDisplayModel FromButtons(ObservableCollection<ButtonModel> buttons)
        {
            var menu = new MenuDisplayModel();
            foreach (var button in buttons)
            {
                menu.ItemsButton.Add(button);
            }
            menu.DisplayType = DisplayType.BindableLayout;
            return menu;
        }

        public static MenuDisplayModel FromCards(ObservableCollection<CardModel> cards)
        {
            var menu = new MenuDisplayModel();
            foreach (var card in cards)
            {
                menu.ItemsCard.Add(card);
            }
            menu.DisplayType = DisplayType.CollectionView;
            return menu;
        }

        public override DataTemplate GetTemplate()
        {
            return DisplayType switch
            {
                DisplayType.BindableLayout => App.Current?.Resources["ButtonTemplate"] as DataTemplate,
                DisplayType.CollectionView => App.Current?.Resources["CardTemplate"] as DataTemplate,
                DisplayType.Grid => App.Current?.Resources["GridTemplate"] as DataTemplate,
                DisplayType.Auto => Itemcount <= 20 ? App.Current?.Resources["ButtonTemplate"] as DataTemplate
                                                  : App.Current?.Resources["CardTemplate"] as DataTemplate,
                _ => throw new NotImplementedException()
            };
        }
    }
}
