using Microsoft.AspNetCore.Mvc;
using RESTAPI.Exceptions;
using RESTAPI.Repositories;

namespace RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstablishmentsController : ControllerBase
    {
        private readonly IEstablishmentRepository _establishmentRepository;

        public EstablishmentsController(IEstablishmentRepository establishmentRepository)
        {
            _establishmentRepository = establishmentRepository;
        }

        // GET: api/Popular/10
        [HttpGet("Popular/{limit}")]
        public IActionResult GetPopular([FromRoute] int limit)
        {
            try
            {
                return Ok(_establishmentRepository.GetPopular(limit));
            }
            catch (EstablishmentException e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/Establishments/ForStore/4
        [HttpGet("ForStore/{storeId}")]
        public IActionResult GetEstablishmentsForStore([FromRoute] int storeId)
        {
            return Ok(_establishmentRepository.GetForStore(storeId));
        }

        // GET: api/Establishments/ForUser/2
        [HttpGet("ForUser/{userId}")]
        public IActionResult GetEstablishmentsForUser([FromRoute] int userId)
        {
            return Ok(_establishmentRepository.GetForUser(userId));
        }
    }
}