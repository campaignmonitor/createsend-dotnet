using System;
using createsend_dotnet;

namespace Samples
{
    public class People
    {
        // see API documentation on where to get this value
        private string clientId = "your_client_id"; 

        public void Add()
        {
            var person = new Person(clientId);
           
            try
            {
                 var personDetails = new PersonDetails
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
                var error = (ErrorResult)ex.Data["ErrorResult"];
                Console.WriteLine(error.Code);
                Console.WriteLine(error.Message);
            }
            catch (Exception ex)
            {
                //handle some other failure
            }
        }

        public void Update()
        {
            var person = new Person(clientId);
           
            try
            {
                var personDetails = person.Details("test@notarealdomain.com");
                personDetails.Name = "test new name";

                person.Update("test@notarealdomain.com", personDetails);
            }
            catch (CreatesendException ex)
            {
                var error = (ErrorResult)ex.Data["ErrorResult"];
                Console.WriteLine(error.Code);
                Console.WriteLine(error.Message);
            }
            catch (Exception ex)
            {
                //handle some other failure
            }
        }

        public void List()
        {
            var client = new Client(clientId);
           
            try
            {
                foreach(var p in client.People())
                {
                    Console.Out.WriteLine("{0}", p.EmailAddress);
                    Console.Out.WriteLine("{0}", p.Name);
                    Console.Out.WriteLine("{0}", p.AccessLevel);
                }
            }
            catch (CreatesendException ex)
            {
                var error = (ErrorResult)ex.Data["ErrorResult"];
                Console.WriteLine(error.Code);
                Console.WriteLine(error.Message);
            }
            catch (Exception ex)
            {
                //handle some other failure
            }
        }

        public void Delete()
        {
            var person = new Person(clientId);
           
            try
            {
               person.Delete("test@notarealdomain.com");
            }
            catch (CreatesendException ex)
            {
                var error = (ErrorResult)ex.Data["ErrorResult"];
                Console.WriteLine(error.Code);
                Console.WriteLine(error.Message);
            }
            catch (Exception ex)
            {
                //handle some other failure
            }
        }

        public void SetPrimaryContact()
        {
            var client = new Client(clientId);
           
            try
            {
               client.SetPrimaryContact("test@notarealdomain.com");
            }
            catch (CreatesendException ex)
            {
                var error = (ErrorResult)ex.Data["ErrorResult"];
                Console.WriteLine(error.Code);
                Console.WriteLine(error.Message);
            }
            catch (Exception ex)
            {
                //handle some other failure
            }
        }

        public void GetPrimaryContact()
        {
            var client = new Client(clientId);
           
            try
            {
                Console.Out.WriteLine(client.GetPrimaryContact());
            }
            catch (CreatesendException ex)
            {
                var error = (ErrorResult)ex.Data["ErrorResult"];
                Console.WriteLine(error.Code);
                Console.WriteLine(error.Message);
            }
            catch (Exception ex)
            {
                //handle some other failure
            }
        }
    }
}
