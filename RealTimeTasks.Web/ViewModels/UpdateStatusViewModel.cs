using RealTimeTasks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeTasks.Web.ViewModels
{
    public class UpdateStatusViewModel
    {
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public Status Status { get; set; }
    }
}
