using AuthenticationPlugin;
using AutoMapper;
using InventorySystem.Api.Constants;
using InventorySystem.Api.Dtos.User;
using InventorySystem.Api.Dtos.Account;
using InventorySystem.Api.Helpers;
using InventorySystem.Core.Models;
using InventorySystem.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace InventorySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private IConfiguration _configuration;
        private readonly AuthService _auth;
        private readonly IMapper _mapper;

        public AccountController(IConfiguration configuration, IAccountService accountService, IUserService userService, IMapper mapper)
        {
            _configuration = configuration;
            _auth = new AuthService(_configuration);
            _accountService = accountService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public ActionResult Register(RegisterDto registerUserDto)
        {
            var userModel = _mapper.Map<RegisterDto, User>(registerUserDto);
            userModel.RoleId = Constant.RoleId;
            userModel.UserStatusId = Constant.StatusId;
            userModel.Password = Security.HashPassword(userModel.Password);

            User registeredUser = new User();
            try
            {
                _accountService.Register(userModel);
            }
            catch (Exception)
            {
                return BadRequest("Register Failed");
            }

            return Ok();
        }

        [HttpPost("Login")]
        public ActionResult Login(LoginDto loginUserDto)
        {
            var userModel = _mapper.Map<LoginDto, User>(loginUserDto);
            var returnedUser = _accountService.Login(userModel);

            if (returnedUser == null)
                return NotFound();

            if (!Security.VerifyHash(loginUserDto.Password, returnedUser.Password))
                return Unauthorized();

            var claims = new[]
            {
                new Claim("Id", returnedUser.Id.ToString()),
                new Claim("Email", returnedUser.Email),
                new Claim("Username", returnedUser.Username),
                new Claim("Name", returnedUser.Name),
                new Claim("Surname", returnedUser.Surname),
                new Claim("Role", returnedUser.Role.Name),
                new Claim("Status", returnedUser.UserStatus.Name),
                new Claim(ClaimTypes.Role, returnedUser.Role.Name) // Needed for Authorize("Roles = "") Attribute
            };

            var token = _auth.GenerateAccessToken(claims);

            return new ObjectResult(new
            {
                access_token = token.AccessToken,
                expires_in = token.ExpiresIn,
                token_type = token.TokenType,
                creation_Time = token.ValidFrom,
                expiration_Time = token.ValidTo,
                user_id = returnedUser.Id,
                user_email = returnedUser.Email,
                user_username = returnedUser.Username,
                user_name = returnedUser.Name,
                user_surname = returnedUser.Surname,
                user_role = returnedUser.Role.Name,
                user_status = returnedUser.UserStatus.Name
            });
        }

        [Authorize]
        [HttpGet]
        public ActionResult<GetUserDto> Account()
        {
            var token = Security.decodeToken(Request);
            var finalToken = new
            {
                username = Security.GetClaimByType("Username", token),
                email = Security.GetClaimByType("Email", token),
                id = Security.GetClaimByType("Id", token),
                name = Security.GetClaimByType("Name", token),
                surname = Security.GetClaimByType("Surname", token),
                role = Security.GetClaimByType("Role", token),
                status = Security.GetClaimByType("Status", token)
            };
            return Ok(finalToken);
        }

        [Authorize]
        [HttpPut]
        public IActionResult Account([FromBody] AccountUpdateDto accountUpdateDto)
        {
            var userId = int.Parse(Security.decodeToken(Request).GetClaimByType("Id"));

            var existingUser = _userService.GetById(userId);
            if(existingUser == null)
                return NotFound();

            var userModel = _mapper.Map(accountUpdateDto, existingUser); // Get user changed fields by mapping userDto to existing user from db

            try
            {
                _userService.Update(userId, userModel);
            }
            catch (Exception)
            {
                return BadRequest("Coulnd't update profile");
            }

            return Ok();
        }

        [Authorize]
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDto passwordDto)
        {
            var userId = int.Parse(Security.decodeToken(Request).GetClaimByType("Id"));

            var existingUser = _userService.GetById(userId);
            if (existingUser == null)
                return NotFound();

            existingUser.Password = Security.HashPassword(passwordDto.password);

            try
            {
                _userService.Update(existingUser.Id, existingUser);
            }
            catch (Exception)
            {
                return BadRequest("Couldn't change password");
            }
            
            return Ok();
        }
    }
}
