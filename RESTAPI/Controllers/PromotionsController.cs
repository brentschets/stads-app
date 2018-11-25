using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // GET: api/Promotions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPromotion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var promotion = await _context.Promotion.Include(p => p.Store)
                .FirstOrDefaultAsync(p => p.PromotionId == id);

            if (promotion == null)
            {
                return NotFound();
            }

            return Ok(promotion);
        }

        // GET: api/Promotions/Popular/10
        [HttpGet("Popular/{limit}")]
        public IActionResult GetPopular([FromRoute] int limit)
        {
            var res = _context.Promotion.Include(p => p.Store).OrderByDescending(p => p.Visited).Take(limit).ToList();
            return Ok(res);
        }

        // PUT: api/Promotions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPromotion([FromRoute] int id, [FromBody] Promotion promotion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != promotion.PromotionId)
            {
                return BadRequest();
            }

            _context.Entry(promotion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PromotionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Promotions
        [HttpPost]
        public async Task<IActionResult> PostPromotion([FromBody] Promotion promotion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Promotion.Add(promotion);
            await _context.SaveChangesAsync();

            // ReSharper disable once Mvc.ActionNotResolved
            return CreatedAtAction("GetPromotion", new {id = promotion.PromotionId}, promotion);
        }

        // DELETE: api/Promotions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromotion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var promotion = await _context.Promotion.FindAsync(id);
            if (promotion == null)
            {
                return NotFound();
            }

            _context.Promotion.Remove(promotion);
            await _context.SaveChangesAsync();

            return Ok(promotion);
        }

        private bool PromotionExists(int id)
        {
            return _context.Promotion.Any(e => e.PromotionId == id);
        }
    }
}