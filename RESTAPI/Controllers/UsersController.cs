using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RESTAPI.Exceptions;
using RESTAPI.Models;
using RESTAPI.Repositories;
using RESTAPI.Utils;

namespace RESTAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] UserDto userDto)
        {
            var user = _userRepository.Authenticate(userDto.Username, userDto.Password);

            if (user == null) return BadRequest(new {message = "Username or password is incorrect"});

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and token to client
            return Ok(new
            {
                user.UserId,
                user.Username,
                user.FirstName,
                user.LastName,
                Token = tokenString,
                Subscriptions = user.Subscriptions.Select(ue => ue.EstablishmentId)
            });
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserDto userDto)
        {
            var user = UserFromDto(userDto);

            try
            {
                _userRepository.Create(user, userDto.Password);
                return Ok();
            }
            catch (AuthenticationException e)
            {
                return BadRequest(new {message = e.Message});
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var user = _userRepository.GetById(id);
            var userDto = DtoFromUser(user);
            return Ok(userDto);
        }

        [HttpPost("Update/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UserDto userDto)
        {
            var user = UserFromDto(userDto);
            user.UserId = id;

            try
            {
                _userRepository.Update(user, userDto.Password);
                return Ok();
            }
            catch (AuthenticationException e)
            {
                return BadRequest(new {message = e.Message});
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _userRepository.Delete(id);
            return Ok();
        }

        [HttpPost("Subscribe")]
        public IActionResult Subscribe([FromBody] SubscriptionDto subscriptionDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = subscriptionDto.UserId;
            var establishmentId = subscriptionDto.EstablishmentId;

            try
            {
                _userRepository.Subscribe(userId, establishmentId);
                return Ok();
            }
            catch (AuthenticationException e)
            {
                return BadRequest(new {message = e.Message});
            }
        }

        [HttpPost("Unsubscribe")]
        public IActionResult Unsubscribe([FromBody] SubscriptionDto subscriptionDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = subscriptionDto.UserId;
            var establishmentId = subscriptionDto.EstablishmentId;

            try
            {
                _userRepository.Unsubscribe(userId, establishmentId);
                return Ok();
            }
            catch (AuthenticationException e)
            {
                return BadRequest(new {message = e.Message});
            }
        }

        #region Helpers

        private static User UserFromDto(UserDto dto)
        {
            return new User
            {
                UserId = dto.UserId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Username = dto.Username,
            };
        }

        private static UserDto DtoFromUser(User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Subscriptions = user.Subscriptions
            };
        }

        #endregion
    }
}