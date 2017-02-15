using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using Xamarin.Forms;
using Microsoft.WindowsAzure.MobileServices;

namespace TaskManager
{
    public partial class App : Application
    {

        public static MobileServiceClient AzureClient { get; internal set; }


        public App()
        {
            InitializeComponent();
            MobileCenter.Start(typeof(Analytics), typeof(Crashes));
            AzureClient = new MobileServiceClient("https://yourguid.azurewebsites.net/");

            MainPage = new TaskManager.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
