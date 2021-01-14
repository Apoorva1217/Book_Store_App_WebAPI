using BusinessLayer.IServices;
using CommonLayer.Models;
using RepositoryLayer.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL : ICartBL
    {
        private readonly ICartRL cartRL;

        public CartBL(ICartRL adminRL)
        {
            this.cartRL = adminRL;
        }

        public List<CartItem> GetCartItems(string LoggedInUser)
        {
            try
            {
                return this.cartRL.GetCartItems(LoggedInUser);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool AddCart(CartItem productId)
        {
            try
            {
                return this.cartRL.AddCart(productId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool UpdateCart(CartItem productId)
        {
            try
            {
                return this.cartRL.UpdateCart(productId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool RemoveCartItem(CartItem productId)
        {
            try
            {
                return this.cartRL.RemoveCartItem(productId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ReduceBookQuantity(CartItem productId)
        {
            try
            {
                return this.cartRL.ReduceBookQuantity(productId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
