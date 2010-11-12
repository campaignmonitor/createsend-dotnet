using System;
using System.Collections.Generic;
using System.Text;
using createsend_dotnet;
using Newtonsoft.Json;

namespace ConsoleRunner
{
    class Program
    {
        //testing - 011ebcadaeb71e9a
        //testing list - aa76d29949e7f10ab28712617634fd0b
        //apicreated  - 277da11f331fc698ad22a66c0c4b5c33
        //list in apicreated - 41a99346539316727de7f24491da29d6
        static void Main(string[] args)
        {
            foreach (Subscriber subscriber in new List("c59ebf5a6314f17ec107c94d84665ca9").Unsubscribed(new DateTime(2008, 1, 1), 2, 10, "email", "asc").Results)
                Console.WriteLine(subscriber.EmailAddress);
        }
    }
}
