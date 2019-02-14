using System.Collections.Generic;
using Application.Services;
using Autofac;
using Autofac.Core;
using Digests.Data.Abstract;
using Digests.Data.EfCore.Repositories;
using Digests.Data.EfCore.Uow;

namespace DddTestApi.AutofacModules
{
    public class ApplicationServicesAutofacModule : Module
    {


        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DigestsService>().AsSelf()
                   .InstancePerLifetimeScope();
                
        }
    }
}