using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.IServices;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDetailsController : ControllerBase
    {
        private readonly ICustomerBL customerBL;

        public CustomerDetailsController(ICustomerBL customerBL)
        {
            this.customerBL = customerBL;
        }

        [HttpPost("AddCustomerDetails")]
        public IActionResult AddCustomerDetails(CustomerDetails customerDetails)
        {
            try
            {
                if (this.customerBL.AddCustomerDetails(customerDetails))
                {
                    return this.Ok(new { success = true, Message = "Customer Details added successfully" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new { success = false, Message = "Customer Details not added succesfully" });
                }
            }
            catch (Exception e)
            {
                var sqlException = e.InnerException as SqlException;

                if (sqlException.Number == 2601 || sqlException.Number == 2627)
                {
                    return StatusCode(StatusCodes.Status409Conflict,
                        new { success = false, ErrorMessage = "Cannot insert duplicate customer deatils." });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = e.Message });
                }
            }
        }
    }
}
