using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using createsend_dotnet;

namespace Samples
{
    public class GeneralSamples
    {
        private AuthenticationDetails auth =
           new OAuthAuthenticationDetails(
               "your access token", "your refresh token");

        public void GetExternalSessionURL()
        {
            var general = new General(auth);

            try
            {
                ExternalSessionOptions externalSessionOptions = new ExternalSessionOptions
                {
                    Email = "test@notarealdomain.com",
                    Chrome = "None",
                    Url = "/subscribers/search?search=belle@example.com",
                    IntegratorID = "your integratorID",
                    ClientID = "your clientID"
                };

                string sessionUrl = general.ExternalSessionUrl(externalSessionOptions);
                Console.WriteLine(string.Format("Session created and accessible at URL: {0}", sessionUrl));
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
