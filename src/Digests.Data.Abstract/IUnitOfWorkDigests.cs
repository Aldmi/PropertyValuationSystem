using System;
using System.Threading.Tasks;
using Shared.Kernel.Enums;

namespace Digests.Data.Abstract
{
    public interface IUnitOfWorkDigests : IDisposable
    {
        ICompanyRepository CompanyRepository { get; }
        IHouseRepository HouseRepository { get; }
        IWallMaterialRepository WallMaterialRepository { get;  }
        ISharedWallMaterialsRepository SharedWallMaterialsRepository { get; }


        Task<int> SaveChangesAsync();
        int SaveChanges();
        Task CreateDb(HowCreateDb howCreateDb);
    }
}