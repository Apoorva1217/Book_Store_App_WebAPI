using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL orderBL;

        public OrderController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }
        [HttpPost("PlaceOrder")]
        public IActionResult PlaceOrder()
        {
            try
            {
                var claims = HttpContext.User.Claims.ToList();
                var email = claims[0].ToString().Split("emailaddress:");
                string LoggedInUser = email[1].Trim();
                var result = this.orderBL.PlaceOrder(LoggedInUser);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Order placed successfully", result });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new { success = false, Message = "Order not placed succesfully " });
                }
            }
            catch (Exception e)
            {
                var sqlException = e.InnerException as SqlException;

                if (sqlException.Number == 2601 || sqlException.Number == 2627)
                {
                    return StatusCode(StatusCodes.Status409Conflict,
                        new { success = false, ErrorMessage = "Cannot insert duplicate values." });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = e.Message });
                }
            }
        }
    }
}
