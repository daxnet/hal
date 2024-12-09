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

namespace Hal.AspNetCore;

/// <summary>
/// Represents the options to configure the HAL support in ASP.NET Core Web API applications.
/// </summary>
public sealed class SupportsHalOptions
{
    #region Public Properties

    /// <summary>
    /// Gets or sets a <see cref="bool"/> value which indicates whether
    /// the HAL feature should be enabled.
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Gets or sets the name of the Id property of the object that represents
    /// the embedded resource.
    /// </summary>
    public string IdPropertyName { get; set; } = "Id";

    /// <summary>
    /// Gets or sets a <see cref="bool"/> value which indicates if the HTTPS scheme
    /// should be used when generating the href links. By default, the value will
    /// be inferred from the type of the hosting environment. If it is Production,
    /// the HTTPS scheme will be used, otherwise, it will be HTTP.
    /// </summary>
    public bool? UseHttpsScheme { get; set; }

    #endregion Public Properties
}