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

namespace Hal.Builders;

/// <summary>
/// Represents that the implemented classes are the HAL resource
/// builders that simply returned the built HAL resource.
/// </summary>
/// <seealso cref="Hal.Builders.IBuilder" />
public interface IResourceBuilder : IBuilder { }

/// <summary>
/// Represents the HAL resource builder.
/// </summary>
/// <seealso cref="Hal.Builders.IResourceBuilder" />
public sealed class ResourceBuilder : IResourceBuilder
{
    #region Private Fields
    private readonly Resource _resource = new Resource();
    #endregion

    #region Ctor        
    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceBuilder"/> class.
    /// </summary>
    public ResourceBuilder() { }
    #endregion

    #region Public Methods        
    /// <summary>
    /// Builds the <see cref="Resource"/> instance.
    /// </summary>
    /// <returns>The <see cref="Resource"/> instance to be built.</returns>
    public Resource Build()
    {
        return _resource;
    }
    #endregion
}