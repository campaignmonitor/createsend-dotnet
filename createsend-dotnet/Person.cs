using System.Collections.Generic;
using System.Collections.Specialized;

namespace createsend_dotnet
{
    public class Person
    {
        private readonly string clientID;

        public Person(string clientID)
        {
            this.clientID = clientID;
        }

        private string PeopleUrl { get { return string.Format("/clients/{0}/people.json", clientID); }}

        public IEnumerable<PersonDetails> All()
        {
            return HttpHelper.Get<IEnumerable<PersonDetails>>(PeopleUrl, null);
        }


        public PersonDetails Details(string emailAddress)
        {
            return HttpHelper.Get<PersonDetails>(PeopleUrl,
                                                 new NameValueCollection {{"email", emailAddress}});
        }

        public string Add(PersonDetails person)
        {
            return HttpHelper.Post<PersonDetails, PersonResult>(PeopleUrl, null, person).EmailAddress;
        }

        public string Update(string emailAddress, PersonDetails person)
        {
            return
                HttpHelper.Put<PersonDetails, PersonResult>(PeopleUrl, new NameValueCollection {{"email", emailAddress}},
                                                            person).EmailAddress;
        }

        public void Delete(string emailAddress)
        {
            HttpHelper.Delete(PeopleUrl,
                              new NameValueCollection {{"email", emailAddress}});
        }
    }
}
