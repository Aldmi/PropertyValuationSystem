using System;
using System.Linq;
using System.Threading.Tasks;
using Digests.Core.Model._4House;
using FluentAssertions;
using Xunit;
using Xunit.Priority;

namespace Digests.IntegrationTests
{
    public class Model2StorageTest : IClassFixture<UowFixture>
    {
        //private readonly UowFixture _uowFixture;
        //public Model2StorageTest(UowFixture uowFixture)
        //{
        //    _uowFixture = uowFixture;
        //}


        //[Fact, Priority(-10)]
        //public void GetEmptyListWallmaterialRep()
        //{
        //    var wmList = _uowFixture.Uow.WallMaterialRepository.List().ToList();
        //    wmList.Count.Should().Be(0);
        //}


        //[Fact, Priority(0)]
        //public async Task AddOneItemInWallmaterialRep()
        //{
        //    var wmNew = new WallMaterial("Кирпич");
        //    await _uowFixture.Uow.WallMaterialRepository.AddAsync(wmNew);
        //    await _uowFixture.Uow.SaveChangesAsync();

        //    var count = await _uowFixture.Uow.WallMaterialRepository.CountAsync(material => true);
        //    var firstItem = await _uowFixture.Uow.WallMaterialRepository.GetSingleAsync(material => true);

        //    count.Should().Be(1);
        //    firstItem.Name.Should().Be("Кирпич"); 
        //}
    }
}