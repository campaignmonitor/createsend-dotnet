using System;
using createsend_dotnet;

namespace Samples
{
    public class Administrators
    {
        private AuthenticationDetails auth =
            new OAuthAuthenticationDetails(
                "your access token", "your refresh token");

        public void Add()
        {
            Administrator administrator = new Administrator(auth);

            try
            {
                 AdministratorDetails administratorDetails = new AdministratorDetails
                                {
                                    EmailAddress = "test@notarealdomain.com",
                                    Name = "test",
                                };

                administrator.Add(administratorDetails);
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
            Administrator administrator = new Administrator(auth);
           
            try
            {
                AdministratorDetails administratorDetails = administrator.Details("test@notarealdomain.com");
                administratorDetails.Name = "test new name";

                administrator.Update("test@notarealdomain.com", administratorDetails);
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

        public void List()
        {
            Account account = new Account(auth);
           
            try
            {
                foreach(AdministratorDetails p in account.Administrators())
                {
                    Console.Out.WriteLine("{0}", p.EmailAddress);
                    Console.Out.WriteLine("{0}", p.Name);
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

        public void Delete()
        {
            Administrator administrator = new Administrator(auth);
           
            try
            {
               administrator.Delete("test@notarealdomain.com");
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

        public void SetPrimaryContact()
        {
            Account account = new Account(auth);
           
            try
            {
               account.SetPrimaryContact("test@notarealdomain.com");
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

        public void GetPrimaryContact()
        {
            Account account = new Account(auth);
           
            try
            {
                Console.Out.WriteLine(account.GetPrimaryContact());
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
