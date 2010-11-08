using System;
using System.Collections.Generic;
using System.Text;
using createsend_dotnet;

namespace ConsoleRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine(General.ApiKey("http://jasoninc.alphacreatesend.com", "jbh", "wordpass"));
            System.Console.WriteLine(General.SystemDate());
            System.Console.WriteLine(General.Countries());
        }
    }
}
