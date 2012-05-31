using System;
using createsend_dotnet;

namespace Samples
{
    public class Administrators
    {
        public void Add()
        {
            Administrator administrator = new Administrator();
           
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
                //handle some other failure
            }
        }

        public void Update()
        {
            Administrator administrator = new Administrator();
           
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
                //handle some other failure
            }
        }

        public void List()
        {
            Account account = new Account();
           
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
                //handle some other failure
            }
        }

        public void Delete()
        {
            Administrator administrator = new Administrator();
           
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
                //handle some other failure
            }
        }

        public void SetPrimaryContact()
        {
            Account account = new Account();
           
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
                //handle some other failure
            }
        }

        public void GetPrimaryContact()
        {
            Account account = new Account();
           
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
                //handle some other failure
            }
        }
    }
}
