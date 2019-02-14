using System.Linq;
using System.Threading.Tasks;
using Digests.Core.Model._4Company;
using Digests.Core.Model._4House;
using Digests.Data.EfCore.Uow;
using Digests.Data.IntegrationTests.UnitOfWork;
using FluentAssertions;
using Xunit;
using Xunit.Priority;

namespace Digests.Data.IntegrationTests.UnitOfWork
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class WallMaterialRepositoryTest : IClassFixture<UowFixture>
    {
        private readonly EfUowDigests _uow;

        public WallMaterialRepositoryTest(UowFixture uowFixture)
        {
            _uow = uowFixture.Uow;
        }



        [Fact, Priority(1)]
        public async Task AddWallMaterialTask()
        {
            var wallMaterial =  WallMaterial.Create("Кирпич", true).Value;
            await _uow.WallMaterialRepository.AddAsync(wallMaterial);
            await _uow.SaveChangesAsync();

            var wallMaterials= await _uow.WallMaterialRepository.ListAsync();
            wallMaterials.Count().Should().Be(1);
        }

    }
}



