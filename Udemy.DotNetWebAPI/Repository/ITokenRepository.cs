using Microsoft.AspNetCore.Identity;

namespace Udemy.DotNetWebAPI.Repositories
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser userm, List<string> roles);

    }
}
