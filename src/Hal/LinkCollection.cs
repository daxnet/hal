using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hal
{
    public sealed class LinkCollection : ICollection<ILink>
    {
        private readonly List<ILink> links = new List<ILink>();

        public int Count => links.Count;

        public bool IsReadOnly => false;

        public void Add(ILink item) => links.Add(item);

        public void Clear() => links.Clear();

        public bool Contains(ILink item) => links.Contains(item);

        public void CopyTo(ILink[] array, int arrayIndex) => links.CopyTo(array, arrayIndex);

        public IEnumerator<ILink> GetEnumerator() => links.GetEnumerator();

        public bool Remove(ILink item) => links.Remove(item);

        IEnumerator IEnumerable.GetEnumerator() => links.GetEnumerator();
    }
}
