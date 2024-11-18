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
// Copyright (c) 2017 Sunny Chen
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

namespace Hal.AspNetCore;

/// <summary>
/// Represents that the implemented classes are the collection
/// that contains a specific page of the records along with
/// the pagination information.
/// </summary>
public interface IPagedResult : IEnumerable
{
    #region Public Properties

    /// <summary>
    /// Gets or sets the page number.
    /// </summary>
    /// <value>
    /// The page number.
    /// </value>
    int PageNumber { get; set; }

    /// <summary>
    /// Gets or sets the size of the page.
    /// </summary>
    /// <value>
    /// The size of the page.
    /// </value>
    int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the total pages.
    /// </summary>
    /// <value>
    /// The total pages.
    /// </value>
    long TotalPages { get; set; }

    /// <summary>
    /// Gets or sets the number of total records.
    /// </summary>
    /// <value>
    /// The number of total records.
    /// </value>
    long TotalRecords { get; set; }

    #endregion Public Properties
}