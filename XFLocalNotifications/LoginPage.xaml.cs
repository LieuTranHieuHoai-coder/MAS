using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFLocalNotifications.Model;
using XFLocalNotifications.Global;
using XFLocalNotifications.ApiController;
using Xamarin.Essentials;
using Newtonsoft.Json;

namespace XFLocalNotifications
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public FactoryAPI factoryAPI = new FactoryAPI();
        public LoginPage()
        {
            InitializeComponent();
            ShowFactories();
        }

       
        public async void ShowFactories()
        {
            PkFactory.ItemsSource = await factoryAPI.GetFactoriesAsync();
            PkFactory.SelectedIndex = 0;
        }

        private async void ButtonLogin_Clicked(object sender, EventArgs e)
        {
            if (CheckInternet())
            {
                if ((EntryUser.Text == null && EntryUser.Text == "") || (EntryPass.Text == null && EntryPass.Text == ""))
                {
                    await DisplayAlert("Msg", "Thông tin đăng nhập không đúng", "OK");
                    App.Current.MainPage = new LoginPage();
                }
                
                else
                {
                    var client = new HttpClient();
                    var select = PkFactory.SelectedItem as Factory;
                    Global.Global.factoryName = select.Name;
                    HttpResponseMessage response = await client.GetAsync(Global.Global.apiURL + "getUsername&" + "id_user=" +
                        EntryUser.Text + "&id_pass=" +
                        EntryPass.Text + "&id_fact=" +
                        select.ID_Factory
                        ); 
                    string result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        string data = JsonConvert.DeserializeObject<string>(result);
                        if (data.Equals("loginFail"))
                        {
                            await DisplayAlert("Msg", "Tên đăng nhập hoặc mật khẩu không đúng", "OK");
                            App.Current.MainPage = new LoginPage();
                        }
                        if (data.Equals("loginSuccess"))
                        {
                            Global.Global.globalFactory = select.ID_Factory;
                            await Navigation.PushModalAsync(new MainPage());
                        }
                        //else
                        //{
                        //    await DisplayAlert("Msg", "Lỗi đăng nhập", "OK");
                        //}
                        
                    }
                    else
                    {
                        await DisplayAlert("Msg", "Lỗi nối đến máy chủ", "OK");
                        App.Current.MainPage = new LoginPage();
                    }
                }
            }
            else
            {
                _ = DisplayAlert("Msg", "No Internet, pls connect!. Chưa kết nối Internet!", "OK");
                return;
            }
        }
        

        private void PkFactory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global.Global.globalFactory = PkFactory.SelectedItem.ToString();
        }


        private bool CheckInternet()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                return false;
            }

            return true;
        }

    }
}