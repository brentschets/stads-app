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
    public class PromotionsController : ControllerBase
    {
        private readonly RESTAPIContext _context;

        public PromotionsController(RESTAPIContext context)
        {
            _context = context;
        }

        // GET: api/Promotions
        [HttpGet]
        public IEnumerable<Promotion> GetEstablishment()
        {
            return _context.Promotion;
        }

        // GET: api/Promotions/Popular/10
        [HttpGet("Popular/{limit}")]
        public IActionResult GetPopular([FromRoute] int limit)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (limit < 0) return BadRequest(new {Message = $"Limit must not be less than 0, got {limit}"});

            var popularPromotions =
                _context.Promotion.Include(p => p.Store).OrderByDescending(p => p.Visited).Take(limit);

            return Ok(popularPromotions);
        }

        // GET: api/Promotions/ForStore
        [HttpGet("Popular/{storeId}")]
        public IActionResult GetForStore([FromRoute] int storeId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var promotions = _context.Promotion.Include(p => p.Store).Where(p => p.Store.StoreId == storeId);

            return Ok(promotions);
        }

        // GET: api/Promotions/ForEstablishment
        [HttpGet("ForEstablishment/{establishmentId}")]
        public IActionResult GetForEstablishment([FromRoute] int establishmentId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var promotions = _context.Promotion.Include(p => p.Store).ThenInclude(s => s.Establishments)
                .Where(p => p.Store.Establishments.Any(e => e.EstablishmentId == establishmentId));

            return Ok(promotions);
        }
    }
}