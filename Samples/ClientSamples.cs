using createsend_dotnet;
using System;
using System.Collections.Generic;

namespace Samples
{
    public class ClientSamples
    {
        private AuthenticationDetails auth =
            new OAuthAuthenticationDetails(
                "your access token", "your refresh token");
        public string ClientID = "your client id";

        public void ListsForEmail(string email)
        {
            Client client = new Client(auth, ClientID);
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
            Client client = new Client(auth, ClientID);
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
            Client client = new Client(auth, ClientID);
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

        public void Journeys()
        {
            var client = new Client(auth, ClientID);
            var journeys = client.Journeys();

            foreach (var journey in journeys)
            {
                Console.WriteLine(journey.Name);
            }
        }

        public void SentCampaigns()
        {
            var client = new Client(auth, ClientID);
            try
            {
                var tags = "Tag1,Tag2";
                var sortDirection = "desc";
                var pageNumber = 1;
                var pageSize = 100;
                var campaigns = client.Campaigns(tags, pageNumber, pageSize, sortDirection, new DateTime(1900, 1, 1), DateTime.Now);
                var numberOfPages = campaigns.NumberOfPages;
                Console.WriteLine($"Total Pages: {numberOfPages}");
                Console.WriteLine("----------");

                while (pageNumber <= numberOfPages)
                {
                    if (pageNumber > 1)
                    {
                        campaigns = client.Campaigns(tags, pageNumber, pageSize, sortDirection, null, null);
                    }

                    foreach (var campaign in campaigns.Results)
                    {
                        Console.WriteLine($"FromName: {campaign.FromName}");
                        Console.WriteLine($"FromEmail: {campaign.FromEmail}");
                        Console.WriteLine($"ReplyTo: {campaign.ReplyTo}");
                        Console.WriteLine($"WebVersionURL: {campaign.WebVersionURL}");
                        Console.WriteLine($"WebVersionTextURL: {campaign.WebVersionTextURL}");
                        Console.WriteLine($"CampaignID: {campaign.CampaignID}");
                        Console.WriteLine($"Subject: {campaign.Subject}");
                        Console.WriteLine($"Name: {campaign.Name}");
                        Console.WriteLine($"SentDate: {campaign.SentDate}");
                        Console.WriteLine($"TotalRecipients: {campaign.TotalRecipients}");
                        Console.WriteLine($"Tags: {string.Join(",", campaign.Tags)}");
                        Console.WriteLine("----------");
                    }
                    pageNumber++;
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

        public void Tags()
        {
            var client = new Client(auth, ClientID);
            try
            {
                var tags = client.Tags();
                Console.WriteLine("----------");

                foreach (var tag in tags)
                {
                    Console.WriteLine($"TagName: {tag.Name}");
                    Console.WriteLine($"NumberOfCampaigns: {tag.NumberOfCampaigns}");
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
    }
}
