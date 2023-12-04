using SecretSanta.API.Models.Enum;

namespace SecretSanta.API.Models
{
    public class Event
    {
        public Event()
        {
            EventParticipants = new List<EventParticipant>();
         }

        public int Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string EmailSubject { get; set; }
        public string EmailTemplatePath { get; set; }
        public decimal MinimumGiftValue { get; set; }
        public decimal MaximumGiftValue { get; set; }
        public EventStatus Status { get; set; }

        public virtual ICollection<EventParticipant> EventParticipants { get; set; }
    }
}
