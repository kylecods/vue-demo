namespace vue_api.Services
{
    public interface ITokenService
    {
        string GenerateToken(string username, bool isAdmin = false);
    }
}
