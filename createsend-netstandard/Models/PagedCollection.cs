using System;
using System.Collections.Generic;
using System.Text;

namespace createsend_dotnet
{
    public class PagedCollection<T>
    {
        //deserializer does not allow deserialization from JSON list straight to IEnumerable<T>
        public List<T> Results { get; set; }
        public string ResultsOrderedBy { get; set; }
        public string OrderDirection { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int RecordsOnThisPage { get; set; }
        public int TotalNumberOfRecords { get; set; }
        public int NumberOfPages { get; set; }
    }
}
