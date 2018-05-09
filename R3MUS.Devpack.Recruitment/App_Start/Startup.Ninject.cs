using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common;

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
        }
    }
}