using DMXamarin.Models;
using DMXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DMXamarin.Views
{
    public partial class NewCompositionPage : ContentPage
    {
        public Item Item { get; set; }

        public NewCompositionPage()
        {
            InitializeComponent();
            BindingContext = new NewCompositionViewModel();
        }
    }
}