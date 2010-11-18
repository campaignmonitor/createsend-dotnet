using System;
using System.Collections.Generic;
using System.Text;

namespace createsend_dotnet
{
    public class BasicList
    {
        public string ListID { get; set; }
        public string Name { get; set; }
    }

    public class Lists : List<BasicList> { }

    public class ListDetail
    {
        public string ListID { get; set; }
        public DateTime DateCreated { get; set; }
        public string Title { get; set; }
        public string UnsubscribePage { get; set; }
        public bool ConfirmedOptIn { get; set; }
        public string ConfirmationSuccessPage { get; set; }
    }

    public class ListCustomField
    {
        public string FieldName { get; set; }
        public string Key { get; set; }
        public string DataType { get; set; }
        public List<string> FieldOptions { get; set; }
    }

    public enum CustomFieldDataType
    {
        Text = 1,
        Number = 2,
        MultiSelectOne = 3,
        MultiSelectMany = 4,
        Date = 5,
    }

    public class ListStats
    {
        public int TotalActiveSubscribers { get; set; }
        public int NewActiveSubscribersToday { get; set; }
        public int NewActiveSubscribersYesterday { get; set; }
        public int NewActiveSubscribersThisWeek { get; set; }
        public int NewActiveSubscribersThisMonth { get; set; }
        public int NewActiveSubscribersThisYear { get; set; }
        public int TotalUnsubscribes { get; set; }
        public int UnsubscribesToday { get; set; }
        public int UnsubscribesYesterday { get; set; }
        public int UnsubscribesThisWeek { get; set; }
        public int UnsubscribesThisMonth { get; set; }
        public int UnsubscribesThisYear { get; set; }
        public int TotalDeleted { get; set; }
        public int DeletedToday { get; set; }
        public int DeletedYesterday { get; set; }
        public int DeletedThisWeek { get; set; }
        public int DeletedThisMonth { get; set; }
        public int DeletedThisYear { get; set; }
        public int TotalBounces { get; set; }
        public int BouncesToday { get; set; }
        public int BouncesYesterday { get; set; }
        public int BouncesThisWeek { get; set; }
        public int BouncesThisMonth { get; set; }
        public int BouncesThisYear { get; set; }
    }
}
