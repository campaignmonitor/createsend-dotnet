using createsend_dotnet;
using createsend_dotnet.Models;
using System;

namespace Samples
{
    class JourneySamples
    {
        private AuthenticationDetails auth =
            new OAuthAuthenticationDetails(
                "your access token", "your refresh token");
        private string journeyID = "your_journey_id";

        public void GetSummary()
        {
            Journey journey = new Journey(auth, journeyID);

            try
            {
                JourneySummary summary = journey.Summary();
                Console.WriteLine(summary.Name);
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
