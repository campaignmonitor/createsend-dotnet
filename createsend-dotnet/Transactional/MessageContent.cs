
namespace createsend_dotnet.Transactional
{
    public class MessageContent
    {
        public EmailAddress From { get; set; }
        public EmailAddress ReplyTo { get; set; }
        public string Subject { get; set; }
        public EmailAddress[] To { get; set; }
        public EmailAddress[] CC { get; set; }
        public EmailAddress BCC { get; set; }
        public MessageBody Body { get; set; }
    }
}
