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
        public double cornerRadius { get; set; }
        public string BackgroundColor { get; set; }
        public string TextColor { get; set; }
        public int RowUser { get; set; }
        public int ColUser { get; set; }
        public int RowDate { get; set; }
        public int ColDate { get; set; }
        public int RowPrice { get; set; }
        public int ColPrice { get; set; }

        public CardViuweModel(CardDataModel dataCard)
        {
            Width = 250;
            Height = 120;
            User = dataCard.user;
            Price = dataCard.price;
            Date = dataCard.date;
            cornerRadius = 7;
            BackgroundColor = "#009886";
            TextColor = "#FFFFFF";
            RowUser = 0;
            ColUser = 0;
            RowDate = 1;
            ColDate = 0;
            RowPrice = 1;
            ColPrice = 1;
        }
    }
}
