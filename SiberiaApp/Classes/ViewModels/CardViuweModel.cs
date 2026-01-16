using Microsoft.Maui;
using SiberiaApp.Classes.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiberiaApp.Classes.ViewModels
{
    public class CardViuweModel
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public string User {  get; set; }
        public string Price { get; set; }
        public string Date { get; set; }
        public CornerRadius cornerRadius { get; set; }
        public string BackgroundColor { get; set; }
        public string TextColor { get; set; }

        public CardViuweModel(CardDataModel dataCard)
        {
            Width = 220;
            Height = 30;
            User = dataCard.user;
            Price = dataCard.price;
            Date = dataCard.date;
            cornerRadius = new CornerRadius(7);
            BackgroundColor = "#E2E2E2";
            TextColor = "#000000";
        }
    }
}
