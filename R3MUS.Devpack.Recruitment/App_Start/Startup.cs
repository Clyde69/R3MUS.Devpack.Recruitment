using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Ninject;
using Ninject.Web.Common.OwinHost;
using System.Web.Http;
using System.Reflection;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Web.Helpers;
using R3MUS.Devpack.Recruitment.Helpers;

[assembly: OwinStartup(typeof(R3MUS.Devpack.Recruitment.App_Start.Startup))]

namespace R3MUS.Devpack.Recruitment.App_Start
{
    public partial class Startup
    {
        public static IKernel Container { get; set; }

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            Container = new StandardKernel();
            Container.Load(Assembly.GetExecutingAssembly());
            ConfigureBindings(Container);

            config.DependencyResolver = new NinjectResolver(Container);

            ConfigureAuth(app);

            app.UseNinjectMiddleware(() => Container);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext<SSOUserManager>(SSOUserManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Index"),
                ExpireTimeSpan = TimeSpan.FromDays(1)
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
    }
}