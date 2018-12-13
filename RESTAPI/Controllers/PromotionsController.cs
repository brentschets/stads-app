using Microsoft.AspNetCore.Mvc;
using RESTAPI.Exceptions;
using RESTAPI.Repositories;

namespace RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly IPromotionRepository _promotionRepository;

        public PromotionsController(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        // GET: api/Promotions
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_promotionRepository.GetAll());
        }

        // GET: api/Promotions/Popular/10
        [HttpGet("Popular/{limit}")]
        public IActionResult GetPopular([FromRoute] int limit)
        {
            try
            {
                return Ok(_promotionRepository.GetPopular(limit));
            }
            catch (PromotionException e)
            {
                return BadRequest(new {message = e.Message});
            }
        }

        // GET: api/Promotions/ForStore
        [HttpGet("Popular/{storeId}")]
        public IActionResult GetForStore([FromRoute] int storeId)
        {
            return Ok(_promotionRepository.GetForStore(storeId));
        }

        // GET: api/Promotions/ForEstablishment
        [HttpGet("ForEstablishment/{establishmentId}")]
        public IActionResult GetForEstablishment([FromRoute] int establishmentId)
        {
            return Ok(_promotionRepository.GetForEstablishment(establishmentId));
        }
    }
}