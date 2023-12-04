using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecretSanta.API.Models;

namespace SecretSanta.API.Data.Configurations
{
    public class EventParticipantConfiguration : IEntityTypeConfiguration<EventParticipant>
    {
        public void Configure(EntityTypeBuilder<EventParticipant> builder)
        {
            builder.ToTable("EventParticipant");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne<Event>(a => a.Event)
                .WithMany(u => u.EventParticipants)
                .HasForeignKey(a => a.EventId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);
        }
    }
}
