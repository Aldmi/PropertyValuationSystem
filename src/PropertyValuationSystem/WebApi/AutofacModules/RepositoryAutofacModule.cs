using System.Collections.Generic;
using Autofac;
using Autofac.Core;
using DAL.Abstract.Concrete;
using DAL.EFCore.Entities.HouseDigests;
using DAL.EFCore.Repository;
using DAL.EFCore.Repository.Digests;

namespace WebApi.AutofacModules
{
    public class RepositoryAutofacModule : Module
    {
        private readonly string _connectionString;



        #region ctor

        public RepositoryAutofacModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        #endregion



        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EfHouseRepository>().As<IHouseRepository>()
                .WithParameters(new List<Parameter>
                {
                    new NamedParameter("connectionString", _connectionString),
                })
                .InstancePerLifetimeScope();

            builder.RegisterType<EfWallMaterialRepository>().As<IWallMaterialRepository>()
                .WithParameters(new List<Parameter>
                {
                    new NamedParameter("connectionString", _connectionString),
                })
                .InstancePerLifetimeScope();

            builder.RegisterType<EfCompanyRepository>().As<ICompanyRepository>()
                .WithParameters(new List<Parameter>
                {
                    new NamedParameter("connectionString", _connectionString),
                })
                .InstancePerLifetimeScope();
        }
    }
}