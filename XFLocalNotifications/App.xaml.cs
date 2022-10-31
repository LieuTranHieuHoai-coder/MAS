using Plugin.LocalNotification;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFLocalNotifications.ApiController;

[assembly: ExportFont("MaterialIconsOutlinedRegular.otf", Alias = "MaterialIconsOutlinedRegular")]
[assembly: ExportFont("MaterialIconsRegular.ttf", Alias = "MaterialIconsRegular")]

namespace XFLocalNotifications
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new LoginPage();
        }
        
        protected override void OnStart()
        {
        }
        public MachineAPI machineAlertAPI = new MachineAPI();
        protected override void OnSleep()
        {
            Device.StartTimer(new TimeSpan(0, 0, 5), () =>
            {
                Device.BeginInvokeOnMainThread(() => ShowMachineAlert());
                return true;
            });
        }

        protected override void OnResume()
        {
        }

        public async void ShowMachineAlert()
        {
            var list = await machineAlertAPI.GetMachineAlertAsync();
            if (Global.Global.countItems == null)
            {
                Global.Global.countItems = 0;
                if (list == null)
                {
                    Global.Global.countItems = 0;

                }
                else
                {
                    //var stream = GetStreamFromFile("bell.mp3");
                    //var audio = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                    //audio.Load(stream);
                    //audio.Play();
                    Global.Global.countItems = list.Count;
                }
            }
            else
            {
                if (list == null)
                {
                    Global.Global.countItems = 0;

                }
                else
                {
                    if (list.Count > Global.Global.countItems)
                    {
                        //var stream = GetStreamFromFile("bell.mp3");
                        //var audio = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                        //audio.Load(stream);
                        //audio.Play();
                        Global.Global.countItems = list.Count;
                    }
                    else
                    {

                        Global.Global.countItems = list.Count;
                    }
                }
            }
        }
        private Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;

            var stream = assembly.GetManifestResourceStream("XFLocalNotifications.Sounds." + filename);

            return stream;
        }
        //push notification 
        public void PushNotification(string note)
        {
            var notification = new NotificationRequest
            {
                BadgeNumber = 1,
                Description = note,
                Title = "Notification",
                NotificationId = 1337

            };
            LocalNotificationCenter.Current.Show(notification);
        }
    }
    
}
