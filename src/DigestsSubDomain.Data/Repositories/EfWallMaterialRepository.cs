using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Database.EFCore;
using Digests.Core.Model._4House;
using Digests.Data.Abstract;
using Digests.Data.EfCore.DbContext;
using Digests.Data.EfCore.Entities._4House;
using Digests.Data.EfCore.Mapper;

namespace Digests.Data.EfCore.Repositories
{
    public class EfWallMaterialRepository : EfBaseRepository<EfWallMaterial, WallMaterial>, IWallMaterialRepository
    {
        #region ctor

        public EfWallMaterialRepository(Context context) : base(context, AutoMapperConfig.Mapper)
        {
        }

        #endregion



        #region Specific Methods


        #endregion
    }
}