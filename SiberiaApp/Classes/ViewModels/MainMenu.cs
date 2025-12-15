using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiberiaApp.Classes.Elements;

namespace SiberiaApp.Classes.ViewModels
{
    public partial class MainMenu : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<ButtonModel> buttons = new();

        public MainMenu()
        {
            buttons.Add(new ButtonModel
            {
                Title = "Заказы"
            });
            buttons.Add(new ButtonModel
            {
                Title = "Заказы в производство"
            });
            buttons.Add(new ButtonModel
            {
                Title = "Журналы"
            });
            buttons.Add(new ButtonModel
            {
                Title = "Номенклатура"
            });
        }

    }
}
