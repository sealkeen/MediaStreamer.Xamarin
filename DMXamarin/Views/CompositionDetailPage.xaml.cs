using DMXamarin.ViewModels;
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
    }
}