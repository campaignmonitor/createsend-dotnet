using System;
using System.Collections.Generic;
using createsend_dotnet;
using createsend_dotnet.Models;

namespace Samples
{
    public class JourneyEmailSamples
    {
        private readonly AuthenticationDetails auth =
            new OAuthAuthenticationDetails(
                "your access token", "your refresh token");
        private const string JourneyEmailID = "your_journey_email_id";

        public void GetRecipients()
        {
            var journeyEmail = new JourneyEmail(auth, JourneyEmailID);

            try
            {
                var recipients = new List<JourneyEmailRecipient>();
                var firstPage = journeyEmail.Recipients(1, 10, "ASC");
                recipients.AddRange(firstPage.Results);

                if (firstPage.NumberOfPages > 1)
                {
                    for (var pageNumber = 2; pageNumber <= firstPage.NumberOfPages; pageNumber++)
                    {
                        var subsequentPage = journeyEmail.Recipients(pageNumber, 10, "ASC");
                        recipients.AddRange(subsequentPage.Results);
                    }
                }

                foreach (var recipient in recipients)
                {
                    Console.WriteLine(recipient.EmailAddress);
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
                // Handle some other failure
                Console.WriteLine(ex.ToString());
            }
        }

        public void GetOpens()
        {
            var journeyEmail = new JourneyEmail(auth, JourneyEmailID);

            try
            {
                var opens = new List<JourneyEmailOpenDetail>();
                var firstPage = journeyEmail.Opens(1, 50, "ASC");
                opens.AddRange(firstPage.Results);

                if (firstPage.NumberOfPages > 1)
                {
                    for (var pageNumber = 2; pageNumber <= firstPage.NumberOfPages; pageNumber++)
                    {
                        var subsequentPage = journeyEmail.Opens(pageNumber, 50, "ASC");
                        opens.AddRange(subsequentPage.Results);
                    }
                }

                foreach (var open in opens)
                {
                    Console.WriteLine(open.EmailAddress);
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
                // Handle some other failure
                Console.WriteLine(ex.ToString());
            }
        }

        public void GetUnsubscribes()
        {
            var journeyEmail = new JourneyEmail(auth, JourneyEmailID);

            try
            {
                var unsubscribes = new List<JourneyEmailUnsubscribeDetail>();
                var firstPage = journeyEmail.Unsubscribes(1, 50, "ASC");
                unsubscribes.AddRange(firstPage.Results);

                if (firstPage.NumberOfPages > 1)
                {
                    for (var pageNumber = 2; pageNumber <= firstPage.NumberOfPages; pageNumber++)
                    {
                        var subsequentPage = journeyEmail.Unsubscribes(pageNumber, 50, "ASC");
                        unsubscribes.AddRange(subsequentPage.Results);
                    }
                }

                foreach (var unsubscribe in unsubscribes)
                {
                    Console.WriteLine(unsubscribe.EmailAddress);
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
                // Handle some other failure
                Console.WriteLine(ex.ToString());
            }
        }

        public void GetClicks()
        {
            var journeyEmail = new JourneyEmail(auth, JourneyEmailID);

            try
            {
                var clicks = new List<JourneyEmailClickDetail>();
                var firstPage = journeyEmail.Clicks(1, 50, "ASC");
                clicks.AddRange(firstPage.Results);

                if (firstPage.NumberOfPages > 1)
                {
                    for (var pageNumber = 2; pageNumber <= firstPage.NumberOfPages; pageNumber++)
                    {
                        var subsequentPage = journeyEmail.Clicks(pageNumber, 50, "ASC");
                        clicks.AddRange(subsequentPage.Results);
                    }
                }

                foreach (var click in clicks)
                {
                    Console.WriteLine(click.EmailAddress);
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
                // Handle some other failure
                Console.WriteLine(ex.ToString());
            }
        }

        public void GetBounces()
        {
            var journeyEmail = new JourneyEmail(auth, JourneyEmailID);

            try
            {
                var bounces = new List<JourneyEmailBounceDetail>();
                var firstPage = journeyEmail.Bounces(1, 50, "ASC");
                bounces.AddRange(firstPage.Results);

                if (firstPage.NumberOfPages > 1)
                {
                    for (var pageNumber = 2; pageNumber <= firstPage.NumberOfPages; pageNumber++)
                    {
                        var subsequentPage = journeyEmail.Bounces(pageNumber, 50, "ASC");
                        bounces.AddRange(subsequentPage.Results);
                    }
                }

                foreach (var bounce in bounces)
                {
                    Console.WriteLine(bounce.EmailAddress);
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
                // Handle some other failure
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
