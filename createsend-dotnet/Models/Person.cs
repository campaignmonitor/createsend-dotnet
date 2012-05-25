namespace createsend_dotnet
{
    public class PersonDetails
    {
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public int AccessLevel { get; set; }
        public string Password { get; set; }
    }

    public class PersonResult
    {
        public string EmailAddress { get; set; }
    }
}
