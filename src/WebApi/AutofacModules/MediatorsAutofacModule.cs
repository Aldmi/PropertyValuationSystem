using Autofac;
using BL.Services.Mediators.DigestMediators;

namespace WebApi.AutofacModules
{
    public class MediatorsAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DigestBaseMediator>().InstancePerDependency();           
        }
    }
}