using System;
using System.Collections.Generic;
using System.Text;
using createsend_dotnet;
using Newtonsoft.Json;

namespace ConsoleRunner
{
    class Program
    {
        //testing - 011ebcadaeb71e9a
        //testing list - aa76d29949e7f10ab28712617634fd0b
        //apicreated  - 277da11f331fc698ad22a66c0c4b5c33
        //list in apicreated - 41a99346539316727de7f24491da29d6
        static void Main(string[] args)
        {
            List list = new List("aa76d29949e7f10ab28712617634fd0b");

            try
            {
                List<SubscriberDetail> allSubscribers = new List<SubscriberDetail>();

                //get the first page, with an old date to signify entire list
                PagedCollection<SubscriberDetail> firstPage = list.Active(new DateTime(1900, 1, 1), 1, 50, "Email", "ASC");

                allSubscribers.AddRange(firstPage.Results);

                if (firstPage.NumberOfPages > 1)
                    for (int pageNumber = 2; pageNumber <= firstPage.NumberOfPages; pageNumber++)
                    {
                        PagedCollection<SubscriberDetail> subsequentPage = list.Active(new DateTime(1900, 1, 1), pageNumber, 50, "Email", "ASC");
                        allSubscribers.AddRange(subsequentPage.Results);
                    }

                //we can now do whatever with every subscriber
                foreach (SubscriberDetail subscriberDetail in allSubscribers)
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
