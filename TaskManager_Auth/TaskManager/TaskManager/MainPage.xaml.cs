using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Authentication;
using TaskManager.Model;
using TaskManager.ViewModel;
using Xamarin.Forms;

namespace TaskManager
{
    public partial class MainPage : ContentPage
    {

        private MyTaskViewModel ViewModel { get; set; }

        public MainPage()
        {
            InitializeComponent();

            this.TaskEntry.IsEnabled = false;
            this.TaskDate.IsEnabled = false;
            this.ViewModel = new MyTaskViewModel();
        }

        private void ManageIndicator()
        {
            this.Indicator1.IsVisible = !this.Indicator1.IsVisible;
            this.Indicator1.IsRunning = !this.Indicator1.IsRunning;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            ManageIndicator();
            if(this.ViewModel.IsUserAuthenticated==true)
            {
                await this.ViewModel.InitAsync();
            }
            this.BindingContext = this.ViewModel;
            ManageIndicator();
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            ManageIndicator();
            await this.ViewModel.SaveTaskAsync(this.ViewModel.CurrentTask);
            this.BindingContext = this.ViewModel;
            ManageIndicator();
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            await this.ViewModel.LoginAsync();
            if(this.ViewModel.IsUserAuthenticated==true)
            {
                await this.ViewModel.InitAsync();
                this.TaskEntry.IsEnabled = true;
                this.TaskDate.IsEnabled = true;
                this.BindingContext = this.ViewModel;
            }
        }
    }
}
