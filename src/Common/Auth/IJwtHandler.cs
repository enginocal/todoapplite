namespace TodoAppLite.Common.Auth
{
    public interface IJwtHandler
    {
        JsonWebToken Create(string userId);
    }
}
