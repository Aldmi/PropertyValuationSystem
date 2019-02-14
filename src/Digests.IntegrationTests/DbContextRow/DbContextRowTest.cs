using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digests.Core.Model._4Company;
using Digests.Core.Model._4House;
using Digests.Data.EfCore.DbContext;
using Digests.Data.EfCore.Entities._4Company;
using Digests.Data.EfCore.Entities._4House;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using PriorityOrderer = Xunit.Priority.PriorityOrderer;

namespace Digests.Data.IntegrationTests.DbContextRow
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class DbContextRowTest : IClassFixture<DbContextFixture>
    {
        //private readonly ITestOutputHelper _output;
        //private readonly Context _context;

        //public DbContextRowTest(DbContextFixture contextFixture, ITestOutputHelper output)
        //{
        //    _output = output;
        //    _context = contextFixture.Context;
        //}



        //[Fact, Priority(1)]
        //public async   Task AddNewCompanyTest()
        //{
        //    var newCompany = new EfCompany { Name = "РакиВДраки", CompanyDetails = new CompanyDetails("Раковая тима") };
        //    await _context.Companys.AddAsync(newCompany);
        //    await _context.SaveChangesAsync();

        //    var addedCompany = await _context.Companys.FirstOrDefaultAsync(comp => comp.Name == newCompany.Name);
        //    var count = await _context.Companys.CountAsync();
        //    count.Should().Be(1);
        //    addedCompany.Should().NotBeNull();
        //    addedCompany.CompanyDetails.Should().NotBeNull();
        //    addedCompany.CompanyDetails.DetailInfo.Should().Be("Раковая тима");
        //}


        //[Fact, Priority(2)]
        //public async Task AddWallMaterialsTest()
        //{
        //    var wallMaterials = new List<EfWallMaterial>
        //    {
        //        new EfWallMaterial {Name = "Кирпич"},
        //        new EfWallMaterial {Name = "Бетон"},
        //        new EfWallMaterial {Name = "Дерево"}
        //    };
        //    await _context.WallMaterials.AddRangeAsync(wallMaterials);
        //    await _context.SaveChangesAsync();

        //    var count = await _context.WallMaterials.CountAsync();
        //    count.Should().Be(3);
        //}


        //[Fact, Priority(3)]
        //public async Task AddHouseInCompany()
        //{
        //    var wallMaterial = await _context.WallMaterials.FirstOrDefaultAsync(wm => wm.Name == "Кирпич");
        //    var newHouse = new EfHouse
        //    {
        //        Address = new Address("Новосиб", "калининский", "Танкоавя", "1", "9852 2586"),
        //        WallMaterial = wallMaterial
        //    };
        //    var company = await _context.Companys.FirstOrDefaultAsync(comp => comp.Name == "РакиВДраки");
        //    company.Houses.Add(newHouse);
        //    await _context.SaveChangesAsync();

        //    company = await _context.Companys.FirstOrDefaultAsync(comp => comp.Name == "РакиВДраки");
        //    company.Houses.Count.Should().Be(1);
        //}


        //[Fact, Priority(4)]
        //public async Task RemoveWallMaterials()
        //{
        //    var wallMaterial = await _context.WallMaterials.FirstOrDefaultAsync(wm => wm.Name == "Кирпич");

        //    _context.WallMaterials.Remove(wallMaterial);
        //    await _context.SaveChangesAsync();

        //    var count = await _context.WallMaterials.CountAsync();
        //    count.Should().Be(2);
        //}


        //[Fact, Priority(5)]
        //public async Task AddNewCompanyWith2House()
        //{
        //    var newCompany = new EfCompany { Name = "рога и копыта", CompanyDetails = new CompanyDetails("холодец") };
        //    await _context.Companys.AddAsync(newCompany);
        //    await _context.SaveChangesAsync();
        //    var addedCompany = await _context.Companys.FirstOrDefaultAsync(comp => comp.Name == newCompany.Name);
        //    var wallMaterial1 = await _context.WallMaterials.FirstOrDefaultAsync(wm => wm.Name == "Бетон");
        //    var wallMaterial2 = await _context.WallMaterials.FirstOrDefaultAsync(wm => wm.Name == "Дерево");
        //    var wallMaterialNotFound = await _context.WallMaterials.FirstOrDefaultAsync(wm => wm.Name == "бамбук");
        //    var newHouses = new List<EfHouse>
        //    {
        //        new EfHouse { Address = new Address("Новосиб", "калининский", "Красный проспект", "10", "9852 2586"), WallMaterial = wallMaterial1 },
        //        new EfHouse { Address = new Address("Новосиб", "калининский", "Иподромская", "589", "9852 2586"), WallMaterial = wallMaterial2 },
        //        new EfHouse { Address = new Address("Новосиб", "калининский", "Танковая", "63", "9852 5963"), WallMaterial = wallMaterialNotFound },
        //    };
        //    addedCompany.Houses.AddRange(newHouses);
        //    await _context.SaveChangesAsync();

        //    var countComp = await _context.Companys.CountAsync();
        //    countComp.Should().Be(2);
        //    addedCompany = await _context.Companys.FirstOrDefaultAsync(comp => comp.Name == newCompany.Name);
        //    var addedHouses = _context.Companys.SelectMany(c=>c.Houses).Where(h => h.EfCompanyId== addedCompany.Id).ToList();
        //    addedHouses.Count.Should().Be(3);
        //}


        //[Fact, Priority(6)]
        //public async Task RemoveHouseInCompany()
        //{
        //    var company = await _context.Companys.FirstOrDefaultAsync(comp => comp.Name == "рога и копыта");
        //    var address = new Address("Новосиб", "калининский", "Танковая", "63", "9852 5963");
        //    var removingHouse = company.Houses.FirstOrDefault(h => (h.EfCompanyId == company.Id) && (h.Address == address));

        //    company.Houses.Remove(removingHouse);
        //    await _context.SaveChangesAsync();

        //    int countHouse = await _context.Companys.SelectMany(c => c.Houses).CountAsync(h => h.EfCompanyId == company.Id);
        //    countHouse.Should().Be(2);
        //}


        //[Fact, Priority(7)]
        //public async Task RemoveCompany()
        //{
        //    var company = await _context.Companys.FirstOrDefaultAsync(comp => comp.Name == "рога и копыта");
        //    _context.Companys.Remove(company);
        //    await _context.SaveChangesAsync();

        //    int countComp = await _context.Companys.CountAsync();
        //    countComp.Should().Be(1);

        //    var firsthouse = await _context.Companys.SelectMany(c => c.Houses).FirstOrDefaultAsync();
        //    var address = new Address("Новосиб", "калининский", "Танкоавя", "1", "9852 2586");
        //    firsthouse.Address.Should().Be(address);

        //    var firstCompany= await _context.Companys.FirstOrDefaultAsync();
        //    firstCompany.Name.Should().Be("РакиВДраки");

        //    var countWm = await _context.WallMaterials.CountAsync();
        //    countWm.Should().Be(2);
        //}
    }
}