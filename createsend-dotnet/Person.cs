using System.Collections.Specialized;

namespace createsend_dotnet
{
    public class Person : CreateSendBase
    {
        private readonly string clientID;

        public Person(AuthenticationDetails auth, string clientID)
            : base(auth)
        {
            this.clientID = clientID;
        }

        private string PeopleUrl { get { return string.Format("/clients/{0}/people.json", clientID); }}        

        public PersonDetails Details(string emailAddress)
        {
            return HttpGet<PersonDetails>(
                PeopleUrl, new NameValueCollection {{"email", emailAddress}});
        }

        public string Add(PersonDetails person)
        {
            return HttpPost<PersonDetails, PersonResult>(
                PeopleUrl, null, person).EmailAddress;
        }

        public string Update(string emailAddress, PersonDetails person)
        {
            return HttpPut<PersonDetails, PersonResult>(
                PeopleUrl, new NameValueCollection {{"email", emailAddress}},
                person).EmailAddress;
        }

        public void Delete(string emailAddress)
        {
            HttpDelete(PeopleUrl,
                new NameValueCollection {{"email", emailAddress}});
        }
    }
}
