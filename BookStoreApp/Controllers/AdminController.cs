using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.IServices;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public IAdminBL adminBL;

        IConfiguration configuration;

        public AdminController(IAdminBL adminBL, IConfiguration configuration)
        {
            this.adminBL = adminBL;
            this.configuration = configuration;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public IActionResult RegisterAdmin(AdminUserRegistration admin)
        {
            try
            {
                if (this.adminBL.RegisterAdmin(admin))
                {
                    return this.Ok(new { success = true, Message = "Admin record added successfully" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new { success = false, Message = "Admin record is not added " });
                }
            }
            catch (Exception e)
            {
                var sqlException = e.InnerException as SqlException;

                if (sqlException.Number == 2601 || sqlException.Number == 2627)
                {
                    return StatusCode(StatusCodes.Status409Conflict,
                        new { success = false, ErrorMessage = "Cannot insert duplicate Email values." });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = e.Message });
                }
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public ActionResult AdminLogin(AdminUserLogin login)
        {
            try
            {
                var result = this.adminBL.AdminLogin(login);
                if (result != null)
                {
                    string token = GenrateJWTToken(result.Email, (long)result.AdminId);
                    return this.Ok(new
                    {
                        success = true,
                        Message = "Admin login successfully",
                        Data = result,
                        Token = token
                    });
                }
                else
                {
                    return this.NotFound(new { success = false, Message = "Admin login unsuccessfully" });
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
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: signinCredentials
                );
            string token = new JwtSecurityTokenHandler().WriteToken(tokenOptionOne);
            return token;
        }
    }
}
