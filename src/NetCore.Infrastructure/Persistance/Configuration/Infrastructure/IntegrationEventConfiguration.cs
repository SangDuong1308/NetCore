using NetCore.Application.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NetCore.Infrastructure.Persistance.Configuration.Infrastructure
{
    internal class IntegrationEventConfiguration : IEntityTypeConfiguration<IntegrationEvent>
    {
        public void Configure(EntityTypeBuilder<IntegrationEvent> builder)
        {
            builder.HasKey(x => x.IntergrationEventId);
            // builder.Property(x => x.OccuredAt).HasDefaultValueSql("SYSUTCDATETIME()");
            builder.Property(x => x.OccuredAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(x => x.Type).HasMaxLength(500);
            builder.Property(x => x.AssemblyName).HasMaxLength(500);
            builder.Property(x => x.Payload);
            builder.Property(x => x.PublishedAt);
        }
    }
}
