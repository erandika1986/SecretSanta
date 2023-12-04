using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecretSanta.API.Models;

namespace SecretSanta.API.Data.Configurations
{
    public class AssignedBuddyConfiguration : IEntityTypeConfiguration<AssignedBuddy>
    {
        public void Configure(EntityTypeBuilder<AssignedBuddy> builder)
        {
            builder.ToTable("AssignedBuddy");

            builder.HasKey(x => new { x.ParticipantId, x.AssignedParticipantId});


            builder.HasOne<EventParticipant>(a => a.Participant)
                .WithMany(u => u.EventParticipants)
                .HasForeignKey(a => a.ParticipantId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);

            builder.HasOne<EventParticipant>(a => a.AssignedParticipant)
                .WithMany(u => u.AssignedParticipants)
                .HasForeignKey(a => a.AssignedParticipantId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);
        }
    }
}
