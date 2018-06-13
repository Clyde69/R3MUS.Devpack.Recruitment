using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.Owin;
using R3MUS.Devpack.Recruitment.Models;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Security.Claims;

namespace R3MUS.Devpack.Recruitment.Helpers
{
    public class SSOUserManager : UserManager<SSOApplicationUser>, ISSOUserManager
    {
        public static SSOApplicationUser SiteUser
        {
            get { return (SSOApplicationUser)HttpContext.Current.Session["SiteUser"]; }
            set { HttpContext.Current.Session["SiteUser"] = value; }
        }

        public SSOUserManager(IUserStore<SSOApplicationUser> store) : base(store)
        {
        }

        public static SSOUserManager Create(IdentityFactoryOptions<SSOUserManager> options, IOwinContext context)
        {
            return new SSOUserManager(new UserStore<SSOApplicationUser>());
        }

        public SSOApplicationUser FindByIdentity(ClaimsIdentity identity)
        {
            FindByIdAsync(identity.GetUserId());
            SiteUser.Identity = identity;
            return SiteUser;
        }

        public override Task<SSOApplicationUser> FindByIdAsync(string userId)
        {
            var character = new Devpack.ESI.Models.Character.Detail(Convert.ToInt64(userId));

            var siteUser = new SSOApplicationUser()
            {
                Id = userId,
                UserName = character.Name,
                CorporationId = (long)character.CorporationId,
                Character = character
            };
            
            if (HttpContext.Current != null)
            {
                SiteUser = siteUser;
            }

            return Task.FromResult(siteUser);
        }
    }
}