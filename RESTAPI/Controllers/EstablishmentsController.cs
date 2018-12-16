using Microsoft.AspNetCore.Mvc;
using RESTAPI.Exceptions;
using RESTAPI.Models;
using RESTAPI.Repositories;
using RESTAPI.ViewModels;

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

        // POST: api/Establishments
        [HttpPost]
        public IActionResult AddEstablishment([FromBody] AddEstablishmentViewModel viewModel)
        {
            var address = new Address
            {
                Street = viewModel.Street,
                Number = viewModel.Number
            };

            var establishment = new Establishment {Address = address};

            try
            {
                var ret = _establishmentRepository.Create(establishment, viewModel.StoreId, viewModel.Image,
                    viewModel.FileName);
                return Ok(ret);
            }
            catch (EstablishmentException e)
            {
                return BadRequest(new {message = e.Message});
            }
        }

        // POST: api/Establishments/Update
        [HttpPost("Update")]
        public IActionResult UpdateEstablishment([FromBody] UpdateEstablishmentViewModel viewModel)
        {
            var establishment = new Establishment
            {
                EstablishmentId = viewModel.EstablishmentId,
                Address = new Address
                {
                    Street = viewModel.Street,
                    Number = viewModel.Number
                }
            };

            return Ok(_establishmentRepository.Update(establishment));
        }

        // DELETE: api/Establishments/7
        [HttpDelete("{id}")]
        public IActionResult DeleteEstablishment([FromRoute] int id)
        {
            _establishmentRepository.Delete(id);
            return Ok();
        }

        // GET: api/Establishments/Popular/10
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