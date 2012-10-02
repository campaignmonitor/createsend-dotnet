using System.Collections.Specialized;

namespace createsend_dotnet
{
    public class Person
    {
        public string ApiKey { get; set; }

        private CreateSendCredentials AuthCredentials
        {
            get { return new CreateSendCredentials(ApiKey != null ? ApiKey : CreateSendOptions.ApiKey, "x"); }
        }

        private readonly string clientID;

        public Person(string clientID)
        {
            this.clientID = clientID;
        }

        private string PeopleUrl { get { return string.Format("/clients/{0}/people.json", clientID); }}        

        public PersonDetails Details(string emailAddress)
        {
            return HttpHelper.Get<PersonDetails>(AuthCredentials, PeopleUrl,
                                                 new NameValueCollection {{"email", emailAddress}});
        }

        public string Add(PersonDetails person)
        {
            return HttpHelper.Post<PersonDetails, PersonResult>(AuthCredentials, PeopleUrl, null, person).EmailAddress;
        }

        public string Update(string emailAddress, PersonDetails person)
        {
            return
                HttpHelper.Put<PersonDetails, PersonResult>(AuthCredentials, PeopleUrl, new NameValueCollection {{"email", emailAddress}},
                                                            person).EmailAddress;
        }

        public void Delete(string emailAddress)
        {
            HttpHelper.Delete(AuthCredentials, PeopleUrl,
                              new NameValueCollection {{"email", emailAddress}});
        }
    }
}
