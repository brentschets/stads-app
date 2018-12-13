using Microsoft.AspNetCore.Mvc;
using RESTAPI.Exceptions;
using RESTAPI.Repositories;

namespace RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;

        public StoresController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        // GET: api/Stores
        [HttpGet]
        public IActionResult GetStore()
        {
            return Ok(_storeRepository.GetAll());
        }

        // GET: api/Stores/ByCategory/5
        [HttpGet("ByCategory/{id}")]
        public IActionResult GetByCategory([FromRoute] int id)
        {
            return Ok(_storeRepository.GetByCategory(id));
        }

        // GET: api/Stores/Popular/10
        [HttpGet("Popular/{limit}")]
        public IActionResult GetPopularStores([FromRoute] int limit)
        {
            try
            {
                return Ok(_storeRepository.GetPopular(limit));
            }
            catch (StoreException e)
            {
                return BadRequest(new {message = e.Message});
            }
        }
    }
}