using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiberiaApp.Classes.Elements
{
    public partial class CardModel
    {
        string name;
        string price;
        string date;

        public CardModel(string name, string price, string date)
        {
            this.name = name;
            this.price = price;
            this.date = date;
        }
    }
}
