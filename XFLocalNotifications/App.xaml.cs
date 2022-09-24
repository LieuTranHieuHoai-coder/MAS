using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


[assembly: ExportFont("MaterialIconsOutlinedRegular.otf", Alias = "MaterialIconsOutlinedRegular")]
[assembly: ExportFont("MaterialIconsRegular.ttf", Alias = "MaterialIconsRegular")]

namespace XFLocalNotifications
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
            //MainPage = new LoginPage();
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
    }
    
}
