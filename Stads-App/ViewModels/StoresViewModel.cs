using System.Collections.ObjectModel;
using Stads_App.Models;

namespace Stads_App.ViewModels
{
    public class StoresViewModel
    {
        public ObservableCollection<Store> Stores { get; }

        public StoresViewModel()
        {
            Stores = new ObservableCollection<Store>
            {
                new Store{Name = "Mister Spaghetti"},
                new Store{Name = "Lennert's Pleasure Palace"},
                new Store{Name = "Winkel 3"},
                new Store{Name = "Das Winkel"},
                new Store{Name = "Jozef's Spijker Emporium"}
            };
        }
    }
}