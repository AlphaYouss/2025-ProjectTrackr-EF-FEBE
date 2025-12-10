using WebApi.Models;

namespace WebApi.Tools.Interface
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}