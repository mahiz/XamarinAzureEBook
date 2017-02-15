using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Model;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using TaskManager.Authentication;
using Microsoft.WindowsAzure.MobileServices;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace TaskManager.ViewModel
{
    public class MyTaskViewModel: INotifyPropertyChanged
    {

        public MyTaskViewModel()
        {
            IsUserAuthenticated = false;
        }

        private bool isUserAuthenticated;

        public bool IsUserAuthenticated
        {
            get
            {
                return isUserAuthenticated;
            }

            set
            {
                isUserAuthenticated = value; OnPropertyChanged();
            }
        }

        private string userId;

        public string UserId
        {
            get
            {
                return userId;
            }

            set
            {
                userId = value; OnPropertyChanged();
            }
        }

        private MyTask currentTask;
        public MyTask CurrentTask
        {
            get
            {
                return currentTask;
            }

            set
            {
                currentTask = value; OnPropertyChanged();
            }
        }

        private ObservableCollection<MyTask> myTasks;
        public ObservableCollection<MyTask> MyTasks
        {
            get
            {
                return myTasks;
            }

            set
            {
                myTasks = value; OnPropertyChanged();
            }
        }

        public async Task InitAsync()
        {
            await LoginAsync();

            if(IsUserAuthenticated == true)
            {
                if (CurrentTask == null) CurrentTask = new MyTask();
                MyTasks = await GetTasksAsync();
            }
        }

        public async Task SaveTaskAsync(MyTask newTask)
        {
            MyTasks.Add(newTask);

            var table = App.AzureClient.GetTable<MyTask>();
            await table.InsertAsync(newTask);
        }

        public async Task<ObservableCollection<MyTask>> GetTasksAsync()
        {
            var table = App.AzureClient.GetTable<MyTask>();
            var listOfTasks = await table.Where(t=>t.userId== UserId).ToListAsync();

            return new ObservableCollection<MyTask>(listOfTasks);
        }

        public async Task LoginAsync()
        {
            var authenticator = DependencyService.Get<IAuthentication>();
            var user = await authenticator.LoginAsync(App.AzureClient, MobileServiceAuthenticationProvider.MicrosoftAccount);
            if (user == null)
            {
                IsUserAuthenticated = false;
                return;
            }
            else
            {
                UserId = user.UserId;
                IsUserAuthenticated = true;
                return;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
