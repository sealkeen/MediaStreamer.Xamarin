using MediaStreamer.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMXamarin.Models
{
    public static class CompositionExtension
    {
        public static Item ToItem(this Composition composition)
        {
            Item item = new Item();
            item.Id = composition.CompositionID.ToString();
            item.Text = composition.Artist.ArtistName;
            item.Description = composition.CompositionName;
            item.FilePath = composition.FilePath;
            return item;
        }
    }
}
