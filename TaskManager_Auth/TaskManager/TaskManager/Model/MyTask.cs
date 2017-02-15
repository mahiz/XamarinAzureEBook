using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TaskManager.Model
{
    
    public class MyTask
    {
        public DateTime createdAt { get; set; }
        public byte[] version { get; set; }
        public bool deleted { get; set; }
        public string id { get; set; }
        public string userId { get; set; }
        public DateTime when { get; set; }
        public DateTime updatedAt { get; set; }
        public string toDo { get; set; }

        public MyTask()
        {
            this.id = Guid.NewGuid().ToString("N");
            this.createdAt = DateTime.Now;
            this.updatedAt = DateTime.Now;
            this.when = DateTime.Now;
        }
    }
}
