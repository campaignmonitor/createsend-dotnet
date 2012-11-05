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

        void CreateFromTemplate()
        {
            try
            {
                List<string> listIDs = new List<string>();
                listIDs.Add("List ID One");
                List<string> segmentIDs = new List<string>();
                segmentIDs.Add("Segment ID One");

                // Prepare the template content
                TemplateContent templateContent = new TemplateContent();

                List<EditableField> singlelines = new List<EditableField>();
                EditableField singleline = new EditableField();
                singleline.Content = "This is a heading";
                singleline.Href = "http://example.com/";
                singlelines.Add(singleline);
                templateContent.Singlelines = singlelines;
                
                List<EditableField> multilines = new List<EditableField>();
                EditableField multiline = new EditableField();
                multiline.Content = "<p>This is example</p><p>multiline <a href=\"http://example.com\">content</a>...</p>";
                multilines.Add(multiline);
                templateContent.Multilines = multilines;
                
                List<EditableField> images = new List<EditableField>();
                EditableField image = new EditableField();
                image.Content = "http://example.com/image.png";
                image.Alt = "This is alt text for an image";
                image.Href = "http://example.com/";
                images.Add(image);
                templateContent.Images = images;

                List<Repeater> repeaters = new List<Repeater>();
                Repeater repeater = new Repeater();
                List<RepeaterItem> items = new List<RepeaterItem>();
                RepeaterItem item = new RepeaterItem();
                item.Layout = "My layout";

                // Just using the same data for Singlelines, Multilines,
                // and Images as above in this example.
                item.Singlelines = singlelines;
                item.Multilines = multilines;
                item.Images = images;

                repeater.Items = items;
                repeaters.Add(repeater);
                templateContent.Repeaters = repeaters;

                // templateContent as defined above would be used to fill the content of
                // a template with markup similar to the following:
                // <html>
                // <head><title>My Template</title></head>
                // <body>
                //     <p><singleline>Enter heading...</singleline></p>
                //     <div><multiline>Enter description...</multiline></div>
                //     <img id="header-image" editable="true" width="500" />
                //     <repeater>
                //     <layout label="My layout">
                //         <div class="repeater-item">
                //         <p><singleline></singleline></p>
                //         <div><multiline></multiline></div>
                //         <img editable="true" width="500" />
                //         </div>
                //     </layout>
                //     </repeater>
                //     <p><unsubscribe>Unsubscribe</unsubscribe></p>
                // </body>
                // </html>     

                string campaignID = Campaign.CreateFromTemplate(
                    "Your Client ID",
                    "Campaign Subject",
                    "Campaign Name",
                    "From Name",
                    "example@example.com",
                    "example@example.com",
                    listIDs,
                    segmentIDs,
                    "Template ID",
                    templateContent);

                Console.WriteLine("Campaign ID: " + campaignID);
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

        public void GetEmailClientUsage()
        {
            Campaign campaign = new Campaign(campaignID);

            try
            {
                var usage = campaign.EmailClientUsage();
                foreach (EmailClient client in usage)
                    Console.WriteLine("{0} ({1}): used by %{2} ({3} subscribers)",
                        client.Client, client.Version, client.Percentage, client.Subscribers);
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
