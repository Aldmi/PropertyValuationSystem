using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Digests.Core.Model._4Company;
using Digests.Data.EfCore.DbContext;
using Digests.Data.EfCore.Entities._4Company;
using Digests.Data.EfCore.Entities._4House;
using Digests.IntegrationTests.Libs;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace Digests.IntegrationTests.DbContextRow
{
    public class DbContextRowTest : IClassFixture<DbContextFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly Context _context;

        public DbContextRowTest(DbContextFixture contextFixture, ITestOutputHelper output)
        {
            _output = output;
            _context = contextFixture.Context;
        }



        [Fact, TestPriority(3)]
        public async Task AddNewCompanyTest()
        {
            //ADD new company-----------------------------------------
            var newCompany = new EfCompany { Name = "РакиВДраки", CompanyDetails = new CompanyDetails("Раковая тима") };
            await _context.Companys.AddAsync(newCompany);
            await _context.SaveChangesAsync();

            var addedCompany = await _context.Companys.FirstOrDefaultAsync(comp => comp.Name == newCompany.Name);
            var count = await _context.Companys.CountAsync();
            count.Should().Be(1);
            addedCompany.Should().NotBeNull();
            //addedCompany.EfCompanyDetail.Should().NotBeNull();
            //addedCompany.EfCompanyDetail.DetailInfo.Should().Be("Раковая тима");

            //Add WallMaterials-----------------------------------------
            var wallMaterials = new List<EfWallMaterial>
            {
                new EfWallMaterial {Name = "Кирпич"},
                new EfWallMaterial {Name = "Бетон"},
                new EfWallMaterial {Name = "Дерево"}
            };
            await _context.WallMaterials.AddRangeAsync(wallMaterials);
            await _context.SaveChangesAsync();

            count = await _context.WallMaterials.CountAsync();
            count.Should().Be(3);

            ////Add House in Company-----------------------------------------
            //var wallMaterial = await _context.WallMaterials.FirstOrDefaultAsync(wm => wm.Name == "Кирпич");
            //var newHouse = new EfHouse {City = "Новосибирск", WallMaterial = wallMaterial };
            //var company = await _context.Companys.FirstOrDefaultAsync(comp => comp.Name == newCompany.Name);
            //company.EfHouses.Add(newHouse);
            //await _context.SaveChangesAsync();

            ////Remove WallMaterials-----------------------------------------
            //wallMaterial = await _context.WallMaterials.FirstOrDefaultAsync(wm => wm.Name == "Кирпич");
            //_context.WallMaterials.Remove(wallMaterial);
            //await _context.SaveChangesAsync();

            //count = await _context.WallMaterials.CountAsync();
            //company = await _context.Companys.FirstOrDefaultAsync(comp => comp.Name == newCompany.Name);
            //var housesWithoutWallmaterial= company.EfHouses.Where(h => h.WallMaterial == null).ToList();
            //housesWithoutWallmaterial.Count.Should().Be(1);
            //count.Should().Be(2);

            ////AddNewCompanyWith2House
            //newCompany = new EfCompany { Name = "рога и копыта", EfCompanyDetail = new EfCompanyDetail { DetailInfo = "холодец" } };
            //await _context.Companys.AddAsync(newCompany);
            //await _context.SaveChangesAsync();
            //addedCompany = await _context.Companys.FirstOrDefaultAsync(comp => comp.Name == newCompany.Name);
            //var wallMaterial1 = await _context.WallMaterials.FirstOrDefaultAsync(wm => wm.Name == "Бетон");
            //var wallMaterial2 = await _context.WallMaterials.FirstOrDefaultAsync(wm => wm.Name == "Дерево");
            //var wallMaterialNotFound = await _context.WallMaterials.FirstOrDefaultAsync(wm => wm.Name == "бамбук");
            //var newHouses = new List<EfHouse>
            //{
            //    new EfHouse {City = "Новосибирск", Street="Красный проспект", WallMaterial = wallMaterial1},
            //    new EfHouse {City = "Новосибирск", Street="Танковая", WallMaterial = wallMaterial2},
            //    new EfHouse {City = "Новосибирск", Street="Мичурина", WallMaterial = wallMaterialNotFound}
            //};        
            //addedCompany.EfHouses.AddRange(newHouses);
            //await _context.SaveChangesAsync();

            //var countComp = await _context.Companys.CountAsync();
            //countComp.Should().Be(2);
            //addedCompany = await _context.Companys.FirstOrDefaultAsync(comp => comp.Name == newCompany.Name);
            //var addedHouses = _context.Houses.Where(h => h.EfCompanyId == addedCompany.Id).ToList();        
            //addedHouses.Count.Should().Be(3);

            ////Remove House in Company-----------------------------------------
            //company = await _context.Companys.FirstOrDefaultAsync(comp => comp.Name == "рога и копыта");
            //var removingHouse =_context.Houses.FirstOrDefault(h => h.EfCompanyId == company.Id && h.Street == "Танковая");
            //company.EfHouses.Remove(removingHouse);
            //await _context.SaveChangesAsync();

            //countComp = await _context.Houses.CountAsync(h=>h.EfCompanyId == company.Id);
            //countComp.Should().Be(2);
        }


        [Fact, TestPriority(1)]
        public async Task xxxxx()
        {

        }


        [Fact, TestPriority(2)]
        public async Task yyyyy()
        {

        }
    }
}