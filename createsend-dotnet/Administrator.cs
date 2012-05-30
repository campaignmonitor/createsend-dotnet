using System.Collections.Specialized;

namespace createsend_dotnet
{
    public class Administrator
    {
        private string AdminsUrl { get { return string.Format("/admins"); }}  

        public AdministratorDetails Details(string emailAddress)
        {
            return HttpHelper.Get<AdministratorDetails>(AdminsUrl,
                                                 new NameValueCollection {{"email", emailAddress}});
        }

        public string Add(AdministratorDetails admin)
        {
            return HttpHelper.Post<AdministratorDetails, AdministratorResult>(AdminsUrl, null, admin).EmailAddress;
        }

        public string Update(string emailAddress, AdministratorDetails admin)
        {
            return
                HttpHelper.Put<AdministratorDetails, AdministratorResult>(AdminsUrl, new NameValueCollection {{"email", emailAddress}},
                                                            admin).EmailAddress;
        }

        public void Delete(string emailAddress)
        {
            HttpHelper.Delete(AdminsUrl,
                              new NameValueCollection {{"email", emailAddress}});
        }
    }
}
