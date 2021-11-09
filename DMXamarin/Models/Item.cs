using System;
using MediaStreamer.Domain;

namespace DMXamarin.Models
{
    public class Item : Composition
    {
        public string Id { get;set; }
        public string Text { get; set; }
        public string Description { get; set; }
    }
}