using DMXamarin.Models;
using DMXamarin.ViewModels;
using MediaManager;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace DMXamarin.Views
{
    public partial class CompositionDetailPage : ContentPage
    {
        private Item _selectedItem;
        public CompositionDetailPage(Item selectedItem = null)
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
        private void btnPlayPause_Clicked(object sender, EventArgs e)
        {
            //btnPlayPause.Source = "Play_Pause_Selected.png";
            CrossMediaManager.Current.PlayPause();
        }

        private void btnPlayPause_Unfocused(object sender, FocusEventArgs e)
        {
            //btnPlayPause.Source = "Play_Pause_Unselected.png";
        }

        private void btnPlayPause_Released(object sender, EventArgs e)
        {
            //btnPlayPause.Source = "Play_Pause_Unselected.png";
        }

        private async void btnBrowseFile_Clicked(object sender, EventArgs e)
        {
            var fileResult = await App.PickAndShow(Xamarin.Essentials.PickOptions.Default);
            if (_selectedItem != null)
            {
                if (System.IO.File.Exists(fileResult))
                {
                    _selectedItem.FilePath = fileResult;
                    App.NetCoreDBRepository.ChangeFilename(_selectedItem, fileResult, App.SetStatusText);
                }
            }
        }
    }
}