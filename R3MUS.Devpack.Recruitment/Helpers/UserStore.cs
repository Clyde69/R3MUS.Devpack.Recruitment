using Microsoft.AspNet.Identity;
using R3MUS.Devpack.Recruitment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Ninject;
using R3MUS.Devpack.Recruitment.App_Start;

namespace R3MUS.Devpack.Recruitment.Helpers
{
    public class UserStore<T> : IUserStore<T>, IUserRoleStore<T> where T : SSOApplicationUser
    {
        [Inject]
        public IEveAuthenticationService AuthService { get; set; }

        public UserStore()
        {
            Startup.Container.Inject(this);
        }

        public Task AddToRoleAsync(T user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(T user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public Task<T> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(T user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(T user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(T user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T user)
        {
            throw new NotImplementedException();
        }
    }
}