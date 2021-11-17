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
    public class CompositionsViewModel : BaseViewModel
    {
        private Item _selectedComposition;
        public ObservableCollection<Item> Compositions { get; }
        public Command LoadCompositionsCommand { get; }
        public Command AddCompositionCommand { get; }
        public Command ClearCommand { get; }
        public Command<Item> CompositionTapped { get; }

        public CompositionsViewModel()
        {
            Title = "Browse";
            Compositions = new ObservableCollection<Item>();
            LoadCompositionsCommand = new Command(async () => await ExecuteLoadCompositionsCommand());

            CompositionTapped = new Command<Item>(OnCompositionSelected);

            AddCompositionCommand = new Command(OnAddComposition);
            ClearCommand = new Command(OnClear);
        }

        async Task ExecuteLoadCompositionsCommand()
        {
            IsBusy = true;

            try
            {
                Compositions.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Compositions.Add(item);
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
            get => _selectedComposition;
            set
            {
                SetProperty(ref _selectedComposition, value);
                OnCompositionSelected(value);
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

        private async void OnAddComposition(object obj)
        {
            await App.OpenFileAndSaveToStorage();
            await ExecuteLoadCompositionsCommand();
            //await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnCompositionSelected(Item item)
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
            //else
            //{
                //// This will push the ItemDetailPage onto the navigation stack
                await Shell.Current.GoToAsync($"{nameof(CompositionDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
            //}
        }
    }
}