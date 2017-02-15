using System;
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
            await this.ViewModel.InitAsync();
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
    }
}
