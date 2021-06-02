using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using RealTimeTasks.Data;
using System;
using System.Collections.Generic;

public class TaskHub : Hub
{
    private readonly string _connectionString;
    public TaskHub(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("ConStr");
    }
    public void TaskUpdated()
    {
        var repo = new TaskItemsRepository(_connectionString); 
        Clients.All.SendAsync("taskUpdated", repo.GetAllIncomplete());
    }
}

