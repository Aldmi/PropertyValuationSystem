using System;
using Digests.Core.Model._4House;
using Digests.Data.EfCore.Entities._4House;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Digests.Data.EfCore.DbContext.EntitiConfiguration
{
    public class EfHouseConfig : IEntityTypeConfiguration<EfHouse>
    {
        public void Configure(EntityTypeBuilder<EfHouse> builder)
        {
            builder.Property(h => h.Address)
                .HasConversion(
                    v => (v == null) ? null : JsonConvert.SerializeObject(v),
                    v => string.IsNullOrEmpty(v) ? null : JsonConvert.DeserializeObject<Address>(v));
        }
    }
}