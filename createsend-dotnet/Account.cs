using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;

namespace createsend_dotnet
{
    public class Account
    {
        public string ApiKey { get; set; }

        private NetworkCredential AuthCredentials
        {
            get { return new NetworkCredential(ApiKey != null ? ApiKey : CreateSendOptions.ApiKey, "x"); }
        }

        public string GetPrimaryContact()
        {
            return HttpHelper.Get<PersonResult>(AuthCredentials, "/primarycontact.json", null).EmailAddress;
        }

        public string SetPrimaryContact(string emailAddress)
        {
            return HttpHelper.Put<string, PersonResult>(AuthCredentials, "/primarycontact.json", new NameValueCollection{{"email", emailAddress}}, null).EmailAddress;
        }

        public IEnumerable<AdministratorDetails> Administrators()
        {
            return HttpHelper.Get<IEnumerable<AdministratorDetails>>(AuthCredentials, "/admins.json", null);
        }
    }
}
