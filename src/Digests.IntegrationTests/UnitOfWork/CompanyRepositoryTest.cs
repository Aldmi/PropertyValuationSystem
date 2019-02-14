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
    public class CompanyRepositoryTest : IClassFixture<UowFixture>
    {
        private readonly EfUowDigests _uow;

        public CompanyRepositoryTest(UowFixture uowFixture)
        {
            _uow = uowFixture.Uow;
        }



        #region SharedData4Tests

        //  string wmInternalName, WallMaterial wmExternal, Address houseAddress, int countHouseExpected
        public static IEnumerable<object[]> GetDataAddHouseInCompany => new[]
        {
            new object[] { "Кирпич", null, Address.Create("Новосиб", "калининский", "Танкоавя", "1", "9852 2586").Value, 1 },
            new object[] { null, WallMaterial.Create("Бамбук", false).Value, Address.Create("Новосиб", "Заельцовский", "Овражная", "11", "4593 5639").Value, 2 },
            new object[] { "Бетон", null, Address.Create("Новосиб", "Академ", "ЦветнойБульвар", "100", "9852 89633").Value, 3 },
            new object[] { null, null, Address.Create("Новосиб", "Кировский", "Петухова", "45", "8632 862").Value, 4 }
        };

        //Address addresshouse, int countHouseExpected, int countHouseWithIsSharedWmExpected
        public static IEnumerable<object[]> GetDataRemoveHouseInCompanyByAddress => new[]
        {
            new object[] { Address.Create("Новосиб", "калининский", "Танкоавя", "1", "9852 2586").Value, 3, 1 },
            new object[] { Address.Create("Новосиб", "Заельцовский", "Овражная", "11", "4593 5639").Value, 2, 0 },
            new object[] { Address.Create("Новосиб", "Кировский", "Петухова", "45", "8632 862").Value, 1, 0 }
        };

        //string wmInternalName, House house, int countWallMaterialExpected
        public static IEnumerable<object[]> GetDataUpdateHouseInCompanyTest => new[]
        {
            new object[] { "Дерево",  House.Create(Address.Create("Кемерово", "Центральный", "Ленина", "562", "9856 5621").Value, null).Value, 0},
            new object[] { null, House.Create(Address.Create("Томск", "Центральный", "Павлова", "562", "8963 7412").Value, WallMaterial.Create("шлакоблок").Value).Value, 1},
            new object[] { null, House.Create(Address.Create("Сочи", "Сталинский", "Ленина", "562", "9856 8963").Value, null).Value, 0},
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
            var newCompany = Company.Create("РакиВДраки", CompanyDetails.Create("Раковая тима").Value).Value;
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
                WallMaterial.Create("Кирпич", true).Value,
                WallMaterial.Create("Бетон", true).Value,
                WallMaterial.Create("Дерево", true).Value
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
            var newHouse =  House.Create(houseAddress, wallMaterial).Value;

            var res = await _uow.CompanyRepository.AddHouseInCompanyAsync(company.Id, newHouse);
            await _uow.SaveChangesAsync();

            res.Should().BeTrue();     
            var houses = await _uow.CompanyRepository.GetAllHouseAsync(company.Id);
            houses.Count.Should().Be(countHouseExpected);            
            var newAddedHouse = await _uow.CompanyRepository.GetHouseAsync(company.Id, houseAddress);
            newAddedHouse.Should().NotBeNull();
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
            var newCompDet = CompanyDetails.Create("new Info 0000").Value;

            var res = await _uow.CompanyRepository.UpdateCompanyDetailsAsync(company.Id, newCompDet);
            await _uow.SaveChangesAsync();

            res.Should().BeTrue();
            company = await _uow.CompanyRepository.GetCompanyByNameAsync("РакиВДраки");
            company.CompanyDetails.DetailInfo.Should().Be(newCompDet.DetailInfo);
        }


        [Theory, Priority(6)]
        [MemberData(nameof(GetDataUpdateHouseInCompanyTest))]
        public async Task UpdateHouseInCompanyTest(string wmInternalName, House house, int countWallMaterialExpected)
        {
            var company = await _uow.CompanyRepository.GetCompanyByNameAsync("РакиВДраки");
            var houseUpdated = company.GetHouses.FirstOrDefault();
            if (!string.IsNullOrEmpty(wmInternalName))
            {
                var wallMaterial = _uow.WallMaterialRepository.GetSingle(material => material.Name == wmInternalName);
                house.ChangeWallMaterial(wallMaterial);
            }

            var res = await _uow.CompanyRepository.UpdateHouseInCompanyAsync(company.Id, houseUpdated.Id, house);
            await _uow.SaveChangesAsync();

            res.Should().BeTrue();
            company = await _uow.CompanyRepository.GetCompanyByNameAsync("РакиВДраки");
            houseUpdated = await _uow.CompanyRepository.GetHouseAsync(company.Id, house.Address);
            houseUpdated.Should().NotBeNull();
            houseUpdated.Address.Should().Be(house.Address);

            if (!string.IsNullOrEmpty(wmInternalName))
            {
                houseUpdated.WallMaterial.Name.Should().Be(wmInternalName);
            }
            else
            if (house.WallMaterial == null)
            {
                houseUpdated.WallMaterial.Should().BeNull();
            }
            else
            {
                houseUpdated.WallMaterial.Name.Should().Be(house.WallMaterial.Name);
            }

            var wallMaterials = await _uow.CompanyRepository.GetAllWallMaterialsAsync(company.Id);
            wallMaterials.Count.Should().Be(countWallMaterialExpected);
        }
    }
}