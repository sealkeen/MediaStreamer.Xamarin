using DMXamarin.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace DMXamarin.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}