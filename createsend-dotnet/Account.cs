using System.Collections.Generic;
using System.Collections.Specialized;

namespace createsend_dotnet
{
    public class Account : CreateSendBase
    {
        public Account(AuthenticationDetails auth) 
            : base(auth)
        {
        }

        public string GetPrimaryContact()
        {
            return HttpGet<PersonResult>("/primarycontact.json", null)
                .EmailAddress;
        }

        public string SetPrimaryContact(string emailAddress)
        {
            return HttpPut<string, PersonResult>("/primarycontact.json",
                new NameValueCollection { { "email", emailAddress } }, null)
                .EmailAddress;
        }

        public IEnumerable<AdministratorDetails> Administrators()
        {
            return HttpGet<IEnumerable<AdministratorDetails>>(
                "/admins.json", null);
        }
    }
}
