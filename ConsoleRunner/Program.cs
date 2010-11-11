using System;
using System.Collections.Generic;
using System.Text;
using createsend_dotnet;
using Newtonsoft.Json;

namespace ConsoleRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Client.Create(new ClientDetail() { CompanyName = "APIv3_3", ContactName = "jason", Country = "Australia", EmailAddress = "jasonh+v3_3@freshview.com", TimeZone = "(GMT+10:00) Canberra, Melbourne, Sydney" }));
        }
    }
}
