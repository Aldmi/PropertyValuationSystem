using System.Linq;
using System.Threading.Tasks;
using Digests.Core.Model._4House;
using FluentAssertions;
using Xunit;


namespace Digests.IntegrationTests
{
    public class SharedItemsStorageTest : IClassFixture<UowFixture>
    {
        //private readonly UowFixture _uowFixture;
        //public SharedItemsStorageTest(UowFixture uowFixture)
        //{
        //    _uowFixture = uowFixture;
        //}



        //[Fact, Priority(0)]
        //public void GetEmptyListWallMaterials()
        //{
        //    var wmList = _uowFixture.Uow.WallMaterialRepository.List().ToList();
        //    wmList.Count.Should().Be(0);
        //}


        //[Fact, Priority(10)]
        //public async Task AddOneItemInWallMaterials()
        //{
        //    var wmNew = new WallMaterial("Кирпич");
        //    await _uowFixture.Uow.SharedWallMaterialsRepository.AddAsync(wmNew);
        //    await _uowFixture.Uow.SaveChangesAsync();

        //    var count = await _uowFixture.Uow.SharedWallMaterialsRepository.CountAsync(material => true);
        //    var firstItem = await _uowFixture.Uow.SharedWallMaterialsRepository.GetSingleAsync(material => true);

        //    count.Should().Be(1);
        //    firstItem.Name.Should().Be("Кирпич");
        //}
    }
}