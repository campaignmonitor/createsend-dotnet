using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using createsend_dotnet;

namespace Samples
{
    public class ClientSamples
    {
        public string ClientID = "your client id";

        public void ListsForEmail(string email)
        {
            Client client = new Client(ClientID);
            try
            {
                IEnumerable<ListForEmail> lists = client.ListsForEmail(email);
                Console.WriteLine("Subscriber with email address {0} is on the following lists:\n-----",
                    email);
                foreach (ListForEmail l in lists)
                {
                    Console.WriteLine("ListID: {0}", l.ListID);
                    Console.WriteLine("ListName: {0}", l.ListName);
                    Console.WriteLine("SubscriberState: {0}", l.SubscriberState);
                    Console.WriteLine("DateSubscriberAdded: {0}", l.DateSubscriberAdded);
                    Console.WriteLine("----------");
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
                Console.WriteLine(ex.ToString());
            }
        }

        public void Suppress(string[] emails)
        {
            Client client = new Client(ClientID);
            try
            {
                client.Suppress(emails);
                Console.WriteLine("Successfully suppressed email addresses provided.");
            }
            catch (CreatesendException ex)
            {
                ErrorResult error = (ErrorResult)ex.Data["ErrorResult"];
                Console.WriteLine(error.Code);
                Console.WriteLine(error.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Unsuppress(string email)
        {
            Client client = new Client(ClientID);
            try
            {
                client.Unsuppress(email);
                Console.WriteLine("Successfully unsuppressed {0}", email);
            }
            catch (CreatesendException ex)
            {
                ErrorResult error = (ErrorResult)ex.Data["ErrorResult"];
                Console.WriteLine(error.Code);
                Console.WriteLine(error.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
