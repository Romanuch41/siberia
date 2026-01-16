using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SiberiaApp.Classes.ViewModels
{
    public enum MainButtonId
    {
        Orders,
        CreateForOrders,
        Jornals,
        DataProducts,
        MainMenu
    }
    public class ButtonViewModel
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double CornerRadius { get; set; }

        public string BackgroundColor { get; set; }
        public string Color { get; set; }
        public string Title { get; set; }
        public MainButtonId Id { get; set; }

        public ButtonViewModel(double width, double height, double  cornerRadius, string backGroundColor, string color, string title, MainButtonId id)
        {
            Width = width;
            Height = height;
            CornerRadius = cornerRadius;
            BackgroundColor = backGroundColor;
            Color = color;
            Title = title;
            Id = id;
        }

        public static ButtonViewModel ButtonMainMenu(string title, MainButtonId id)
        {
            return new ButtonViewModel(190, 30, 5, "#009886", "#FFFFFF", title, id);
        }


    }
}
