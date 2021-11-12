using DMXamarin.Models;
using MediaStreamer;
using MediaStreamer.DataAccess.NetStandard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DMXamarin.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>();
            foreach (var item in App.NetCoreDBRepository.DB.GetCompositions())
            {
                items.Add(item.ToItem());
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            if (File.Exists(item.Description))
            {
                App.FileManipulator.DecomposeAudioFile(item.Description);
                items.Add(item);
            }
            return await Task.FromResult(true);
        }

        public bool AddItem(Item item)
        {
            try
            {
                if (File.Exists(item.FilePath))
                {
                    App.FileManipulator.DecomposeAudioFile(item.FilePath);
                    item.Text = item.Artist?.ArtistName;
                    item.Description = item?.CompositionName;
                    items.Add(item);
                }
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            items.Clear();
            foreach (var item in App.NetCoreDBRepository.DB.GetCompositions())
            {
                items.Add(item.ToItem());
            }
            return await Task.FromResult(items);
        }
    }
}