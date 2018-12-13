using Microsoft.AspNetCore.Mvc;
using RESTAPI.Exceptions;
using RESTAPI.Repositories;

namespace RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;

        public EventsController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        // GET: api/Events
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_eventRepository.GetAll());
        }

        // GET: api/Events/Popular/10
        [HttpGet("Popular/{limit}")]
        public IActionResult GetPopular([FromRoute] int limit)
        {
            try
            {
                return Ok(_eventRepository.GetPopular(limit));
            }
            catch (EventException e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/Events/ForEstablishment/2
        [HttpGet("ForEstablishment/{estalishmentId}")]
        public IActionResult GetForEstablishment([FromRoute] int establishmentId)
        {
            return Ok(_eventRepository.GetForEstablishment(establishmentId));
        }
    }
}