using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealTimeTasks.Data;

namespace RealTimeTasks.Web.ViewModels
{
        public class SignupViewModel : User
    {
        public string Password { get; set; }
    }
}
