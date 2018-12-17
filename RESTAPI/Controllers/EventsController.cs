using Microsoft.AspNetCore.Mvc;
using RESTAPI.Exceptions;
using RESTAPI.Models;
using RESTAPI.Repositories;
using RESTAPI.ViewModels;

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
        [HttpGet("ForEstablishment/{establishmentId}")]
        public IActionResult GetForEstablishment([FromRoute] int establishmentId)
        {
            return Ok(_eventRepository.GetForEstablishment(establishmentId));
        }

        // POST: api/Events
        [HttpPost]
        public IActionResult AddEvent([FromBody] AddEventViewModel viewModel)
        {
            var @event = new Event
            {
                Name = viewModel.Name,
                Description = viewModel.Description
            };

            try
            {
                return Ok(_eventRepository.Add(@event, viewModel.EstablishmentId));
            }
            catch (EventException e)
            {
                return BadRequest(new {message = e.Message});
            }
        }

        // DELETE: api/Events/3
        [HttpDelete("{eventId}")]
        public IActionResult DeleteEvent([FromRoute] int eventId)
        {
            _eventRepository.Delete(eventId);
            return Ok();
        }
    }
}