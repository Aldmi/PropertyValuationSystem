using System;
using Database.EFCore;
using Digests.Data.Abstract;
using Digests.Data.EfCore.DbContext;
using Digests.Data.EfCore.Mapper;
using Digests.Data.EfCore.Repositories;

namespace Digests.Data.EfCore.Uow
{
    public class EfUowDigests : EfBaseUnitOfWork, IUnitOfWorkDigests
    {
        #region fields

        private readonly Context _context;
        private ICompanyRepository _companyRepository;
        private IWallMaterialRepository _wallMaterialRepository;

        #endregion


        #region prop

        public ICompanyRepository CompanyRepository => _companyRepository ?? (_companyRepository = new EfCompanyRepository(_context));    
        public IWallMaterialRepository WallMaterialRepository => _wallMaterialRepository ?? (_wallMaterialRepository = new EfWallMaterialRepository(_context));

        #endregion



        #region ctor

        private EfUowDigests(Context context) : base(context)
        {
            _context = context;
        }

        static EfUowDigests()
        {
            AutoMapperConfig.Register(); //При первом создании Uow, будет настроенн AutoMapper 
            try
            {
                AutoMapperConfig.AssertConfigurationIsValid();//Если настройки мапиинга не валидны будет выбрашенно исключение
            }
            catch (Exception e)
            {
                throw;
            }
        
        }

        #endregion



        public static EfUowDigests UowDigestsFactory(string connectionString)
        {
           var context= new Context(connectionString);
           var uow = new EfUowDigests(context);
           return uow;
        }




        #region Disposing

        public new void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}