using AutomotiveForumSystem.Helpers.Contracts;
using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.DTOS;
using AutomotiveForumSystem.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveForumSystem.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersApiController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IUserMapper userMapper;

        public UsersApiController(IUsersService usersService, IUserMapper userMapper)
        {
            this.usersService = usersService;
            this.userMapper = userMapper;
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] UserCreateDTO userDTO)
        {
            var user = this.userMapper.Map(userDTO);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromHeader] string credentials)
        {

        }
    }
}
