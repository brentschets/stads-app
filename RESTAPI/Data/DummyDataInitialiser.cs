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
                new Store {StoreId = 1, Name = "Mister Spaghetti", ImgPath = "https://stadsapprestapi.azurewebsites.net/img/0005.jpg", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas urna turpis, vestibulum at dolor quis, pretium interdum magna. Morbi ultricies, urna in convallis tincidunt, nibh felis laoreet mauris, et fermentum mauris nibh vitae arcu. Fusce vel nisl faucibus, lobortis urna nec, dictum diam. Vestibulum sodales erat sit amet tincidunt tempus. Praesent fringilla vel sem et hendrerit. Aliquam et justo mi. Nam semper justo lacinia ex tristique ornare. Aliquam blandit ante a nibh suscipit hendrerit eget sed ex. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Sed laoreet gravida nisi vulputate dignissim. Nam hendrerit justo nunc, vitae dignissim turpis vehicula ac."},
                new Store {StoreId = 2, Name = "Lennert's Pleasure Palace", ImgPath = "https://stadsapprestapi.azurewebsites.net/img/0012.jpg", Description = "Curabitur pulvinar laoreet lacus non viverra. Morbi tincidunt risus a nulla tempor, et varius ante tempus."},
                new Store {StoreId = 3, Name = "Winkel nr. 3", ImgPath = "https://stadsapprestapi.azurewebsites.net/img/0035.jpg", Description = "Morbi quam diam, iaculis eu hendrerit at, imperdiet sed augue."},
                new Store {StoreId = 4, Name = "Jozef's Spijker Emporium", ImgPath = "https://stadsapprestapi.azurewebsites.net/img/0012.jpg", Description = "Quisque quis augue arcu. Mauris pulvinar aliquet mi, varius euismod ante porttitor in. Sed imperdiet vitae augue vehicula vestibulum. Praesent lacinia nulla id risus elementum, id pellentesque ipsum dictum. Phasellus sit amet molestie dolor. Aliquam viverra turpis eu quam euismod tempor. Phasellus at risus interdum, laoreet erat at, sollicitudin eros."},
                new Store {StoreId = 5, Name = "Winkel nr. 5", ImgPath = "https://stadsapprestapi.azurewebsites.net/img/0005.jpg", Description = "Maecenas sit amet dolor et libero dignissim mattis in malesuada magna. Suspendisse eu pellentesque metus, sed blandit augue. Nunc venenatis ligula nunc, non placerat libero tincidunt vitae. Nullam a sapien turpis. Mauris sit amet enim in elit blandit mollis. Nulla ac massa vitae sapien iaculis aliquet in eget odio. Sed rutrum mauris id sem aliquet imperdiet."},
                new Store {StoreId = 6, Name = "De Zesde Winkel", ImgPath = "https://stadsapprestapi.azurewebsites.net/img/0035.jpg", Description = "Duis egestas sapien odio, sit amet scelerisque quam pellentesque ac."}
            };
            foreach (var store in stores)
            {
                var dbStore = _context.Store.Find(store.StoreId);
                if (dbStore == null) _context.Store.Add(store);
            }

            _context.Database.BeginTransaction();
            _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Store ON");
            _context.SaveChanges();
            _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Store OFF");
            _context.Database.CommitTransaction();
        }
    }
}