using System;
using System.Collections.Generic;
using System.Text;
using createsend_dotnet;

namespace Samples
{
    public class ListSamples
    {
        private string listID = "your_list_id";

        void CreateCustomField()
        {
            List list = new List(listID);

            try
            {
                string newCustomFieldKey = list.CreateCustomField("NewCustomField", CustomFieldDataType.Text, null);
                Console.WriteLine(newCustomFieldKey);
            }
            catch (CreatesendException ex)
            {
                ErrorResult error = (ErrorResult)ex.Data["ErrorResult"];
                Console.WriteLine(error.Code);
                Console.WriteLine(error.Message);
            }
            catch (Exception ex)
            {
                //handle some other failure
            }
        }

        void CreateMultiOptionCustomField()
        {
            List list = new List(listID);

            try
            {
                List<string> options = new List<string>() { "Option 1", "Option 2", "Option 3" };
                string newCustomFieldKey = list.CreateCustomField("NewCustomField", CustomFieldDataType.MultiSelectOne, options);
                Console.WriteLine(newCustomFieldKey);
            }
            catch (CreatesendException ex)
            {
                ErrorResult error = (ErrorResult)ex.Data["ErrorResult"];
                Console.WriteLine(error.Code);
                Console.WriteLine(error.Message);
            }
            catch (Exception ex)
            {
                //handle some other failure
            }
        }

        void GetActiveSubscribers()
        {
            List list = new List(listID);

            try
            {
                List<SubscriberDetail> allSubscribers = new List<SubscriberDetail>();

                //get the first page, with an old date to signify entire list
                PagedCollection<SubscriberDetail> firstPage = list.Active(new DateTime(1900, 1, 1), 1, 50, "Email", "ASC");

                allSubscribers.AddRange(firstPage.Results);

                if(firstPage.NumberOfPages > 1)
                    for (int pageNumber = 2; pageNumber <= firstPage.NumberOfPages; pageNumber++)
                    {
                        PagedCollection<SubscriberDetail> subsequentPage = list.Active(new DateTime(1900, 1, 1), pageNumber, 50, "Email", "ASC");
                        allSubscribers.AddRange(subsequentPage.Results);
                    }

                //we can now do whatever with every subscriber
                foreach(SubscriberDetail subscriberDetail in allSubscribers)
                    Console.WriteLine(subscriberDetail.EmailAddress);
            }
            catch (CreatesendException ex)
            {
                ErrorResult error = (ErrorResult)ex.Data["ErrorResult"];
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
