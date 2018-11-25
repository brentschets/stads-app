using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTAPI.Data;

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

        // GET: api/Events/Popular/10
        [HttpGet("Popular/{limit}")]
        public IActionResult GetPopular([FromRoute] int limit)
        {
            var res = _context.Event.Include(e => e.Store).ThenInclude(s => s.Address).Include(e => e.Store)
                .ThenInclude(s => s.Category).OrderByDescending(e => e.Visited).Take(limit).ToList();
            return Ok(res);
        }
    }
}