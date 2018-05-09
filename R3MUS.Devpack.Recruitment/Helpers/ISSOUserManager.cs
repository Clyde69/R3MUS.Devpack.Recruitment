using System.Threading.Tasks;
using R3MUS.Devpack.Recruitment.Models;

namespace R3MUS.Devpack.Recruitment.Helpers
{
    public interface ISSOUserManager
    {
        Task<SSOApplicationUser> FindByIdAsync(string userId);
    }
}