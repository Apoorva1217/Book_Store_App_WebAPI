using CommonLayer.Models;
using RepositoryLayer.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RepositoryLayer.Services
{
    public class ProductRL : IProductRL
    {
        private readonly BookStoreContext context;

        public ProductRL(BookStoreContext context)
        {
            this.context = context;
        }
        public List<Product> GetAllProducts()
        {
            try
            {
                var productRecord=from p in this.context.products.ToList() select p;
                var cart = from c in this.context.cartItems.ToList() select c;
                List<Product> products = new List<Product>();
                foreach(Product item in productRecord)
                {
                    var result = this.context.cartItems.Where(x => x.Product_id == item.Product_id).FirstOrDefault();
                    if (result != null)
                    {
                        item.AddedToCart = true;
                    }
                    else
                    {
                        item.AddedToCart = false;
                    }
                    products.Add(item);
                }
                return products;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
