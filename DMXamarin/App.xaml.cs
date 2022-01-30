using DMXamarin.Models;
using DMXamarin.Services;
using DMXamarin.Views;
using MediaManager;
using MediaStreamer;
using MediaStreamer.Domain;
using MediaStreamer.IO;
//using Plugin.FilePicker;
//using Plugin.FilePicker.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Core;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DMXamarin
{
    public partial class App : Application
    {
        public static Label StatusLabel;
        public static FileManipulator FileManipulator { get; set; }
        public static DBRepository NetCoreDBRepository;
        public App()
        {
            InitializeComponent();
            CrossMediaManager.Current.Init();
            DependencyService.Register<CompositionStore>();
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            SetStatusText("Loading Database in progress...");
            await Task.Run(() =>
           FileManipulator = new FileManipulator(NetCoreDBRepository = new DBRepository() { DB = new DMEntities() })
            );
            SetStatusText("Database has just been loaded.");
        }

        public static void SetStatusText(string text)
        {
            if (StatusLabel != null)
                StatusLabel.Text = text;
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static async Task<bool> OpenFileAndSaveToStorage()
        {
            var fileResult = await App.PickAndShow(Xamarin.Essentials.PickOptions.Default);
            if (fileResult == null)
                return false;
            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Text = "New Song",
                Description = fileResult,
                FilePath = fileResult
            };
            IDataStore<Item> DataStore = DependencyService.Get<IDataStore<Item>>();
            DataStore.AddItem(newItem);
            return true;
        }

        public static async Task<string> PickAndShow(PickOptions options)
        {
            try
            {
                var result = await FilePicker.PickAsync(options);
                if (result != null)
                {
                    if (result.FileName.EndsWith("mp3", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("wav", StringComparison.OrdinalIgnoreCase))
                    {

                        return result.FullPath;
                        bool exists = File.Exists(result.FullPath);
                    }
                }

                return result.FullPath;
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }

            return null;
        }
    }
}
