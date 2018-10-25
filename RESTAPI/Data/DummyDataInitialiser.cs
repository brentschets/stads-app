using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RESTAPI.Models;

namespace RESTAPI.Data
{
    public class DummyDataInitialiser
    {
        private readonly RESTAPIContext _context;

        public DummyDataInitialiser(RESTAPIContext context)
        {
            _context = context;
        }

        public void InitialiseData()
        {
            if (_context.Database.EnsureCreated())
            {
                List<Store> stores = new List<Store>
                {
                    new Store{Name = "Mister Spaghetti"},
                    new Store{Name = "Lennert's Pleasure Palace"},
                    new Store{Name = "Winkel nr. 3"},
                    new Store{Name = "Jozef's Spijker Emporium"},
                    new Store{Name = "Winkel nr. 5"}
                };
                _context.Store.AddRange(stores);

                _context.SaveChanges();
            }
        }
    }
}
