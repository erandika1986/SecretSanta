namespace SecretSanta.API.Helper
{
    public class EmailMessage
    {
        public EmailMessage()
        {
            ToEmails = new List<string>();            
        }
        public string Subject { get; set; }
        public string EmailBody { get; set; }
        public string FromEmail { get; set; }
        public List<string> ToEmails { get; set; }
        public bool IsHtmlEnable { get; set; }
        public string HTMLTemplatePath { get; set; }
    }
}
