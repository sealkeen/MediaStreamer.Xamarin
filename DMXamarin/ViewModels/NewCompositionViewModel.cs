using DMXamarin.Models;
using DMXamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DMXamarin.ViewModels
{
    public class NewCompositionViewModel : BaseViewModel
    {
        private string text;
        private string description;

        public NewCompositionViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var fileResult = await App.PickAndShow(Xamarin.Essentials.PickOptions.Default);
            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Text = Text,
                Description = fileResult,
                FilePath = fileResult
            };
            IDataStore<Item> DataStore = DependencyService.Get<IDataStore<Item>>();
            DataStore.AddItem(newItem);
            await Shell.Current.GoToAsync("..");
        }
    }
}
