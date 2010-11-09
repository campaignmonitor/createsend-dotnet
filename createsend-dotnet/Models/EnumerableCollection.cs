using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace createsend_dotnet
{
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
