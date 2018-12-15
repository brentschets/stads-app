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
using RESTAPI.ViewModels;

namespace RESTAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        private readonly IStoreRepository _storeRepository;

        public UsersController(IUserRepository userRepository, IStoreRepository storeRepository)
        {
            _userRepository = userRepository;
            _storeRepository = storeRepository;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateUserViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = _userRepository.Authenticate(viewModel.Username, viewModel.Password);

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
                Subscriptions = user.Subscriptions.Select(ue => ue.EstablishmentId),
                user.StoreId
            });
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterUserViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new User
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Username = viewModel.Username
            };

            try
            {
                _userRepository.Create(user, viewModel.Password);
                return Ok();
            }
            catch (AuthenticationException e)
            {
                return BadRequest(new {message = e.Message});
            }
        }

        [AllowAnonymous]
        [HttpPost("RegisterStore")]
        public IActionResult RegisterStore([FromBody] RegisterStoreViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // create user
            var user = new User
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Username = viewModel.Username
            };

            try
            {
                user = _userRepository.Create(user, viewModel.Password);
            }
            catch (AuthenticationException e)
            {
                return BadRequest(new {message = e.Message});
            }

            // create store
            var store = new Store
            {
                Name = viewModel.StoreName,
                Description = viewModel.StoreDescription
            };

            try
            {
                _storeRepository.Create(store, viewModel.CategoryId, viewModel.Image, viewModel.FileName, user.UserId);
                return Ok();
            }
            catch (StoreException e)
            {
                return BadRequest(new {message = e.Message});
            }
        }

        [HttpPost("Update/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateUserViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new User
            {
                UserId = id,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Username = viewModel.Username
            };

            try
            {
                _userRepository.Update(user, viewModel.Password);
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
        public IActionResult Subscribe([FromBody] SubscribeViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                _userRepository.Subscribe(viewModel.UserId, viewModel.EstablishmentId);
                return Ok();
            }
            catch (AuthenticationException e)
            {
                return BadRequest(new {message = e.Message});
            }
        }

        [HttpPost("Unsubscribe")]
        public IActionResult Unsubscribe([FromBody] SubscribeViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                _userRepository.Unsubscribe(viewModel.UserId, viewModel.EstablishmentId);
                return Ok();
            }
            catch (AuthenticationException e)
            {
                return BadRequest(new {message = e.Message});
            }
        }
    }
}