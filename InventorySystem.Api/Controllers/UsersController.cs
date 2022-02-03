using AuthenticationPlugin;
using AutoMapper;
using InventorySystem.Api.Constants;
using InventorySystem.Api.Dtos.User;
using InventorySystem.Api.Helpers;
using InventorySystem.Core.Models;
using InventorySystem.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InventorySystem.Api.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }


        [HttpGet]
        public ActionResult<IEnumerable<GetUserDto>> GetAll()
        {
            var users = _userService.GetAll();
            var getUserDtos = _mapper.Map<IEnumerable<User>, IEnumerable<GetUserDto>>(users);

            return Ok(getUserDtos);
        }


        [HttpGet("{id}")]
        public ActionResult<GetUserDto> GetById(int id)
        {
            var user = _userService.GetById(id);
            var getUserDto = _mapper.Map<User, GetUserDto>(user);

            return Ok(getUserDto);
        }


        [HttpPost]
        public ActionResult Create([FromBody] CreateUserDto createUserDto)
        {
            var userToCreate = _mapper.Map<CreateUserDto, User>(createUserDto);
            userToCreate.Password = Security.HashPassword(Constant.UserPassword); // Get default user password and hash it

            var newUser = new User();
            try
            {
                newUser = _userService.Create(userToCreate);
            }
            catch (Exception)
            {
                return BadRequest("Coulnd't create user");
            }

            var userDto = _mapper.Map<User, CreateUserDto>(newUser);
            return Ok(userDto);
        }


        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] CreateUserDto createUserDto)
        {
            var userToBeUpdated = _userService.GetById(id);

            if (userToBeUpdated == null)
                return NotFound();

            if(userToBeUpdated.Role.Name == "Admin") // Admin cannot update other admins
                return Forbid();

            var user = _mapper.Map<CreateUserDto, User>(createUserDto);

            try
            {
                _userService.Update(id, user);
            }
            catch (Exception)
            {
                return BadRequest("Couldn't update user");
            }

            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingUser = _userService.GetById(id);
            if (existingUser == null)
                return NotFound();
            if (existingUser.Role.Name == "Admin") // Admin cannot delete other admins
                return Forbid();

            try
            {
                _userService.Delete(existingUser);
            }
            catch (Exception)
            {
                return BadRequest("Couldn't delete user");
            }
            
            return Ok();
        }
    }
}
