using DMXamarin.Models;
using DMXamarin.ViewModels;
using DMXamarin.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DMXamarin.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;
        public ItemsPage()
        {
            //App.mediaElement = mE;
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}