using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using RealTimeTasks.Data;
using System;
using System.Collections.Generic;

public class TaskHub : Hub
{
    //private static List<TaskItem> _tasks = new List<TaskItem>();

    public TaskHub(IConfiguration configuration)
    {
        configuration.GetConnectionString("ConStr");
    }

    //public void NewUser()
    //{
    //      //    var countMessage = new UserCountMessage { Count = _totalUsers };
    //    Clients.All.SendAsync("newUser", countMessage);
    //}

    //public void SendMessage(NewMessage newMessage)
    //{
    //    //Context.User.Identity.Name

    //    _messages.Add(newMessage.Message);
    //    Clients.All.SendAsync("newMessage", _messages);
    //}
}

