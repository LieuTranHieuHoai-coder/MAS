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
                   
                    PushNotification(list[list.Count - 1].NameLine + " " + list[list.Count - 1].ButtonName);
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
                        PushNotification(list[list.Count - 1].NameLine +" "+ list[list.Count - 1].ButtonName);
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
                Title = "Thông Báo",
                NotificationId = 1337,
                Android =
                {
                    IconSmallName = new Plugin.LocalNotification.AndroidOption.AndroidIcon("my_icon"),
                    IconLargeName = new Plugin.LocalNotification.AndroidOption.AndroidIcon("my_icon"),
                    AutoCancel = true,
                    //Ongoing = true
                }

            };
            LocalNotificationCenter.Current.Show(notification);
        }

    }
    
}
