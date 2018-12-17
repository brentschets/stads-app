using Microsoft.AspNetCore.Mvc;
using RESTAPI.Exceptions;
using RESTAPI.Models;
using RESTAPI.Repositories;
using RESTAPI.ViewModels;

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
        [HttpGet("Forstore/{storeId}")]
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

        //Delete: api/Promotions/
        [HttpDelete("{promotionId}")]
        public void DeleteForStore([FromRoute] int promotionId)
        {
            _promotionRepository.DeletePromotionForStore(promotionId);
        }

        // POST: api/promotion
        [HttpPost]
        public IActionResult AddPromotion([FromBody] AddPromotionViewModel viewModel)
        {
            var promotion = new Promotion
            {
                Name = viewModel.Name
            };
            try
            {
                return Ok(_promotionRepository.AddPromotion(promotion, viewModel.StoreId));
            }
            catch (PromotionException e)
            {
                return BadRequest(new {message = e.Message});
            }
        }
    }
}