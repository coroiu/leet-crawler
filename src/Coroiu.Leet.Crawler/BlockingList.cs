using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Coroiu.Leet.Crawler
{
    internal class BlockingList<T> : IEnumerable<T>
    {
        private object listLock;
        private IList<T> list;

        public BlockingList()
        {
            listLock = new object();
            list = new List<T>();
        }

        public void Add(T item)
        {
            lock (listLock)
            {
                list.Add(item);
            }
        }

        public void Remove(T item)
        {
            lock (listLock)
            {
                list.Remove(item);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            IList<T> clone;
            lock (listLock)
            {
                clone = new List<T>(list);
            }

            return clone.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
}
