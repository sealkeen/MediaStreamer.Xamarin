using DMXamarin.Models;
using DMXamarin.Views;
using MediaManager;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Core;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace DMXamarin.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;
        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command ClearCommand { get; }
        public Command<Item> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
            ClearCommand = new Command(OnClear);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnClear(object obj)
        {
            //TODO: check if not throws exception
            await Task.Run(() => App.NetCoreDBRepository.DB.Clear());
            App.StatusLabel.Text = $"ArtistGenresCount: {App.NetCoreDBRepository.DB.GetArtistGenres().Count()}\n" +
                $"Artists count: {App.NetCoreDBRepository.DB.GetArtists().Count()}\n" +
                $"GroupMembers count : {App.NetCoreDBRepository.DB.GetGroupMembers().Count()}";

        }

        private async void OnAddItem(object obj)
        {
            await App.OpenFileAndSaveToStorage();
            await ExecuteLoadItemsCommand();
            //await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;
            if (File.Exists(item.FilePath))
            {
                //App.mediaElement = new MediaElement();
                if(item.FilePath != null)
                    await CrossMediaManager.Current.Play($"{item.FilePath}");
                //App.mediaElement.Source = MediaSource.FromFile(item.Description);
                //App.mediaElement.Play();
            }
            else
            {
                //// This will push the ItemDetailPage onto the navigation stack
                await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
            }
        }
    }
}