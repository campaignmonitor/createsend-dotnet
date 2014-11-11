namespace createsend_dotnet
{
    public class ExternalSessionOptions
    {
        public string Email { get; set; }
        public string Chrome { get; set; }
        public string Url { get; set; }
        public string IntegratorID { get; set; }
        public string ClientID { get; set; }
    }

    public class ExternalSessionResult
    {
        public string SessionUrl { get; set; }
    }
}
