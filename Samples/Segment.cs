using System;
using System.Collections.Generic;
using createsend_dotnet;

namespace Samples
{
    public class SegmentSamples
    {
        private AuthenticationDetails auth =
            new OAuthAuthenticationDetails(
                "your access token", "your refresh token");
        private string segmentID = "your_segment_id";

        public void GetActiveSubscribers()
        {
            Segment segment = new Segment(auth, segmentID);

            try
            {
                List<SubscriberDetail> allSubscribers = new List<SubscriberDetail>();

                // get the first page, with an old date to signify entire list
                // get the first page, with an old date to signify entire list
                PagedCollection<SubscriberDetail> firstPage = segment.Subscribers(new DateTime(1900, 1, 1), 1, 50, "Email", "ASC", false);

                allSubscribers.AddRange(firstPage.Results);

                if (firstPage.NumberOfPages > 1)
                {
                    for (int pageNumber = 2; pageNumber <= firstPage.NumberOfPages; pageNumber++)
                    {
                        PagedCollection<SubscriberDetail> subsequentPage = segment.Subscribers(new DateTime(1900, 1, 1), pageNumber, 50, "Email", "ASC", false);
                        allSubscribers.AddRange(subsequentPage.Results);
                    }
                }

                foreach (SubscriberDetail subscriberDetail in allSubscribers)
                {
                    Console.WriteLine(string.Format(
                        "Subscriber with email address {0} reads mail with {1}.",
                        subscriberDetail.EmailAddress, subscriberDetail.ReadsEmailWith));
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
