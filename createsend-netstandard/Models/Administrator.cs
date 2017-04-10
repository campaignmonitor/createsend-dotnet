namespace createsend_dotnet
{
    public class AdministratorDetails
    {
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// status is only used when retrieving an administrator (or administrators)
        /// </summary>
        public string Status { get; set; }
    }

    public class AdministratorResult
    {
        public string EmailAddress { get; set; }
    }
}
