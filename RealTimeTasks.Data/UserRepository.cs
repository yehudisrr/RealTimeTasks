﻿using System.Linq;

namespace RealTimeTasks.Data
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(User user, string password)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);

            using var context = new TaskItemsContext(_connectionString);
            context.Users.Add(user);
            context.SaveChanges();
        }

        public User GetByEmail(string email)
        {
            using var context = new TaskItemsContext(_connectionString);
            return context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User GetById(int id)
        {
            using var context = new TaskItemsContext(_connectionString);
            return context.Users.FirstOrDefault(u => u.Id == id);
        }

        public User Login(string email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }

            bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            return isCorrectPassword ? user : null;
        }
    }
}
