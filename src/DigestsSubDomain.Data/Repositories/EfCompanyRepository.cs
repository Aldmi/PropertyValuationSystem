using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Database.EFCore;
using Digests.Core.Model._4Company;
using Digests.Data.Abstract;
using Digests.Data.EfCore.DbContext;
using Digests.Data.EfCore.Entities._4Company;
using Digests.Data.EfCore.Mapper;

namespace Digests.Data.EfCore.Repositories
{
    public class EfCompanyRepository : EfBaseRepository<EfCompany, Company>, ICompanyRepository
    {
        #region ctor

        public EfCompanyRepository(Context context) : base(context, AutoMapperConfig.Mapper)
        {
        }

        #endregion



        #region Specific Methods


        #endregion
    }
}