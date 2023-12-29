using Microsoft.AspNetCore.Mvc;
using AutomotiveForumSystem.Exceptions;
using AutomotiveForumSystem.Helpers.Contracts;
using AutomotiveForumSystem.Services.Contracts;

namespace AutomotiveForumSystem.Controllers
{
    [ApiController]
    [Route("api/admin/users")]
    public class AdminsAPIController : ControllerBase
    {
        private readonly IUsersService usersService;
        private readonly IAuthManager authManager;
        private readonly IUserMapper userMapper;

        public AdminsAPIController(IUsersService usersService, IAuthManager authManager, IUserMapper userMapper)
        {
            this.usersService = usersService;
            this.authManager = authManager;
            this.userMapper = userMapper;
        }

        [HttpGet("{username}")]
        public IActionResult GetByUsername([FromRoute] string username, [FromHeader] string credentials)
        {
            try
            {
                var requestingUser = this.authManager.TryGetUser(credentials);
                var user = this.usersService.GetByUsername(username);
                var response = this.userMapper.Map(user);

                return Ok(response);
            }
            catch (AuthenticationException e)
            {
                return Unauthorized(e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{email}")]
        public IActionResult GetByEmail([FromRoute] string email, [FromHeader] string credentials)
        {
            try
            {
                var requestingUser = this.authManager.TryGetUser(credentials);
                var user = this.usersService.GetByEmail(email);
                var response = this.userMapper.Map(user);

                return Ok(response);
            }
            catch (AuthenticationException e)
            {
                return Unauthorized(e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{firstName}")]
        public IActionResult GetByFirstName([FromRoute] string firstName, [FromHeader] string credentials)
        {
            try
            {
                var requestingUser = this.authManager.TryGetUser(credentials);
                var users = this.usersService.GetByFirstName(firstName);
                var response = this.userMapper.Map(users);

                return Ok(response);
            }
            catch (AuthenticationException e)
            {
                return Unauthorized(e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("block/{id}")]
        public IActionResult Block([FromRoute] int id, [FromHeader] string credentials)
        {
            try
            {
                var requestingUser = this.authManager.TryGetUser(credentials);
                var userToBlock = this.usersService.GetById(id);
                var blockedUser = this.usersService.Block(requestingUser, userToBlock);
                var response = this.userMapper.Map(blockedUser);

                return Ok(response);
            }
            catch (AuthenticationException e)
            {
                return Unauthorized(e.Message);                
            }
            catch (AuthorizationException e)
            {
                return Unauthorized(e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (UserBlockedException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("unblock/{id}")]
        public IActionResult Unblock([FromRoute] int id, [FromHeader] string credentials)
        {
            try
            {
                var requestingUser = this.authManager.TryGetUser(credentials);
                var userToUnblock = this.usersService.GetById(id);
                var unblockedUser = this.usersService.Unblock(requestingUser, userToUnblock);
                var response = this.userMapper.Map(unblockedUser);

                return Ok(response);
            }
            catch (AuthenticationException e)
            {
                return Unauthorized(e.Message);
            }
            catch (AuthorizationException e)
            {
                return Unauthorized(e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (UserNotBlockedException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("setAdmin/{id}")]
        public IActionResult SetAdmin([FromRoute] int id, [FromHeader] string credentials)
        {
            try
            {
                var requestingUser = this.authManager.TryGetUser(credentials);
                var userToSetAsAdmin = this.usersService.GetById(id);
                var admin = this.usersService.SetAdmin(requestingUser, userToSetAsAdmin);
                var response = this.userMapper.Map(admin);

                return Ok(response);
            }
            catch (AuthenticationException e)
            {
                return Unauthorized(e.Message);
            }
            catch (AuthorizationException e)
            {
                return Unauthorized(e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (UserNotBlockedException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
