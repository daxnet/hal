// ---------------------------------------------------------------------------
//  _    _          _
// | |  | |   /\   | |
// | |__| |  /  \  | |
// |  __  | / /\ \ | |
// | |  | |/ ____ \| |____
// |_|  |_/_/    \_\______|
//
// A C#/.NET Core implementation of Hypertext Application Language
// http://stateless.co/hal_specification.html
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
using Hal.AspNetCore;
using Hal.Example.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hal.Example.Controllers
{
    [ServiceFilter(typeof(SupportsHalAttribute))]
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingRoomsController : ControllerBase
    {
        #region Public Methods

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll([FromQuery] int size = 5, [FromQuery] int page = 1) => Ok(new PagedResult<MeetingRoom>(
                MeetingRoom.FakeRooms.Skip((page - 1) * size).Take(size),
                page,
                size,
                MeetingRoom.FakeRooms.Count(),
                (int)Math.Ceiling((double)MeetingRoom.FakeRooms.Count() / size)));

        [HttpGet("{identifier}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int identifier)
        {
            await Task.Delay(1);
            if (MeetingRoom.FakeRooms.Any(mr => mr.ID == identifier))
            {
                return Ok(MeetingRoom.FakeRooms.First(mr => mr.ID == identifier));
            }

            return NotFound($"Meeting Room Id {identifier} doesn't exist.");
        }

        [GetByIdMethodImpl]
        [HttpGet("get-by-id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAnother(int id)
        {
            if (MeetingRoom.FakeRooms.Any(mr => mr.ID == id))
            {
                return Ok(MeetingRoom.FakeRooms.First(mr => mr.ID == id));
            }

            return NotFound($"Meeting Room Id {id} doesn't exist.");
        }

        [HttpGet("get-by-name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByName(string name)
            => Ok(MeetingRoom.FakeRooms.Where(f => f.Name.Contains(name)));

        #endregion Public Methods
    }
}