using System;
using createsend_dotnet;

namespace Samples
{
    public class Administrators
    {
        public void Add()
        {
            var administrator = new Administrator();
           
            try
            {
                 var administratorDetails = new AdministratorDetails
                                {
                                    EmailAddress = "test@notarealdomain.com",
                                    Name = "test",
                                };

                administrator.Add(administratorDetails);
            }
            catch (CreatesendException ex)
            {
                var error = (ErrorResult)ex.Data["ErrorResult"];
                Console.WriteLine(error.Code);
                Console.WriteLine(error.Message);
            }
            catch (Exception ex)
            {
                //handle some other failure
            }
        }

        public void Update()
        {
            var administrator = new Administrator();
           
            try
            {
                var administratorDetails = administrator.Details("test@notarealdomain.com");
                administratorDetails.Name = "test new name";

                administrator.Update("test@notarealdomain.com", administratorDetails);
            }
            catch (CreatesendException ex)
            {
                var error = (ErrorResult)ex.Data["ErrorResult"];
                Console.WriteLine(error.Code);
                Console.WriteLine(error.Message);
            }
            catch (Exception ex)
            {
                //handle some other failure
            }
        }

        public void List()
        {
            var account = new Account();
           
            try
            {
                foreach(var p in account.Administrators())
                {
                    Console.Out.WriteLine("{0}", p.EmailAddress);
                    Console.Out.WriteLine("{0}", p.Name);
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
                //handle some other failure
            }
        }

        public void Delete()
        {
            var administrator = new Administrator();
           
            try
            {
               administrator.Delete("test@notarealdomain.com");
            }
            catch (CreatesendException ex)
            {
                var error = (ErrorResult)ex.Data["ErrorResult"];
                Console.WriteLine(error.Code);
                Console.WriteLine(error.Message);
            }
            catch (Exception ex)
            {
                //handle some other failure
            }
        }

        public void SetPrimaryContact()
        {
            var account = new Account();
           
            try
            {
               account.SetPrimaryContact("test@notarealdomain.com");
            }
            catch (CreatesendException ex)
            {
                var error = (ErrorResult)ex.Data["ErrorResult"];
                Console.WriteLine(error.Code);
                Console.WriteLine(error.Message);
            }
            catch (Exception ex)
            {
                //handle some other failure
            }
        }

        public void GetPrimaryContact()
        {
            var account = new Account();
           
            try
            {
                Console.Out.WriteLine(account.GetPrimaryContact());
            }
            catch (CreatesendException ex)
            {
                var error = (ErrorResult)ex.Data["ErrorResult"];
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
