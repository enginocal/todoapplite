using System;
using System.Collections.Generic;
using TodoAppLite.Common;
using TodoAppLite.Services;

namespace TodoAppLite.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Salt { get;  set; }
        public DateTime CreatedAt { get; set; }
        public List<TodoItem> TodoList { get; set; }

        //public User(string email, string name)
        //{
        //    if (string.IsNullOrEmpty(email))
        //    {
        //        throw new TodoException("empty_user_email", $"User email can not be empty.");
        //    }

        //    if (string.IsNullOrEmpty(name))
        //    {
        //        throw new TodoException("empty_user_name", $"User name can not be empty.");
        //    }

        //    Id = Guid.NewGuid();
        //    Email = email.ToLowerInvariant();
        //    Name = name;
        //    CreatedAt = DateTime.UtcNow;
        //}

        public void SetPassword(string password, IEncrypter encrypter)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new TodoException("empty_password", $"password cannot be empty");
            }
            Salt = encrypter.GetSalt();
            Password = encrypter.GetHash(password, Salt);
        }

        public bool ValidatePassword(string password,string salt, IEncrypter encrypter) =>
            Password.Equals(encrypter.GetHash(password, salt));

    }
}
