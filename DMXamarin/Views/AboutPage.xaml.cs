using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DMXamarin.ViewModels;
using MediaManager;
using System.Threading.Tasks;

namespace DMXamarin.Views
{
    public partial class AboutPage : ContentPage
    {
        CompositionsViewModel _viewModel;
        public AboutPage()
        {
            InitializeComponent();
            App.StatusLabel = lblStatus;
            //Task.Run(() => InitializeViewModel());
            Task.Run(() => refreshView.BindingContext = _viewModel = new CompositionsViewModel());
            //contentView.Content = DMXamarin.Extensions.ContentPageToView.Convert(compositions);
        }

        public async Task InitializeViewModel()
        {
            var task = Task.Run(() => refreshView.BindingContext = _viewModel = new CompositionsViewModel());
            await task;
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

        private void btnCompositions_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}