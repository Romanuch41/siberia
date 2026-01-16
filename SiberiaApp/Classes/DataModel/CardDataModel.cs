using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiberiaApp.Classes.DataModel
{
     public class CardDataModel
    {
        public string user { get; set; }
        public string price { get; set; }
        public string date {  get; set; }

        public CardDataModel(string user, string price, string date)
        {
            this.user = user;
            this.price = price;
            this.date = date;
        }
    }
}
