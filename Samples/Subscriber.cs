using System;
using System.Collections.Generic;
using System.Text;
using createsend_dotnet;

namespace Samples
{
    public class SubscriberSamples
    {
        private AuthenticationDetails auth =
            new OAuthAuthenticationDetails(
                "your access token", "your refresh token");
        private string listID = "your_list_id";

        void BasicAdd()
        {
            Subscriber subscriber = new Subscriber(auth, listID);

            try
            {
                string newSubscriberID = subscriber.Add("test@notarealdomain.com", "Test Name", null, false);
                Console.WriteLine(newSubscriberID);
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

        void AddWithCustomFields()
        {
            Subscriber subscriber = new Subscriber(auth, listID);

            try
            {
                List<SubscriberCustomField> customFields = new List<SubscriberCustomField>();
                customFields.Add(new SubscriberCustomField() { Key = "CustomFieldKey", Value = "Value" });
                customFields.Add(new SubscriberCustomField() { Key = "CustomFieldKey2", Value = "Value2" });

                string newSubscriberID = subscriber.Add("test@notarealdomain.com", "Test Name", customFields, false);
                Console.WriteLine(newSubscriberID);
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

        /// <summary>
        /// Updates the subscriber with current email address of "test@notarealdomain.com" to have the new email
        /// "new_address@notarealdomain.com", while leaving the name unchanged.
        /// Also clears the value of CustomFieldKey
        /// </summary>
        void UpdateWithNewEmailAndUnchangedNameDontResubscribe()
        {
            Subscriber subscriber = new Subscriber(auth, listID);

            try
            {
                List<SubscriberCustomField> customFields = new List<SubscriberCustomField>();
                customFields.Add(new SubscriberCustomField() { Key = "CustomFieldKey", Clear = true });
                customFields.Add(new SubscriberCustomField() { Key = "CustomFieldKey2", Value = "Value2" });

                subscriber.Update("test@notarealdomain.com", "new_address@notarealdomain.com", null, customFields, false);
                Console.WriteLine("Subscriber Updated successfully with new email: new_address@notarealdomain.com. Name was unchanged");
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

        void BatchAdd()
        {
            Subscriber subscriber = new Subscriber(auth, listID);

            List<SubscriberDetail> newSubscribers = new List<SubscriberDetail>();

            SubscriberDetail subscriber1 = new SubscriberDetail("test1@notarealdomain.com", "Test Person 1", new List<SubscriberCustomField>());
            subscriber1.CustomFields.Add(new SubscriberCustomField() { Key = "CustomFieldKey", Value = "Value" });
            subscriber1.CustomFields.Add(new SubscriberCustomField() { Key = "CustomFieldKey2", Value = "Value2" });

            newSubscribers.Add(subscriber1);

            SubscriberDetail subscriber2 = new SubscriberDetail("test2@notarealdomain.com", "Test Person 2", new List<SubscriberCustomField>());
            subscriber2.CustomFields.Add(new SubscriberCustomField() { Key = "CustomFieldKey", Value = "Value3" });
            subscriber2.CustomFields.Add(new SubscriberCustomField() { Key = "CustomFieldKey2", Value = "Value4" });

            newSubscribers.Add(subscriber2);

            try
            {
                BulkImportResults results = subscriber.Import(newSubscribers, true);
                Console.WriteLine(results.TotalNewSubscribers + " subscribers added");
                Console.WriteLine(results.TotalExistingSubscribers + " total subscribers in list");
            }
            catch (CreatesendException ex)
            {
                ErrorResult<BulkImportResults> error = (ErrorResult<BulkImportResults>)ex.Data["ErrorResult"];

                Console.WriteLine(error.Code);
                Console.WriteLine(error.Message);

                if (error.ResultData != null)
                {
                    //handle the returned data
                    BulkImportResults results = error.ResultData;

                    //success details are here as normal
                    Console.WriteLine(results.TotalNewSubscribers + " subscribers were still added");

                    //but we also have additional failure detail
                    foreach (ImportResult result in results.FailureDetails)
                    {
                        Console.WriteLine("Failed Address");
                        Console.WriteLine(result.Message + " - " + result.EmailAddress);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle some other failure
                Console.WriteLine(ex.ToString());
            }
        }

        void DeleteSubscriber()
        {
            Subscriber subscriber = new Subscriber(auth, listID);
            subscriber.Delete("test1@notarealdomain.com");
        }
    }
}
