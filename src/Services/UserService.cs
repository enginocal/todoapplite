using System;
using TodoAppLite.Common;
using TodoAppLite.Common.Auth;
using TodoAppLite.Models;
using TodoAppLite.Repositories;

namespace TodoAppLite.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IEncrypter encyrpter;
        private readonly IJwtHandler jwtHandler;

        public UserService(IUserRepository userRepository, IEncrypter encyrpter, IJwtHandler jwtHandler)
        {
            this.userRepository = userRepository;
            this.encyrpter = encyrpter;
            this.jwtHandler = jwtHandler;
        }

        public JsonWebToken Login(LoginModel userInfo)
        {
            var user = userRepository.Get(CreateUserKey(userInfo.Email));
            if (user==null)
            {
                throw new TodoException("invalid_credentials", $"Invalid credentials");
            }

            if (!user.ValidatePassword(userInfo.Password,user.Salt,encyrpter))
            {
                throw new TodoException("invalid_credentials", $"Invalid credentials");
            }

            return jwtHandler.Create(user.Id);
        }

        public void Register(RegisterUser userInfo)
        {
            var user = userRepository.Get(CreateUserKey(userInfo.Email));
            if (user != null)
            {
                throw new TodoException("email_in_use", $"Email :'{userInfo.Email}' is already in use.");
            }
            user = new User(){
                Id=CreateUserKey(userInfo.Email),
                Email=userInfo.Email,
                Name=userInfo.Name,
                CreatedAt = DateTime.UtcNow
            };
            user.SetPassword(userInfo.Password, encyrpter);
            userRepository.AddUser(user);
        }

        private static string CreateUserKey(string email)
        {
            return $"user::{email}";
        }
    }
}
