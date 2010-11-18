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
            foreach (BasicSegment segment in new Client("011ebcadaeb71e9a").Segments())
                Console.WriteLine(new Segment(segment.SegmentID).Details());
            SegmentRules rules = new SegmentRules();
            rules.Add(new Rule() { Subject = "Email", Clauses = new List<string>() { "EQUALS jasonh" } });
            var segmentID = Segment.Create("aa76d29949e7f10ab28712617634fd0b", "fucker", rules);
            Console.WriteLine(segmentID);

        }
    }
}
