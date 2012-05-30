using System.Collections.Generic;
using System.Collections.Specialized;

namespace createsend_dotnet
{
    public class Account
    {
        public string GetPrimaryContact()
        {
            return HttpHelper.Get<PersonResult>("/primarycontact.json", null).EmailAddress;
        }

        public string SetPrimaryContact(string emailAddress)
        {
            return HttpHelper.Put<string, PersonResult>("/primarycontact.json", new NameValueCollection{{"email", emailAddress}}, null).EmailAddress;
        }

        public IEnumerable<AdministratorDetails> Administrators()
        {
            return HttpHelper.Get<IEnumerable<AdministratorDetails>>("/admins.json", null);
        }
    }
}
