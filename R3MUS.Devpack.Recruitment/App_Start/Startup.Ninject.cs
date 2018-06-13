using AutoMapper;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common;
using System.Reflection;

namespace R3MUS.Devpack.Recruitment.App_Start
{
    public partial class Startup
    {
        private static void ConfigureBindings(IKernel kernel)
        {
            kernel.Bind(x =>
            {
                x.FromThisAssembly()
                    .SelectAllClasses()
                    .BindDefaultInterface()
                    .Configure(y => y.InRequestScope());                
            });
            kernel.Bind<IMapper>()
                .ToMethod(context =>
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfiles(Assembly.GetExecutingAssembly());
                        // tell automapper to use ninject when creating value converters and resolvers
                        cfg.ConstructServicesUsing(t => kernel.Get(t));
                    });
                    return config.CreateMapper();
                }).InSingletonScope();
        }
    }
}