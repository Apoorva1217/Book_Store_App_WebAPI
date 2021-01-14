using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.IServices;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookStoreApp.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL employeeBL;
        IConfiguration configuration;

        public UserController(IUserBL employeeBL, IConfiguration configuration)
        {
            this.employeeBL = employeeBL;
            this.configuration = configuration;
        }

        [HttpPost("Register")]
        public IActionResult RegisterEmployee(UserRegistration employee)
        {
            try
            {
                if (this.employeeBL.RegisterUser(employee))
                {
                    return this.Ok(new { success = true, Message = "User record added successfully" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new { success = false, Message = "User record is not added " });
                }
            }
            catch (Exception exception)
            {

                if (exception != null)
                {
                    return StatusCode(StatusCodes.Status409Conflict,
                        new { success = false, ErrorMessage = "Cannot insert duplicate Email values." });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = exception.Message });
                }
            }
        }

        [HttpPost("Login")]
        public ActionResult UserLogin(UserLogin login)
        {
            try
            {
                var result = this.employeeBL.UserLogin(login);
                if (result != null)
                {
                    HttpContext.Session.SetString("LoggedInUser", result.Email);

                    string token = GenrateJWTToken(result.Email, result.UserId);
                    return this.Ok(new
                    {
                        success = true,
                        Message = "User login successfully",
                        Data = result,
                        Token = token
                    });
                }
                else
                {
                    return this.NotFound(new { success = false, Message = "User login unsuccessfully" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, Message = e.Message });
            }
        }

        private string GenrateJWTToken(string email, long id)
        {
            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Key"]));
            var signinCredentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
            string userId = Convert.ToString(id);
            var claims = new List<Claim>
                        {
                            new Claim("email", email),
                            new Claim("id",userId),
                        };
            var tokenOptionOne = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
                );
            string token = new JwtSecurityTokenHandler().WriteToken(tokenOptionOne);
            return token;
        }
    }
}
