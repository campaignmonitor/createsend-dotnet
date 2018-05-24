using System;
using System.Collections.Generic;
using System.Text;
using createsend_dotnet;

namespace Samples
{
    public class ListSamples
    {
        private AuthenticationDetails auth =
            new OAuthAuthenticationDetails(
                "your access token", "your refresh token");
        public string ListID = "your_list_id";

        public void GetDetails()
        {
            List list = new List(auth, ListID);
            try
            {
                ListDetail ld = list.Details();
                Console.WriteLine("List Details:\n-----");
                Console.WriteLine("ListID: {0}", ld.ListID);
                Console.WriteLine("Title: {0}", ld.Title);
                Console.WriteLine("ConfirmedOptIn: {0}", ld.ConfirmedOptIn);
                Console.WriteLine("ConfirmationSuccessPage: {0}", ld.ConfirmationSuccessPage);
                Console.WriteLine("UnsubscribePage: {0}", ld.UnsubscribePage);
                Console.WriteLine("UnsubscribeSetting: {0}", ld.UnsubscribeSetting);
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

        public void Update()
        {
            List list = new List(auth, ListID);
            try
            {
                list.Update(
                    "New Title",
                    "example.com/unsubscribe",
                    false,
                    "example.com/success",
                    UnsubscribeSetting.OnlyThisList,
                    false,
                    false);
                Console.WriteLine("List Successfully Updated.");
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

        public void CreateCustomField()
        {
            List list = new List(auth, ListID);

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
                // Handle some other failure
                Console.WriteLine(ex.ToString());
            }
        }

        public void CreateMultiOptionCustomField()
        {
            List list = new List(auth, ListID);

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
                // Handle some other failure
                Console.WriteLine(ex.ToString());
            }
        }

        public void GetCustomFields()
        {
            List list = new List(auth, ListID);
            try
            {
                var allFields = list.CustomFields();
                foreach (ListCustomField field in allFields)
                {
                    Console.WriteLine(string.Format(
                        "Field \"{0}\" of type {1}: {2}visible in the preference centre",
                        field.FieldName,
                        field.DataType,
                        field.VisibleInPreferenceCenter ? "" : "not "));
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

        public void GetActiveSubscribers()
        {
            List list = new List(auth, ListID);

            try
            {
                List<SubscriberDetail> allSubscribers = new List<SubscriberDetail>();

                // get the first page, with an old date to signify entire list
                PagedCollection<SubscriberDetail> firstPage = list.Active(new DateTime(1900, 1, 1), 1, 50, "Email", "ASC", false);

                allSubscribers.AddRange(firstPage.Results);

                if (firstPage.NumberOfPages > 1)
                {
                    for (int pageNumber = 2; pageNumber <= firstPage.NumberOfPages; pageNumber++)
                    {
                        PagedCollection<SubscriberDetail> subsequentPage = list.Active(new DateTime(1900, 1, 1), pageNumber, 50, "Email", "ASC", false);
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
