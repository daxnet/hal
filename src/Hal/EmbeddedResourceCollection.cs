// ---------------------------------------------------------------------------
//  _    _          _      
// | |  | |   /\   | |     
// | |__| |  /  \  | |     
// |  __  | / /\ \ | |     
// | |  | |/ ____ \| |____ 
// |_|  |_/_/    \_\______|
//
// A C#/.NET Core implementation of Hypertext Application Language
// https://stateless.group/hal_specification.html
// 
// MIT License
//
// Copyright (c) 2017-2025 Sunny Chen
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// ---------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;

namespace Hal;

/// <summary>
/// Represents the list of <see cref="IEmbeddedResource"/> objects.
/// </summary>
public sealed class EmbeddedResourceCollection : ICollection<IEmbeddedResource>
{
    #region Private Fields
    private readonly List<IEmbeddedResource> _items = new List<IEmbeddedResource>();
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="EmbeddedResourceCollection"/> class.
    /// </summary>
    /// <param name="enforcingArrayConverting">If set to <c>true</c>, the embedded resource will be converted to an array
    /// even if it only contains one element.</param>
    public EmbeddedResourceCollection(bool enforcingArrayConverting = false)
    {
        EnforcingArrayConverting = enforcingArrayConverting;
    }

    #region Public Properties

    /// <summary>
    /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    public int Count => _items.Count;

    /// <summary>
    /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
    /// </summary>
    public bool IsReadOnly => false;

    /// <summary>
    /// Gets a value indicating whether the embedded resource will be converted to an array
    /// even if it only contains one element.
    /// </summary>
    public bool EnforcingArrayConverting { get; }
    #endregion

    #region Public Methods

    /// <summary>
    /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
    public void Add(IEmbeddedResource item) => _items.Add(item);

    /// <summary>
    /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    public void Clear() => _items.Clear();

    /// <summary>
    /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
    /// </summary>
    /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
    /// <returns>
    /// true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.
    /// </returns>
    public bool Contains(IEmbeddedResource item) => _items.Contains(item);

    /// <summary>
    /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.
    /// </summary>
    /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
    /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
    public void CopyTo(IEmbeddedResource[] array, int arrayIndex) => _items.CopyTo(array, arrayIndex);

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// An enumerator that can be used to iterate through the collection.
    /// </returns>
    public IEnumerator<IEmbeddedResource> GetEnumerator() => _items.GetEnumerator();

    /// <summary>
    /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
    /// <returns>
    /// true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </returns>
    public bool Remove(IEmbeddedResource item) => _items.Remove(item);

    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();
    #endregion
}