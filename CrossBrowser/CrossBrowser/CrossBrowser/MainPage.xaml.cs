using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CrossBrowser
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }

        private async void GoButton_Clicked(object sender, EventArgs e)
        {
            if(Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
                if (!string.IsNullOrEmpty(this.WebEntry.Text) &&
                    this.WebEntry.Text.ToLower().StartsWith("http://"))

                    this.WebBrowser.Source = this.WebEntry.Text;
                else
                    await DisplayAlert("Error", "URL is incorrect", "OK");
            }
        }

        private void WebBrowser_Navigating(object sender, WebNavigatingEventArgs e)
        {
            this.Indicator.IsRunning = true;
        }
        private void WebBrowser_Navigated(object sender, WebNavigatedEventArgs e)
        {
            this.Indicator.IsRunning = false;
        }
    }
}
