using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using RealTimeTasks.Data;
using RealTimeTasks.Web.ViewModels;
using System.Collections.Generic;

namespace RealTimeTasks.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskItemController : ControllerBase
    {
        private readonly string _connectionString;

        public TaskItemController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

    
        [HttpGet]
        [Route("getallincomplete")]
        public List<TaskItem> GetAllIncomplete()
        {
            var repo = new TaskItemsRepository(_connectionString);
            return repo.GetAllIncomplete();
        }


        [HttpPost]
        [Route("add")]
        public void Add(string title)
        {
            var repo = new TaskItemsRepository(_connectionString);
            var userId = GetCurrentUser().Id;
            repo.Add(title, userId);
        }

        [HttpPost]
        [Route("updatestatus")]
        public void UpdateStatus(UpdateStatusViewModel viewModel)
        {
            var repo = new TaskItemsRepository(_connectionString);
            repo.UpdateStatus(viewModel.TaskId, viewModel.Status, viewModel.UserId);
       }

        private User GetCurrentUser()
        {
            var userRepo = new UserRepository(_connectionString);
            var user = userRepo.GetByEmail(User.Identity.Name);
            return user;
        }
    }
}
