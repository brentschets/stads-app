﻿namespace RESTAPI.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Store Store { get; set; }
    }
}
