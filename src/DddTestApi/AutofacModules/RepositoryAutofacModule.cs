using System.Collections.Generic;
using Autofac;
using Autofac.Core;
using Digests.Data.Abstract;
using Digests.Data.EfCore.Repositories;
using Digests.Data.EfCore.Uow;

namespace DddTestApi.AutofacModules
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
            builder.Register(c => EfUowDigests.UowDigestsFactory(_connectionString)).As<IUnitOfWorkDigests>()
                   .InstancePerLifetimeScope();
                

        }
    }
}