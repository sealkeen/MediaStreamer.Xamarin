using DMXamarin.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace DMXamarin.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel detailView;
        public ItemDetailPage()
        {
            InitializeComponent();
            //detailView = new ItemDetailViewModel();
            BindingContext = new ItemDetailViewModel();
        }

        private void mediaElement_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //((MediaElement)sender).Source = e.PropertyName;
        }

        private void btnPlay_Clicked(object sender, EventArgs e)
        {
            try
            {
                mediaElement.Source = detailView.Description;
            }
            catch (Exception ex) { }
        }
    }
}