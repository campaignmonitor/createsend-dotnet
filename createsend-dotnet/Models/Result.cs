using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Collections;

namespace createsend_dotnet
{
    [XmlRoot("Result")]
    public class ErrorResult
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }

    [XmlRoot("Result")]
    public class ApiKeyResult
    {
        public string ApiKey { get; set; }
    }

    [XmlRoot("Result")]
    public class SystemDateResult
    {
        public string SystemDate { get; set; }
    }

    public abstract class EnumerableCollection<T> : IEnumerable<T>
    {
        private List<T> _items = new List<T>();

        [XmlIgnore]
        public List<T> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public void Add(T item)
        {
            _items.Add(item);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}
