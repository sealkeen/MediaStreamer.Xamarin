using DMXamarin.ViewModels;
using DMXamarin.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DMXamarin
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CompositionDetailPage), typeof(CompositionDetailPage));
            Routing.RegisterRoute(nameof(NewCompositionPage), typeof(NewCompositionPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
