using System;
using createsend_dotnet;
using createsend_dotnet.Transactional;

namespace Samples
{
    public class Transactional
    {
        private readonly IAgencyTransactional accountTransationalContext = Authenticate.ByAccountApiKey("your account api key");
        private readonly ITransactional clientTransationalContext = Authenticate.ByClientApiKey("your client api key");

        public void SendClassicEmailUsingAccountAPIKey()
        {
            try
            {
                var result = accountTransationalContext.ClassicEmail.Send("your client id", "no-reply@abcwidgets.com", "Tranasactional Classic Email",
                    "<html><body>TEST EMAIL</body></html>", "TEST EMAIL", ConsentToTrack.Yes, to: "example@abcwidgets.com");

                foreach (var recipientStatus in result.Response)
                {
                    Console.WriteLine($"{recipientStatus.Recipient.Email}:{recipientStatus.Status}");
                }
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

        private void SendClassicEmailUsinClientAPIKey()
        {
            try
            {
                var result = clientTransationalContext.ClassicEmail.Send("no-reply@abcwidgets.com", "Tranasactional Classic Email",
                    "<html><body>TEST EMAIL</body></html>", "TEST EMAIL", ConsentToTrack.Yes, to: "example@abcwidgets.com");

                foreach (var recipientStatus in result.Response)
                {
                    Console.WriteLine($"{recipientStatus.Recipient.Email}:{recipientStatus.Status}");
                }
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
