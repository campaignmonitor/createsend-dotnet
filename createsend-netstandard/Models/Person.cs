namespace createsend_dotnet
{
    public class PersonDetails
    {
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public int AccessLevel { get; set; }
        /// <summary>
        /// status is only used when retrieving a person or people
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// password is only used when adding a person
        /// </summary>
        public string Password { get; set; }
    }

    public class PersonResult
    {
        public string EmailAddress { get; set; }
    }
}
