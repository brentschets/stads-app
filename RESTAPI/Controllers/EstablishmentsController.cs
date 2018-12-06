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
    public class EstablishmentsController : ControllerBase
    {
        private readonly RESTAPIContext _context;

        public EstablishmentsController(RESTAPIContext context)
        {
            _context = context;
        }

        // GET: api/Establishments
        [HttpGet]
        public IEnumerable<Establishment> GetEstablishment()
        {
            return _context.Establishment;
        }

        // GET: api/Popular/10
        [HttpGet("Popular/{limit}")]
        public IActionResult GetPopular([FromRoute] int limit)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (limit < 0) return BadRequest(new {Message = $"Limit must not be less than 0, got {limit}"});

            var popularEstablishments = _context.Establishment.Include(e => e.Store).Include(e => e.Address)
                .OrderByDescending(e => e.Visited).Take(limit);

            return Ok(popularEstablishments);
        }

        // GET: api/Establishments/ForStore/4
        [HttpGet("ForStore/{storeId}")]
        public IActionResult GetEstablishmentsForStore([FromRoute] int storeId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var establishments = _context.Establishment.Include(e => e.Address).Include(e => e.Store)
                .Where(e => e.Store.StoreId == storeId);

            return Ok(establishments);
        }

        // GET: api/Establishments/ForUser/2
        [HttpGet("ForUser/{userId}")]
        public IActionResult GetEstablishmentsForUser([FromRoute] int userId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var establishments = _context.Establishment.Include(e => e.Address).Include(e => e.Store)
                .Where(e => e.SubscribedUsers.Any(ue => ue.UserId == userId))
                .Distinct();

            return Ok(establishments);
        }
    }
}