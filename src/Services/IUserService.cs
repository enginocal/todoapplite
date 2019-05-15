using TodoAppLite.Common.Auth;
using TodoAppLite.Models;

namespace TodoAppLite.Services
{
    public interface IUserService
    {
        void Register(RegisterUser user);
        JsonWebToken Login(LoginModel userInfo);
    }
}
