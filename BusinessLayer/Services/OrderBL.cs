using BusinessLayer.IServices;
using CommonLayer.Models;
using RepositoryLayer.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class OrderBL : IOrderBL
    {
        private readonly IOrderRL orderRL;

        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;
        }
        public NewOrder PlaceOrder(string LoggedInUser)
        {
            try
            {
                return this.orderRL.PlaceOrder(LoggedInUser);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
