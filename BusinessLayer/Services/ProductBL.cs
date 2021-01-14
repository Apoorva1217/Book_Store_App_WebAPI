using BusinessLayer.IServices;
using CommonLayer.Models;
using RepositoryLayer.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class ProductBL : IProductBL
    {
        private readonly IProductRL productRL;

        public ProductBL(IProductRL productRL)
        {
            this.productRL = productRL;
        }
        public List<Product> GetAllProducts()
        {
            try
            {
                return this.productRL.GetAllProducts();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
