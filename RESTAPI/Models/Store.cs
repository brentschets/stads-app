﻿namespace RESTAPI.Models
{
    public class Store
    {
        public int StoreId { get; set; }
        public string Name { get; set; }
        public string ImgPath { get; set; }
        public int Visited { get; set; }
        public string Description { get; set; }
        public int Event { get; set; }
        public int Promotion { get; set; }
    }
}
