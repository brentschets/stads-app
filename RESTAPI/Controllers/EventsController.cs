using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTAPI.Data;
using RESTAPI.Models;

namespace RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly RESTAPIContext _context;

        public EventsController(RESTAPIContext context)
        {
            _context = context;
        }

        // GET: api/Establishments
        [HttpGet]
        public IEnumerable<Event> GetEstablishment()
        {
            return _context.Event;
        }

        // GET: api/Events/Popular/10
        [HttpGet("Popular/{limit}")]
        public IActionResult GetPopular([FromRoute] int limit)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (limit < 0) return BadRequest(new {Message = $"Limit must not be less than 0, got {limit}"});

            var popularEvents = _context.Event.Include(e => e.Establishment).ThenInclude(e => e.Address)
                .Include(e => e.Establishment).ThenInclude(e => e.Store)
                .OrderByDescending(e => e.Visited).Take(limit);

            return Ok(popularEvents);
        }

        // GET: api/Events/ForEstablishment/2
        [HttpGet("ForEstablishment/{estalishmentId}")]
        public IActionResult GetForEstablishment([FromRoute] int establishmentId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var events = _context.Event.Include(e => e.Establishment)
                .Where(e => e.Establishment.EstablishmentId == establishmentId);

            return Ok(events);
        }
    }
}