using Database.EFCore;
using DigestsSubDomain.Data.EfCore.DbContext;
using DigestsSubDomain.Data.EfCore.Repositories;

namespace DigestsSubDomain.Data.EfCore.Uow
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

        #endregion

    }
}