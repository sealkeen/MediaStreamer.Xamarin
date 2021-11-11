using DMXamarin.Services;
using DMXamarin.Views;
using MediaManager;
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
        public static MediaElement mediaElement;
        public App()
        {
            CrossMediaManager.Current.Init();
            InitializeComponent();
            mediaElement = new MediaElement();
            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        public static async Task<string> PickAndShow(PickOptions options)
        {
            try
            {
                var result = await FilePicker.PickAsync(options);
                if (result != null)
                {
                    //Text = $"File Name: {result.FileName}";
                    if (result.FileName.EndsWith("mp3", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("wav", StringComparison.OrdinalIgnoreCase))
                    {
                        //var stream = await result.OpenReadAsync();
                        //var mediaStream = new StreamMediaSource(stream);
                        //mediaStream.Stream = mediaStream;
                        //mediaElement.Source = mediaStream;
                        return result.FullPath;
                        mediaElement.Source = result.FileName;
                        bool exists = File.Exists(result.FullPath);
                        //FileData fileData = await CrossFilePicker.Current.PickFile();
                        //var result = new FileResult(fileData.FileName);
                        //if (fileData == null)
                        //    return result; // user canceled file picking
                        //mediaElement.Source = MediaSource.FromFile(fileData.FileName);
                        //mediaElement.Play();

                        mediaElement.Play();
                        //.FromStream(() => stream);
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
