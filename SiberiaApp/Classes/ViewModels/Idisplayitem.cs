using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiberiaApp.Classes.ViewModels
{
    public interface Idisplayitem
    {
        DisplayType DisplayType { get; } // как показывать
        DataTemplate GetTemplate(); // что показывать
        int Itemcount { get; } // сколько элементов показывать
    }

    public enum DisplayType
    {
        Auto,           // Автовыбор (до 20 - Bindable, больше - Collection)
        BindableLayout, // Всегда BindableLayout
        CollectionView, // Всегда CollectionView
        Grid,           // Особый случай
        Custom          // Кастомный контрол
    }

    public abstract class DisplayModel : Idisplayitem
    {
        public abstract DisplayType DisplayType { get; set; }
        public abstract DataTemplate GetTemplate();
        public abstract int Itemcount { get; }
    }
}
