using System.Collections.Specialized;

namespace createsend_dotnet
{
    public class Administrator : CreateSendBase
    {
        private string AdminsUrl { get { return string.Format("/admins.json"); } }
        
        public Administrator(AuthenticationDetails auth) : base(auth) { }

        public AdministratorDetails Details(string emailAddress)
        {
            return HttpGet<AdministratorDetails>(
                AdminsUrl, new NameValueCollection {{"email", emailAddress}});
        }

        public string Add(AdministratorDetails admin)
        {
            return HttpPost<AdministratorDetails, AdministratorResult>(
                AdminsUrl, null, admin).EmailAddress;
        }

        public string Update(string emailAddress, AdministratorDetails admin)
        {
            return HttpPut<AdministratorDetails, AdministratorResult>(
                AdminsUrl, new NameValueCollection {{ "email", emailAddress }},
                admin).EmailAddress;
        }

        public void Delete(string emailAddress)
        {
            HttpDelete(AdminsUrl, new NameValueCollection {{"email", emailAddress}});
        }
    }
}
