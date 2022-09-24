using System;
using System.Collections.Generic;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using XFLocalNotifications.Model;
using XFLocalNotifications.ApiController;

namespace XFLocalNotifications
{

    public partial class MainPage : ContentPage
    {
        public string getFac { get; set; }
        public MachineAPI machineAlertAPI = new MachineAPI();
        public MainPage()
        {
            InitializeComponent();
            ShowMachineAlert();
            if (Global.Global.globalFactory != null)
            {
                FactoryName.Text = Global.Global.factoryName;
            }

        }

        public MainPage(string code)
        {
            InitializeComponent();
            ShowMachineAlert();
        }
       
        public async void ShowMachineAlert()
        {
            if (await machineAlertAPI.GetMachineAlertAsync()==null)
            {
                Alert.Text = "Hiện không có máy hư, nghỉ làm";
            }
            else
            {
                //Alert.IsVisible = false;
                MachineList.ItemsSource = await machineAlertAPI.GetMachineAlertAsync();
            }
           
        }
        public async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage());
        }

        private void ScanButton_Clicked(object sender, EventArgs e)
        {
            var scan = new ZXingScannerPage();
            Navigation.PushModalAsync(scan);
            scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(() =>
               {
                   Navigation.PushModalAsync(new MainPage(result.Text));
                   //Global.Global.test = result.Text;

               });
            };
        }
       
    }
}
