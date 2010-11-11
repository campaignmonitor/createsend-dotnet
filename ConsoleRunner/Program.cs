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
        //277da11f331fc698ad22a66c0c4b5c33
        static void Main(string[] args)
        {
            new Client("34a1fa45cbf30b2024e3df25fa214e54").Delete();
        }
    }
}
