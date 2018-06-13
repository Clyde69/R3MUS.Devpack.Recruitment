using System.Threading.Tasks;
using R3MUS.Devpack.Recruitment.Models;
using System.Security.Claims;

namespace R3MUS.Devpack.Recruitment.Helpers
{
    public interface ISSOUserManager
    {
        SSOApplicationUser FindByIdentity(ClaimsIdentity identity);
        Task<SSOApplicationUser> FindByIdAsync(string userId);
    }
}