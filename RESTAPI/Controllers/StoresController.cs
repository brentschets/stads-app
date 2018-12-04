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
    public class StoresController : ControllerBase
    {
        private readonly RESTAPIContext _context;

        public StoresController(RESTAPIContext context)
        {
            _context = context;
        }

        // GET: api/Stores
        [HttpGet]
        public IEnumerable<Store> GetStore()
        {
            return _context.Store.Include(s => s.Category);
        }

        // GET: api/Stores/ByCategory/5
        [HttpGet("ByCategory/{id}")]
        public IActionResult GetByCategory([FromRoute] int id)
        {
            var res = _context.Store.Include(s => s.Category).Where(s => s.Category.CategoryId == id);
            return Ok(res);
        }

        // GET: api/Stores/Popular/10
        [HttpGet("Popular/{limit}")]
        public IActionResult GetPopularStores([FromRoute] int limit)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (limit < 0) return BadRequest(new {Message = $"Limit must not be less than 0, got {limit}"});

            var popularStores = _context.Establishment.OrderByDescending(e => e.Visited).Select(e => e.Store)
                .Distinct().Take(10);

            return Ok(popularStores);
        }
    }
}