using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Model;
using System.Collections.ObjectModel;
using Microsoft.WindowsAzure.MobileServices;

namespace TaskManager.ViewModel
{
    public class MyTaskViewModel
    {
        public MyTask CurrentTask { get; set; }
        public ObservableCollection<MyTask> MyTasks { get; set; }

        public async Task InitAsync()
        {
            if (this.CurrentTask == null) this.CurrentTask = new MyTask();

            this.MyTasks = await GetTasksAsync();
        }

        public async Task SaveTaskAsync(MyTask newTask)
        {
            this.MyTasks.Add(newTask);

            IMobileServiceTable<MyTask> table = App.AzureClient.GetTable<MyTask>();
            await table.InsertAsync(newTask);
        }

        public async Task<ObservableCollection<MyTask>> GetTasksAsync()
        {
            var table = App.AzureClient.GetTable<MyTask>();
            var listOfTasks = await table.ToListAsync();

            return new ObservableCollection<MyTask>(listOfTasks);
        }
    }
}
