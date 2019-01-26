using System;
using System.Threading.Tasks;
using Shared.Kernel.Enums;

namespace Digests.Data.Abstract
{
    public interface IUnitOfWorkDigests : IDisposable
    {
        ICompanyRepository CompanyRepository { get; }
        IWallMaterialRepository WallMaterialRepository { get;  }

        Task<int> SaveChangesAsync();
        int SaveChanges();
        Task CreateDb(HowCreateDb howCreateDb);
    }
}