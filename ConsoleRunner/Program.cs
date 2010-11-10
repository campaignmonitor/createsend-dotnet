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
            foreach(Client client in General.Clients())
                Console.WriteLine(client.Name);
            Console.ReadLine();

            Console.WriteLine(General.SystemDate());
        }
    }
}
