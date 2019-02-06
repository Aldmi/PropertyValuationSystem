using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Database.EFCore;
using Digests.Core.Model._4House;
using Digests.Data.Abstract;
using Digests.Data.EfCore.DbContext;
using Digests.Data.EfCore.Entities._4House;
using Digests.Data.EfCore.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Digests.Data.EfCore.Repositories
{
    public class EfWallMaterialRepository : EfBaseRepository<EfWallMaterial, WallMaterial>, IWallMaterialRepository
    {
        private readonly Context _context;


        #region ctor

        public EfWallMaterialRepository(Context context) : base(context, AutoMapperConfig.Mapper)
        {
            _context = context;
        }

        #endregion



        #region Special Methods

        //DEBUG
        public void UpdateTest(string name)
        {
            //var wallMaterial= DbSet.FirstOrDefault(w => w.Name == name);
            //var wallMaterial = DbSet.AsNoTracking().FirstOrDefault();
            var wallMaterial = _context.WallMaterials.AsNoTracking().FirstOrDefault();        
            wallMaterial.Name = "New22222";
            var state = _context.Entry(wallMaterial).State;
            //_context.Entry(wallMaterial).State = EntityState.Modified;
            _context.Entry(wallMaterial).Property(p => p.Name).IsModified = true;
            //context.Entry(student).State = student.StudentId == 0 ? EntityState.Added : EntityState.Modified;

            //DbSet.Update(wallMaterial);
        }

        #endregion


    }
}