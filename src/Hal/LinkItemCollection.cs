using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Hal.Converters;

namespace Hal
{
    public sealed class LinkItemCollection : ICollection<ILinkItem>
    {
        public LinkItemCollection(bool enforcingArraryConverting = false)
        {
            this.EnforcingArraryConverting = enforcingArraryConverting;
        }

        private readonly List<ILinkItem> items = new List<ILinkItem>();

        public int Count => items.Count;

        public bool IsReadOnly => false;

        public void Add(ILinkItem item) => items.Add(item);

        public void Clear() => items.Clear();

        public bool Contains(ILinkItem item) => items.Contains(item);

        public void CopyTo(ILinkItem[] array, int arrayIndex) => items.CopyTo(array, arrayIndex);

        public IEnumerator<ILinkItem> GetEnumerator() => items.GetEnumerator();

        public bool Remove(ILinkItem item) => items.Remove(item);

        IEnumerator IEnumerable.GetEnumerator() => items.GetEnumerator();

        public bool EnforcingArraryConverting { get; }
    }
}
