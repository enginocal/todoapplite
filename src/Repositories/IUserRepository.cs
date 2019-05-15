using System;
using TodoAppLite.Models;

namespace TodoAppLite.Repositories
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User Get(string email);
        User Get(Guid id);
    }
}
