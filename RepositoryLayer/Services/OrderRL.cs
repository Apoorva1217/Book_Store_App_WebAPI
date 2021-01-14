using CommonLayer.Models;
using RepositoryLayer.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RepositoryLayer;


namespace RepositoryLayer.Services
{
    public class OrderRL : IOrderRL
    {
        private readonly BookStoreContext context;

        public OrderRL(BookStoreContext context)
        {
            this.context = context;
        }
        public NewOrder PlaceOrder(string LoggedInUser)
        {
            try
            {
                List<CartItem> list = (from e in this.context.cartItems
                                       select new CartItem
                                       {
                                           Product_id = e.Product_id,
                                           QuantityToBuy = e.QuantityToBuy,
                                           LoginUser = e.LoginUser,
                                           Product=e.Product
                                       })
                                       .Where(x => x.LoginUser == LoggedInUser)
                                       .ToList<CartItem>();
                var customer = (from data in context.customerDetails
                                where data.Email == LoggedInUser
                                select data).FirstOrDefault();
                
                NewOrder newOrder = new NewOrder();
                newOrder.Customer = customer;
                newOrder.Customer.CustomerId = customer.CustomerId;
                this.context.Add(newOrder);
                int result = this.context.SaveChanges();
                if (newOrder.Orders != null && result > 0)
                {
                    return newOrder;
                }
                else
                {
                    throw new Exception("Order not placed succesfully");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
