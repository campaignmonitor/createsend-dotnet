using System;
using createsend_dotnet;

namespace Samples
{
    public class JourneySamples
    {
        private readonly AuthenticationDetails auth =
            new OAuthAuthenticationDetails(
                "your access token", "your refresh token");
        private const string JourneyID = "your_journey_id";

        public void GetSummary()
        {
            var journey = new Journey(auth, JourneyID);

            try
            {
                var summary = journey.Summary();
                Console.WriteLine(summary.Name);
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
