namespace SecretSanta.API.Models
{
    public class EventParticipant
    {
        public EventParticipant()
        {
            EventParticipants = new HashSet<AssignedBuddy>();
            AssignedParticipants = new HashSet<AssignedBuddy>();
        }

        public int Id { get; set; }
        public int EventId { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }

        public virtual Event Event { get; set; }
        public virtual ICollection<AssignedBuddy> EventParticipants { get; set; }
        public virtual ICollection<AssignedBuddy> AssignedParticipants { get; set; }

    }
}
