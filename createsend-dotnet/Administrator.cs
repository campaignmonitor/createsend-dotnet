using System.Collections.Specialized;

namespace createsend_dotnet
{
    public class Administrator
    {
        public string ApiKey { get; set; }

        private CreateSendCredentials AuthCredentials
        {
            get { return new CreateSendCredentials(ApiKey != null ? ApiKey : CreateSendOptions.ApiKey, "x"); }
        }

        private string AdminsUrl { get { return string.Format("/admins.json"); }}  

        public AdministratorDetails Details(string emailAddress)
        {
            return HttpHelper.Get<AdministratorDetails>(AuthCredentials, AdminsUrl,
                                                 new NameValueCollection {{"email", emailAddress}});
        }

        public string Add(AdministratorDetails admin)
        {
            return HttpHelper.Post<AdministratorDetails, AdministratorResult>(AuthCredentials, AdminsUrl, null, admin).EmailAddress;
        }

        public string Update(string emailAddress, AdministratorDetails admin)
        {
            return
                HttpHelper.Put<AdministratorDetails, AdministratorResult>(AuthCredentials, AdminsUrl, new NameValueCollection {{"email", emailAddress}},
                                                            admin).EmailAddress;
        }

        public void Delete(string emailAddress)
        {
            HttpHelper.Delete(AuthCredentials, AdminsUrl,
                              new NameValueCollection {{"email", emailAddress}});
        }
    }
}
