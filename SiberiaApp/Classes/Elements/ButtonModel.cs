using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SiberiaApp.Classes.Elements
{
    public partial class ButtonModel : ObservableObject
    {
        [ObservableProperty]
        private string title = string.Empty;
        [ObservableProperty]
        private string backgroundColor = "#008676";
        [ObservableProperty]
        private string commandId = string.Empty;

        public IRelayCommand? Command { get; set; }

        public Action? CommandAction { get; set; }

    }
}
