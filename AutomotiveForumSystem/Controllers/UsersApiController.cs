﻿using AutomotiveForumSystem.Exceptions;
using AutomotiveForumSystem.Helpers.Contracts;
using AutomotiveForumSystem.Models.DTOs;
using AutomotiveForumSystem.Services.Contracts;

using Microsoft.AspNetCore.Mvc;

namespace AutomotiveForumSystem.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersAPIController : ControllerBase
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

        [HttpPost]
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

        [HttpPut]
        public IActionResult UpdateProfileInformation([FromHeader] string credentials, [FromBody] UserUpdateProfileInformationDTO userDTO)
        {
            try
            {
                var user = this.authManager.TryGetUser(credentials);
                var updatedUser = this.usersService.UpdateProfileInformation(user, userDTO);
                var userResponse = this.userMapper.Map(updatedUser);

                return Ok(userResponse);
            }
            catch (AuthenticationException e)
            {
                return Unauthorized(e.Message);
            }
            catch (UserBlockedException e)
            {
                return BadRequest(e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }        
        
        [HttpDelete]
        public IActionResult Delete([FromRoute] int id, [FromHeader] string credentials)
        {
            try
            {
                var user = this.authManager.TryGetUser(credentials);
                this.usersService.Delete(user);

                return Ok();
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
    }
}
