using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealTimeTasks.Data
{
    public class TaskItemsRepository
    {
        private readonly string _connectionString;

        public TaskItemsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(string title, int userId)
        {
            TaskItem task = new TaskItem
            {
                Title = title,
                Status = 0,
                UserId = userId
            };

            using var context = new TaskItemsContext(_connectionString);
            context.Tasks.Add(task);
            context.SaveChanges();
        }

        public List<TaskItem> GetAllIncomplete()
        {
            using var context = new TaskItemsContext(_connectionString);
            return context.Tasks.Where(t => t.Status != Status.Completed).ToList();
        }

        public void UpdateStatus(int taskId, Status status, int userId)
        {
            using var context = new TaskItemsContext(_connectionString);
            context.Database.ExecuteSqlInterpolated($"UPDATE Tasks SET Status = {status}, UserId = {userId} WHERE Id = {taskId}");
        }

    }
}

