using System;
using System.Collections.Generic;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using XFLocalNotifications.Model;
using XFLocalNotifications.ApiController;
using System.Threading.Tasks;
using XFLocalNotifications.ViewModels;
using Plugin.SimpleAudioPlayer;
using System.IO;
using System.Reflection;
using Plugin.LocalNotification;
using Android;

namespace XFLocalNotifications
{

    public partial class MainPage : ContentPage
    {
        public string getFac { get; set; }
        public int count = 0;
        public MainPageViewModel pushAlert = new MainPageViewModel();
        public MachineAPI machineAlertAPI = new MachineAPI();
        

        public MainPage()
        {
            InitializeComponent();
            ShowMachineAlert();
            if (Global.Global.globalFactory != null)
            {
                FactoryName.Text = Global.Global.factoryName;
            }
            Device.StartTimer(new TimeSpan(0, 0, 5), () =>
             {
                 Device.BeginInvokeOnMainThread(() => ShowMachineAlert());
                 return true;
             });
        }

        public MainPage(string status)
        {
            InitializeComponent();
            if (status.Contains("wasUpdate"))
            {
                PlaySound("bell.mp3");
                //DisplayAlert("Thông báo", "Xác nhận sửa xong", "OK");
            }
            else
            {
                PlaySound("beep.mp3");
                //DisplayAlert("Thông báo", "Đã sửa chữa", "OK");
            }
            ShowMachineAlert();
            if (Global.Global.globalFactory != null)
            {
                FactoryName.Text = Global.Global.factoryName;
            }
            Device.StartTimer(new TimeSpan(0, 0, 5), () =>
            {
                Device.BeginInvokeOnMainThread(() => ShowMachineAlert());
                return true;
            });
        }
        // show data
        public async void ShowMachineAlert()
        {
            var list = await machineAlertAPI.GetMachineAlertAsync();
            if (Global.Global.countItems == null)
            {
                Global.Global.countItems = 0;
                if (list == null)
                {
                    Global.Global.countItems = 0;
                    Alert.Text = "Hiện không có máy hư, ngồi im uống nước";
                    MachineList.ItemsSource = list;
                }
                else
                { 
                    Alert.Text = list.Count.ToString() + " máy hỏng";
                    MachineList.ItemsSource = list;

                    Global.Global.countItems = list.Count;
                    PushNotification(list[list.Count - 1].NameLine + " " + list[list.Count - 1].ButtonName);
                    //var stream = GetStreamFromFile("bell.mp3");
                    //var audio = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                    //audio.Load(stream);
                    //audio.Play();
                }
            }
            else
            {
                if (list == null)
                {
                    Global.Global.countItems = 0;
                    Alert.Text = "Hiện không có máy hư, ngồi im xơi nước";
                    MachineList.ItemsSource = list;
                }
                else
                {
                    if (list.Count > Global.Global.countItems)
                    {
                        Alert.Text = list.Count.ToString() + " máy hỏng";
                        MachineList.ItemsSource = list;

                        Global.Global.countItems = list.Count;
                        PushNotification(list[list.Count - 1].NameLine + " " + list[list.Count - 1].ButtonName);
                        //var stream = GetStreamFromFile("bell.mp3");
                        //var audio = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                        //audio.Load(stream);
                        //audio.Play();
                    }
                    else
                    {
                        Alert.Text = list.Count.ToString() + " máy hỏng";
                        MachineList.ItemsSource = list;
                        Global.Global.countItems = list.Count;
                    }
                    

                }
            }
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
        //log out
        public async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage());
        }
        //scan barcode
        private async void ScanButton_Clicked(object sender, EventArgs e)
        {
            if (sender is Frame frame)
            {

                MachineList.SelectedItem = frame.BindingContext;
                MachineAlert update = new MachineAlert();
                update = (MachineAlert)MachineList.SelectedItem;
                //var scan = new ZXingScannerPage();
                //await Navigation.PushModalAsync(scan);
                //scan.OnScanResult += (data) =>
                //{
                //    Device.BeginInvokeOnMainThread(async() =>
                //    {
                //        var result = await machineAlertAPI.ScanMachineAlertAsync(data.Text, update.ID_Line, update.ID_Factory);
                //        if (result.Contains("wasUpdate"))
                //        {
                //            App.Current.MainPage = new MainPage(result);
                //        }
                //        else
                //        {
                //            //await DisplayAlert("Thông báo", "Đã sửa chữa", "OK");
                //            //await Navigation.PushModalAsync(new MainPage(result));
                //            App.Current.MainPage = new MainPage(result);
                //        }

                //    });
                //};
                var action = await DisplayAlert("Thông báo", "Nhấn Yes để xác nhận máy sửa xong.", "Yes", "No");
                if (action)
                {
                    await machineAlertAPI.ScanMachineAlertAsync(update.ID_Button, update.ID_Line, update.ID_Factory);
                }
                //await DisplayAlert("Thông báo", "Đã sửa chữa", "OK");
            }
            
        }
        // Begin fix time button
        private async void StartButton_Clicked(object sender, EventArgs e)
        {
            if (sender is Frame frame)
            {
                
                MachineList.SelectedItem = frame.BindingContext;
                MachineAlert update = new MachineAlert();
                update = (MachineAlert)MachineList.SelectedItem;
                _ = await machineAlertAPI.UpdateMachineAlertAsync(update.ID_Button, update.ID_Line, update.ID_Factory);
                ShowMachineAlert();
                //if (update.BeginFixTime == "" || update.BeginFixTime == null)
                //{
                //    _ = await machineAlertAPI.UpdateMachineAlertAsync(update.ID_Button, update.ID_Line, update.ID_Factory);
                //    ShowMachineAlert();
                    
                //}
                //else
                //{
                //    await DisplayAlert("Thông báo", "Thời gian đã được đặt", "OK");
                //    ShowMachineAlert();
                //}
                
            }
           
        }
        // Auto Refresh data
        private async void refreshList_Refreshing(object sender, EventArgs e)
        {
            await Task.Delay(1000);
            ShowMachineAlert();
            refreshList.IsRefreshing = false;
        }
        // Get FILE .mp3
        private Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;

            var stream = assembly.GetManifestResourceStream("XFLocalNotifications.Sounds." + filename);

            return stream;
        }

        public void PlaySound(string soundName)
        {
            var stream = GetStreamFromFile(soundName);
            var audio = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            audio.Load(stream);
            audio.Play();
        }
    }
}
