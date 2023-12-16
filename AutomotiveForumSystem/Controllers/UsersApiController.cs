using AutomotiveForumSystem.Helpers.Contracts;
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
            //try
            //{
            //    var user = this.userMapper.Map(userDTO);
            //    this.usersService.GetByUsername(userDTO.Username);
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromHeader] string credentials)
        {
            throw new NotImplementedException();
        }
    }
}
