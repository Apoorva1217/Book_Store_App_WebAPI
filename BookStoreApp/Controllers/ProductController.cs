using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.IServices;
using CommonLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductBL productBL;
        IConfiguration configuration;

        public ProductController(IProductBL productBL, IConfiguration configuration)
        {
            this.productBL = productBL;
            this.configuration = configuration;
        }
        [HttpGet("GetBooks")]
        public IActionResult GetAllProducts()
        {
            try
            {
                List<Product> empList = this.productBL.GetAllProducts();
                if (empList != null)
                {
                    return this.Ok(new { success = true, Message = "Successfully fetched all Books data", data = empList });
                }
                else
                {
                    return this.NotFound(new { success = false, Message = "Unsuccessful...can't fetch Books data" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, Message = e.Message });
            }
        }
    }
}
