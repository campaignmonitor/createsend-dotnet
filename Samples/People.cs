using System;
using createsend_dotnet;

namespace Samples
{
    public class People
    {
        private AuthenticationDetails auth =
            new OAuthAuthenticationDetails(
                "your access token", "your refresh token");
        private string clientId = "your client id";

        public void Add()
        {
            Person person = new Person(auth, clientId);
           
            try
            {
                 PersonDetails personDetails = new PersonDetails
                                {
                                    EmailAddress = "test@notarealdomain.com",
                                    Name = "test",
                                    AccessLevel = 1023,
                                    Password = "somepassword"
                                };

                person.Add(personDetails);
            }
            catch (CreatesendException ex)
            {
                ErrorResult error = (ErrorResult)ex.Data["ErrorResult"];
                Console.WriteLine(error.Code);
                Console.WriteLine(error.Message);
            }
            catch (Exception ex)
            {
                // Handle some other failure
                Console.WriteLine(ex.ToString());
            }
        }

        public void Update()
        {
            Person person = new Person(auth, clientId);
           
            try
            {
                PersonDetails personDetails = person.Details("test@notarealdomain.com");
                personDetails.Name = "test new name";

                person.Update("test@notarealdomain.com", personDetails);
            }
            catch (CreatesendException ex)
            {
                ErrorResult error = (ErrorResult)ex.Data["ErrorResult"];
                Console.WriteLine(error.Code);
                Console.WriteLine(error.Message);
            }
            catch (Exception ex)
            {
                // Handle some other failure
                Console.WriteLine(ex.ToString());
            }
        }

        public void List()
        {
            Client client = new Client(auth, clientId);
           
            try
            {
                foreach(PersonDetails p in client.People())
                {
                    Console.Out.WriteLine("{0}", p.EmailAddress);
                    Console.Out.WriteLine("{0}", p.Name);
                    Console.Out.WriteLine("{0}", p.AccessLevel);
                }
            }
            catch (CreatesendException ex)
            {
                ErrorResult error = (ErrorResult)ex.Data["ErrorResult"];
                Console.WriteLine(error.Code);
                Console.WriteLine(error.Message);
            }
            catch (Exception ex)
            {
                // Handle some other failure
                Console.WriteLine(ex.ToString());
            }
        }

        public void Delete()
        {
            Person person = new Person(auth, clientId);
           
            try
            {
               person.Delete("test@notarealdomain.com");
            }
            catch (CreatesendException ex)
            {
                ErrorResult error = (ErrorResult)ex.Data["ErrorResult"];
                Console.WriteLine(error.Code);
                Console.WriteLine(error.Message);
            }
            catch (Exception ex)
            {
                // Handle some other failure
                Console.WriteLine(ex.ToString());
            }
        }

        public void SetPrimaryContact()
        {
            Client client = new Client(auth, clientId);
           
            try
            {
               client.SetPrimaryContact("test@notarealdomain.com");
            }
            catch (CreatesendException ex)
            {
                ErrorResult error = (ErrorResult)ex.Data["ErrorResult"];
                Console.WriteLine(error.Code);
                Console.WriteLine(error.Message);
            }
            catch (Exception ex)
            {
                // Handle some other failure
                Console.WriteLine(ex.ToString());
            }
        }

        public void GetPrimaryContact()
        {
            Client client = new Client(auth, clientId);
           
            try
            {
                Console.Out.WriteLine(client.GetPrimaryContact());
            }
            catch (CreatesendException ex)
            {
                ErrorResult error = (ErrorResult)ex.Data["ErrorResult"];
                Console.WriteLine(error.Code);
                Console.WriteLine(error.Message);
            }
            catch (Exception ex)
            {
                // Handle some other failure
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
