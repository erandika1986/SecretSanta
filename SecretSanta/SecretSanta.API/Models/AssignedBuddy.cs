namespace SecretSanta.API.Models
{
    public class AssignedBuddy
    {
        public int ParticipantId { get; set; }
        public int AssignedParticipantId { get; set; }

        public virtual EventParticipant Participant { get; set; }
        public virtual EventParticipant AssignedParticipant { get; set; }
    }
}
