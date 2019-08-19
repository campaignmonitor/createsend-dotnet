using createsend_dotnet;
using createsend_dotnet.Models;
using System;
using System.Collections.Generic;

namespace Samples
{
    class JourneyEmailSamples
    {
        private AuthenticationDetails auth =
            new OAuthAuthenticationDetails(
                "your access token", "your refresh token");
        private string journeyEmailID = "your_journey_email_id";

        public void GetRecipients()
        {
            JourneyEmail journeyEmail = new JourneyEmail(auth, journeyEmailID);

            try
            {
                List<JourneyEmailRecipient> recipients = new List<JourneyEmailRecipient>();
                PagedCollection<JourneyEmailRecipient> firstPage = journeyEmail.Recipients(1, 10, "ASC");
                recipients.AddRange(firstPage.Results);

                if (firstPage.NumberOfPages > 1)
                {
                    for (int pageNumber = 2; pageNumber <= firstPage.NumberOfPages; pageNumber++)
                    {
                        PagedCollection<JourneyEmailRecipient> subsequentPage = journeyEmail.Recipients(pageNumber, 10, "ASC");
                        recipients.AddRange(subsequentPage.Results);
                    }
                }

                foreach (JourneyEmailRecipient recipient in recipients)
                {
                    Console.WriteLine(recipient.EmailAddress);
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

        public void GetOpens()
        {
            JourneyEmail journeyEmail = new JourneyEmail(auth, journeyEmailID);

            try
            {
                List<JourneyEmailOpenDetail> opens = new List<JourneyEmailOpenDetail>();
                PagedCollection<JourneyEmailOpenDetail> firstPage = journeyEmail.Opens(1, 50, "ASC");
                opens.AddRange(firstPage.Results);

                if (firstPage.NumberOfPages > 1)
                {
                    for (int pageNumber = 2; pageNumber <= firstPage.NumberOfPages; pageNumber++)
                    {
                        PagedCollection<JourneyEmailOpenDetail> subsequentPage = journeyEmail.Opens(pageNumber, 50, "ASC");
                        opens.AddRange(subsequentPage.Results);
                    }
                }

                foreach (JourneyEmailOpenDetail open in opens)
                {
                    Console.WriteLine(open.EmailAddress);
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

        public void GetUnsubscribes()
        {
            JourneyEmail journeyEmail = new JourneyEmail(auth, journeyEmailID);

            try
            {
                List<JourneyEmailUnsubscribeDetail> unsubscribes = new List<JourneyEmailUnsubscribeDetail>();
                PagedCollection<JourneyEmailUnsubscribeDetail> firstPage = journeyEmail.Unsubscribes(1, 50, "ASC");
                unsubscribes.AddRange(firstPage.Results);

                if (firstPage.NumberOfPages > 1)
                {
                    for (int pageNumber = 2; pageNumber <= firstPage.NumberOfPages; pageNumber++)
                    {
                        PagedCollection<JourneyEmailUnsubscribeDetail> subsequentPage = journeyEmail.Unsubscribes(pageNumber, 50, "ASC");
                        unsubscribes.AddRange(subsequentPage.Results);
                    }
                }

                foreach (JourneyEmailUnsubscribeDetail unsubscribe in unsubscribes)
                {
                    Console.WriteLine(unsubscribe.EmailAddress);
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

        public void GetClicks()
        {
            JourneyEmail journeyEmail = new JourneyEmail(auth, journeyEmailID);

            try
            {
                List<JourneyEmailClickDetail> clicks = new List<JourneyEmailClickDetail>();
                PagedCollection<JourneyEmailClickDetail> firstPage = journeyEmail.Clicks(1, 50, "ASC");
                clicks.AddRange(firstPage.Results);

                if (firstPage.NumberOfPages > 1)
                {
                    for (int pageNumber = 2; pageNumber <= firstPage.NumberOfPages; pageNumber++)
                    {
                        PagedCollection<JourneyEmailClickDetail> subsequentPage = journeyEmail.Clicks(pageNumber, 50, "ASC");
                        clicks.AddRange(subsequentPage.Results);
                    }
                }

                foreach (JourneyEmailClickDetail click in clicks)
                {
                    Console.WriteLine(click.EmailAddress);
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

        public void GetBounces()
        {
            JourneyEmail journeyEmail = new JourneyEmail(auth, journeyEmailID);

            try
            {
                List<JourneyEmailBounceDetail> bounces = new List<JourneyEmailBounceDetail>();
                PagedCollection<JourneyEmailBounceDetail> firstPage = journeyEmail.Bounces(1, 50, "ASC");
                bounces.AddRange(firstPage.Results);

                if (firstPage.NumberOfPages > 1)
                {
                    for (int pageNumber = 2; pageNumber <= firstPage.NumberOfPages; pageNumber++)
                    {
                        PagedCollection<JourneyEmailBounceDetail> subsequentPage = journeyEmail.Bounces(pageNumber, 50, "ASC");
                        bounces.AddRange(subsequentPage.Results);
                    }
                }

                foreach (JourneyEmailBounceDetail bounce in bounces)
                {
                    Console.WriteLine(bounce.EmailAddress);
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
    }
}
