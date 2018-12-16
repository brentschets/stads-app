using System.Collections.Generic;

namespace Stads_App.Models
{
    public class Store
    {
        public int StoreId { get; set; }
        public string Name { get; set; }
        public string ImgPath { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public List<Establishment> Establishments { get; set; }

        public bool EqualsForUpdate(Store store)
        {
            return store.StoreId == StoreId && store.Name == Name && store.Description == Description &&
                   store.Category.CategoryId == Category.CategoryId;
        }
    }
}