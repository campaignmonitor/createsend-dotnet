using System;
using System.Collections.Generic;
using System.Text;
using createsend_dotnet;
using Newtonsoft.Json;

namespace ConsoleRunner
{
    class Program
    {
        //011ebcadaeb71e9a
        static void Main(string[] args)
        {
            foreach (BasicSegment campaign in Client.Segments("011ebcadaeb71e9a"))
                Console.WriteLine(campaign.Title);
        }
    }
}
