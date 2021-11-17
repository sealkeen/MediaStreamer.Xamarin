using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DMXamarin.ViewModels;
using MediaManager;

namespace DMXamarin.Views
{
    public partial class AboutPage : ContentPage
    {
        CompositionsViewModel _viewModel;
        public AboutPage()
        {
            InitializeComponent();
            refreshView.BindingContext = _viewModel = new CompositionsViewModel();
            //contentView.Content = DMXamarin.Extensions.ContentPageToView.Convert(compositions);
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