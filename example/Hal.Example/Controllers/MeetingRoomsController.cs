using Hal.AspNetCore;
using Hal.Example.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hal.Example.Controllers
{
    [ServiceFilter(typeof(SupportsHalAttribute))]
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingRoomsController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll([FromQuery] int size = 5, [FromQuery] int page = 1) => Ok(new PagedResult<MeetingRoom>(
                MeetingRoom.FakeRooms.Skip((page - 1) * size).Take(size),
                page,
                size,
                MeetingRoom.FakeRooms.Count(),
                (int)Math.Ceiling((double)MeetingRoom.FakeRooms.Count() / size)));

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
            => MeetingRoom.FakeRooms.Any(mr => mr.ID == id)
            ?
            Ok(MeetingRoom.FakeRooms.First(mr => mr.ID == id))
            :
            NotFound($"Meeting Room Id {id} doesn't exist.");
    }
}
