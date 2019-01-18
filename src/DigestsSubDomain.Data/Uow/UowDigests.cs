using Database.EFCore;
using Digests.Data.EfCore.DbContext;
using Digests.Data.EfCore.Mapper;
using Digests.Data.EfCore.Repositories;

namespace Digests.Data.EfCore.Uow
{
    public class UowDigests : EfBaseUnitOfWork
    {
        private readonly Microsoft.EntityFrameworkCore.DbContext _dbContext;


        #region prop

        public ICompanyRepository CompanyRepository { get; set; }

        #endregion



        #region ctor

        public UowDigests(string connectionString) : base(new Context(connectionString))
        {
            _dbContext = new Context(connectionString);
        }

        static UowDigests()
        {
            AutoMapperConfig.Register();
        }

        #endregion
    }
}