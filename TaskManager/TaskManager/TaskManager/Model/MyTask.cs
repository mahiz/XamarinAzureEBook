using System;
using Newtonsoft.Json;

namespace TaskManager.Model
{

    public class MyTask
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }
        [JsonProperty("updatedAt")]
        public DateTimeOffset UpdatedAt { get; set; }
        [JsonProperty("version")]
        public byte[] Version { get; set; }
        [JsonProperty("deleted")]
        public bool Deleted { get; set; }
        [JsonProperty("toDo")]
        public string ToDo { get; set; }
        [JsonProperty("when")]
        public DateTime When { get; set; }

        public MyTask()
        {
            this.Id = Guid.NewGuid().ToString("N");
            this.CreatedAt = DateTimeOffset.Now;
            this.UpdatedAt = DateTimeOffset.Now;
            this.When = DateTime.Now;
        }
    }
}
