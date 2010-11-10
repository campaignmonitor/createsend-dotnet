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
            foreach(string country in General.Countries())
                Console.WriteLine(country);

        }
    }
}
