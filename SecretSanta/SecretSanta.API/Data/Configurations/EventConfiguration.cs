using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecretSanta.API.Models;

namespace SecretSanta.API.Data.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Event");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.MinimumGiftValue)
                .HasPrecision(6,2)
                .HasDefaultValue(1000.00);

            builder.Property(p => p.MaximumGiftValue)
                .HasPrecision(6, 2)
                .HasDefaultValue(2500.00);
        }
    }
}
