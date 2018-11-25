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
        public IEnumerable<Promotion> GetPromotion()
        {
            return _context.Promotion.Include(p => p.Store).ThenInclude(s => s.Address).Include(p => p.Store)
                .ThenInclude(s => s.Category);
        }

        // GET: api/Promotions/Popular/10
        [HttpGet("Popular/{limit}")]
        public IActionResult GetPopular([FromRoute] int limit)
        {
            var res = _context.Promotion.Include(p => p.Store).OrderByDescending(p => p.Visited).Take(limit).ToList();
            return Ok(res);
        }
    }
}