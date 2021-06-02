using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealTimeTasks.Data
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Status Status {get; set;}

        public int UserId { get; set; }


    }
}
