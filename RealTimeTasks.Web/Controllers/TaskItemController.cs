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
        private readonly IHubContext<TaskHub> _context;

        public TaskItemController(IConfiguration configuration, IHubContext<TaskHub> context)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
            _context = context;
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
            _context.Clients.All.SendAsync("taskadded", GetAllIncomplete());
        }

        [HttpPost]
        [Route("updatestatus")]
        public void UpdateStatus(UpdateStatusViewModel viewModel)
        {
            var user = GetCurrentUser();
            viewModel.UserId = user.Id;
            var repo = new TaskItemsRepository(_connectionString);
            repo.UpdateStatus(viewModel.TaskId, viewModel.Status, viewModel.UserId);
            _context.Clients.All.SendAsync("taskupdated", GetAllIncomplete());

       }

        private User GetCurrentUser()
        {
            var userRepo = new UserRepository(_connectionString);
            var user = userRepo.GetByEmail(User.Identity.Name);
            return user;
        }
    }
}
