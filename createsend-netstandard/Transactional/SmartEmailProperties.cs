
namespace createsend_dotnet.Transactional
{
    public class SmartEmailProperties
    {
        public EmailAddress From { get; set; }
        public EmailAddress ReplyTo { get; set; }
        public string Subject { get; set; }
        public PropertyContent Content { get; set; }
        public string TextPreviewUrl { get; set; }
        public string HtmlPreviewUrl { get; set; }
    }
}
