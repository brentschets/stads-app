﻿namespace RESTAPI.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Visited { get; set; }
        public Establishment Establishment { get; set; }
    }
}
