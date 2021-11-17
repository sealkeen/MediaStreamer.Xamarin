using DMXamarin.ViewModels;
using MediaManager;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace DMXamarin.Views
{
    public partial class CompositionDetailPage : ContentPage
    {
        public CompositionDetailPage()
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
    }
}