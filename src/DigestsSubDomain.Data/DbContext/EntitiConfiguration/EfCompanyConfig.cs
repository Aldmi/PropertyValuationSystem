using Digests.Core.Model._4Company;
using Digests.Data.EfCore.Entities._4Company;
using Digests.Data.EfCore.Entities._4House;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Digests.Data.EfCore.DbContext.EntitiConfiguration
{
    public class EfCompanyConfig : IEntityTypeConfiguration<EfCompany>
    {
        public void Configure(EntityTypeBuilder<EfCompany> builder)
        {
            builder.Property(h => h.CompanyDetails)
                .HasConversion(
                    v => (v == null) ? null : JsonConvert.SerializeObject(v),
                    v => string.IsNullOrEmpty(v) ? null : JsonConvert.DeserializeObject<CompanyDetails>(v));
        }
    }
}