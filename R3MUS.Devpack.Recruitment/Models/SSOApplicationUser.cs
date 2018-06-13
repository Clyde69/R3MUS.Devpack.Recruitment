using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Security.Principal;

namespace R3MUS.Devpack.Recruitment.Models
{
    public class SSOApplicationUser : IdentityUser, IPrincipal
    {
        public string AuthToken { get; set; }
        public long CorporationId { get; set; }

        public IIdentity Identity { get; set; }

        public ESI.Models.Character.Detail Character { get; set; }

        public void AddToRole(string roleName)
        {
            Roles.Add(new IdentityUserRole() { RoleId = roleName, UserId = this.Id });
        }

        public bool IsInRole(string role)
        {
            return Roles.Select(s => s.RoleId).Contains(role);
        }
    }
}