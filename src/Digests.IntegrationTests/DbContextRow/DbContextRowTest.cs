using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Digests.Core.Model._4Company;
using Digests.Data.EfCore.DbContext;
using Digests.Data.EfCore.Entities._4Company;
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
            //var newCompany = new EfCompany {Name = "РакиВДраки", EfCompanyDetail = new EfCompanyDetail{DetailInfo = "Раковая тима"}};
            //await _context.Companys.AddAsync(newCompany);
            //await _context.SaveChangesAsync();

            //var addedCompany= await _context.Companys.FirstOrDefaultAsync(comp => comp.Name == newCompany.Name);  
            //var count= await _context.Companys.CountAsync();
            //count.Should().Be(1);
            //addedCompany.Should().NotBeNull();
            //addedCompany.EfCompanyDetail.Should().NotBeNull();
            //addedCompany.EfCompanyDetail.DetailInfo.Should().Be("Раковая тима");

            _output.WriteLine(DateTime.Now.ToString("O"));
        }


        [Fact, TestPriority(1)]
        public async Task xxxxx()
        {
            //10.Should().Be(0);
            _output.WriteLine(DateTime.Now.ToString("O"));
        }


        [Fact, TestPriority(2)]
        public async Task yyyyy()
        {
            //10. Should().Be(0);
            _output.WriteLine(DateTime.Now.ToString("O"));
        }
    }
}