using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiberiaApp.Classes.Elements
{
    public partial class Card
    {
        string name;
        string price;
        string date;

        public Card(string name, string price, string date)
        {
            this.name = name;
            this.price = price;
            this.date = date;
        }
    }
}
