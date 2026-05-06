using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//User будет использоваться для работы с данными пользователей

namespace SiberiaApp.Classes
{
    public class User
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string TitleJob { get; set; }

        public User(string name, string phone, string titleJob)
        {
            Name = name;
            Phone = phone;
            TitleJob = titleJob;
        }
    }
}
