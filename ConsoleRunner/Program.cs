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
            Console.WriteLine(General.ApiKey("jasoninc.alphacreatesend.com", "jbh", "wordpass"));
        }
    }
}
