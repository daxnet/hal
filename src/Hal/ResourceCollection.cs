using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hal
{
    public sealed class ResourceCollection : ICollection<IResource>
    {
        private readonly List<IResource> resources = new List<IResource>();

        public int Count => resources.Count;

        public bool IsReadOnly => false;

        public void Add(IResource item) => resources.Add(item);

        public void Clear() => resources.Clear();

        public bool Contains(IResource item) => resources.Contains(item);

        public void CopyTo(IResource[] array, int arrayIndex) => resources.CopyTo(array, arrayIndex);

        public IEnumerator<IResource> GetEnumerator() => resources.GetEnumerator();

        public bool Remove(IResource item) => resources.Remove(item);

        IEnumerator IEnumerable.GetEnumerator() => resources.GetEnumerator();
    }
}
