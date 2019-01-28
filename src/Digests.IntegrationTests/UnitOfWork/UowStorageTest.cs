using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digests.Core.Model._4Company;
using Digests.Core.Model._4House;
using Digests.Data.EfCore.Entities._4Company;
using Digests.Data.EfCore.Entities._4House;
using Digests.Data.EfCore.Mapper;
using Digests.Data.EfCore.Uow;
using FluentAssertions;
using Xunit;
using Xunit.Priority;

namespace Digests.Data.IntegrationTests.UnitOfWork
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class UowStorageTest : IClassFixture<UowFixture>
    {
        private readonly EfUowDigests _uow;

        public UowStorageTest(UowFixture uowFixture)
        {
            _uow = uowFixture.Uow;
        }



        [Fact, Priority(1)]
        public async Task AddNewCompanyTest()
        {
            var newCompany = new Company("РакиВДраки", new CompanyDetails("Раковая тима"));
            await _uow.CompanyRepository.AddAsync(newCompany);
            await _uow.SaveChangesAsync();

            var addedCompany = await _uow.CompanyRepository.GetSingleAsync(comp => comp.Name == newCompany.Name);
            var companies = await _uow.CompanyRepository.ListAsync();
            companies.Count().Should().Be(1);
            addedCompany.Should().NotBeNull();
            addedCompany.CompanyDetails.Should().NotBeNull();
            addedCompany.CompanyDetails.DetailInfo.Should().Be("Раковая тима");
        }


        [Fact, Priority(2)]
        public async Task AddWallMaterialsTest()
        {
            var wallMaterials = new List<WallMaterial>
            {
                new WallMaterial("Кирпич"),
                new WallMaterial("Бетон"),
                new WallMaterial("Дерево")
            };
           await _uow.WallMaterialRepository.AddRangeAsync(wallMaterials);
           await _uow.SaveChangesAsync();

            var listWm = (await _uow.WallMaterialRepository.ListAsync()).ToList();
            var count = listWm.Count;
            count.Should().Be(3);
        }
    }
}