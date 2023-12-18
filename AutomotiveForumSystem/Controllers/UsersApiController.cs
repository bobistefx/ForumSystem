using AutomotiveForumSystem.Exceptions;
using AutomotiveForumSystem.Helpers.Contracts;
using AutomotiveForumSystem.Models.DTOs;
using AutomotiveForumSystem.Models.DTOS;
using AutomotiveForumSystem.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveForumSystem.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersAPIController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IUserMapper userMapper;
        private readonly IAuthManager authManager;

        public UsersAPIController(IUsersService usersService, IUserMapper userMapper, IAuthManager authManager)
        {
            this.usersService = usersService;
            this.userMapper = userMapper;
            this.authManager = authManager;
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] UserCreateDTO userDTO)
        {
            try
            {
                var user = this.userMapper.Map(userDTO);
                var createdUser = this.usersService.Create(user);
                var userResponse = this.userMapper.Map(createdUser);

                return StatusCode(StatusCodes.Status201Created, userResponse);
            }
            catch (DuplicateEntityException e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpPut("")]
        public IActionResult Update([FromHeader] string credentials, [FromBody] UserUpdateDTO userDTO)
        {
            try
            {
                var user = this.authManager.TryGetUser(credentials);
                var updatedUser = this.usersService.Update(user, userDTO);
                var userResponse = this.userMapper.Map(updatedUser);

                return Ok(userResponse);
            }
            catch (AuthenticationException e)
            {
                return Unauthorized(e.Message);
            }
        }
    }
}
