using System;
using System.Collections.Generic;
using System.Text;
using createsend_dotnet;

namespace Samples
{
    class CampaignSamples
    {
        private string campaignID = "your_campaign_id";

        void GetRecipients()
        {
            Campaign campaign = new Campaign(campaignID);

            try
            {
                List<CampaignRecipient> recipients = new List<CampaignRecipient>();
                PagedCollection<CampaignRecipient> firstPage = campaign.Recipients(1, 50, "email", "ASC");
                recipients.AddRange(firstPage.Results);

                if (firstPage.NumberOfPages > 1)
                    for (int pageNumber = 2; pageNumber <= firstPage.NumberOfPages; pageNumber++)
                    {
                        PagedCollection<CampaignRecipient> subsequentPage = campaign.Recipients(pageNumber, 50, "email", "ASC");
                        recipients.AddRange(subsequentPage.Results);
                    }

                foreach (CampaignRecipient recipient in recipients)
                    Console.WriteLine(recipient.EmailAddress);
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
