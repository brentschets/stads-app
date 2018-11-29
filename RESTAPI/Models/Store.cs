using System.Collections.Generic;

namespace RESTAPI.Models
{
    public class Store
    {
        public int StoreId { get; set; }
        public string Name { get; set; }
        public string ImgPath { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public List<Establishment> Establishments { get; set; }
    }
}
