using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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

        public void Initialise()
        {
            _context.Database.Migrate();
            var stores = new List<Store>
            {
                new Store {StoreId = 1, Name = "Mister Spaghetti", ImgPath = "https://stadsapprestapi.azurewebsites.net/img/0005.jpg"},
                new Store {StoreId = 2, Name = "Lennert's Pleasure Palace", ImgPath = "https://stadsapprestapi.azurewebsites.net/img/0012.jpg"},
                new Store {StoreId = 3, Name = "Winkel nr. 3", ImgPath = "https://stadsapprestapi.azurewebsites.net/img/0035.jpg"},
                new Store {StoreId = 4, Name = "Jozef's Spijker Emporium", ImgPath = "https://stadsapprestapi.azurewebsites.net/img/0012.jpg"},
                new Store {StoreId = 5, Name = "Winkel nr. 5", ImgPath = "https://stadsapprestapi.azurewebsites.net/img/0005.jpg"},
                new Store {StoreId = 6, Name = "De Zesde Winkel", ImgPath = "https://stadsapprestapi.azurewebsites.net/img/0035.jpg"}
            };
            foreach (var store in stores)
            {
                if (_context.Store.Find(store.StoreId) == null) _context.Store.Add(store);
            }

            _context.Database.BeginTransaction();
            _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Store ON");
            _context.SaveChanges();
            _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Store OFF");
            _context.Database.CommitTransaction();
        }
    }
}