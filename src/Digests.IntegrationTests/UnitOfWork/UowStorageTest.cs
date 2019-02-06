using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
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



        #region SharedData4Tests

        //  string wmInternalName, WallMaterial wmExternal, Address houseAddress, int countHouseExpected
        public static IEnumerable<object[]> GetDataAddHouseInCompany => new[]
        {
            new object[] { "Кирпич", null, new Address("Новосиб", "калининский", "Танкоавя", "1", "9852 2586"), 1 },
            new object[] { null, new WallMaterial("Бамбук", false), new Address("Новосиб", "Заельцовский", "Овражная", "11", "4593 5639"), 2 },
            new object[] { "Бетон", null, new Address("Новосиб", "Академ", "ЦветнойБульвар", "100", "9852 89633"), 3 },
            new object[] { null, null, new Address("Новосиб", "Кировский", "Петухова", "45", "8632 862"), 4 }
        };

        //Address addresshouse, int countHouseExpected, int countHouseWithIsSharedWmExpected
        public static IEnumerable<object[]> GetDataRemoveHouseInCompanyByAddress => new[]
        {
            new object[] { new Address("Новосиб", "калининский", "Танкоавя", "1", "9852 2586"), 3, 1 },
            new object[] { new Address("Новосиб", "Заельцовский", "Овражная", "11", "4593 5639"), 2, 0 },
            new object[] { new Address("Новосиб", "Кировский", "Петухова", "45", "8632 862"), 1, 0 }
        };
                    
        #endregion







        //[Fact, Priority(-10)]
        //public void MapperTest()
        //{
        //    try
        //    {
        //        var efWm = new EfWallMaterial { Name = "saswas", IsShared = true, Id = 10 };
        //        var wm = AutoMapperConfig.Mapper.Map<WallMaterial>(efWm);

        //        wm = new WallMaterial("fdfdfd", true);
        //        efWm = AutoMapperConfig.Mapper.Map<EfWallMaterial>(wm);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //}


        [Fact, Priority(1)]
        public async Task AddNewCompanyTest()
        {
            var newCompany = new Company("РакиВДраки", new CompanyDetails("Раковая тима"));
            await _uow.CompanyRepository.AddAsync(newCompany);
            await _uow.SaveChangesAsync();

            var companyes = await _uow.CompanyRepository.ListAsync();
            companyes.Count().Should().Be(1);
            var addedCompany = await _uow.CompanyRepository.GetSingleAsync(comp => comp.Name == newCompany.Name);
            addedCompany.Should().NotBeNull();
            addedCompany.CompanyDetails.Should().NotBeNull();
            addedCompany.CompanyDetails.DetailInfo.Should().Be("Раковая тима");
        }


        [Fact, Priority(2)]
        public async Task AddWallMaterialsTest()
        {
            var wallMaterials = new List<WallMaterial>
            {
                new WallMaterial("Кирпич", true),
                new WallMaterial("Бетон", true),
                new WallMaterial("Дерево", true)
            };
           await _uow.WallMaterialRepository.AddRangeAsync(wallMaterials);
           await _uow.SaveChangesAsync();

            var listWm = (await _uow.WallMaterialRepository.ListAsync()).ToList();
            var count = listWm.Count;
            count.Should().Be(3);
        }


        //[Fact, Priority(3)]
        //public async Task UpdateWallMaterialsTest()
        //{
        //    try
        //    {
        //        //var wallMaterial = await _uow.WallMaterialRepository.GetSingleAsync(wm => wm.Name == "Кирпич");
        //        //wallMaterial.ChangeName("New Кирпич");
        //        //_uow.WallMaterialRepository.Edit(wallMaterial);

        //        _uow.WallMaterialRepository.UpdateTest("Кирпич");

        //        await _uow.SaveChangesAsync();
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}



        [Theory, Priority(3)]
        [MemberData(nameof(GetDataAddHouseInCompany))]
        public async Task AddHouseInCompany(string wmInternalName,
                                            WallMaterial wmExternal,
                                            Address houseAddress,
                                            int countHouseExpected)
        {
            var wallMaterial = !string.IsNullOrEmpty(wmInternalName)
                ? await _uow.WallMaterialRepository.GetSingleAsync(wm => wm.Name == "Кирпич")
                : wmExternal;
            var company = await _uow.CompanyRepository.GetCompanyByNameAsync("РакиВДраки");
            var newHouse = new House(houseAddress, wallMaterial);

            var res = await _uow.CompanyRepository.AddHouseInCompanyAsync(company.Id, newHouse);
            await _uow.SaveChangesAsync();

            res.Should().BeTrue();
            var houses = await _uow.CompanyRepository.GetAllHouseAsync(company.Id);
            houses.Count.Should().Be(countHouseExpected);
            var newAddedHouse = await _uow.CompanyRepository.GetHouseAsync(company.Id, houseAddress);

            if (wallMaterial == null)
            {
                newAddedHouse.WallMaterial.Should().BeNull();
            }
            else
            {
                newAddedHouse.WallMaterial.Name.Should().Be(wallMaterial.Name);
                newAddedHouse.WallMaterial.IsShared.Should().Be(wallMaterial.IsShared);
            }
        }


        [Theory, Priority(4)]
        [MemberData(nameof(GetDataRemoveHouseInCompanyByAddress))]
        public async Task RemoveHouseInCompanyByAddress(Address addresshouse, int countHouseExpected, int countHouseWithIsSharedWmExpected)
        {
            var company = await _uow.CompanyRepository.GetCompanyByNameAsync("РакиВДраки");
            var res = await _uow.CompanyRepository.RemoveHouseInCompanyAsync(company.Id, addresshouse);
            await _uow.SaveChangesAsync();

            res.Should().BeTrue();
            var houses = await _uow.CompanyRepository.GetAllHouseAsync(company.Id);
            houses.Count.Should().Be(countHouseExpected);

            var wallMaterials = await _uow.CompanyRepository.GetAllWallMaterialsAsync(company.Id);
            wallMaterials.Count.Should().Be(countHouseWithIsSharedWmExpected);
        }



        [Fact, Priority(5)]
        public async Task UpdateCompanyDetailsTest()
        {
            var company = await _uow.CompanyRepository.GetCompanyByNameAsync("РакиВДраки");
            var newCompDet = new CompanyDetails("new Info 0000");

            var res = await _uow.CompanyRepository.UpdateCompanyDetailsAsync(company.Id, newCompDet);
            await _uow.SaveChangesAsync();

            res.Should().BeTrue();
            company = await _uow.CompanyRepository.GetCompanyByNameAsync("РакиВДраки");
            company.CompanyDetails.DetailInfo.Should().Be(newCompDet.DetailInfo);        
        }
    }
}