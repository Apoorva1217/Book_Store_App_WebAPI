using CommonLayer.Models;
using RepositoryLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RepositoryLayer;
using Microsoft.AspNetCore.Http;

namespace RepositoryLayer.Services
{
    public class CartRL : ICartRL
    {
        private readonly BookStoreContext context;

        public CartRL(BookStoreContext context)
        {
            this.context = context;
        }
        public bool AddCart(CartItem cart)
        {
            try
            {
                var CartRecord = from p in this.context.products.ToList() select p;
                int result = 0;
                Product res = this.context.products.Where(x =>
                                                   x.Product_id == cart.Product_id).FirstOrDefault();
                CartItem cart1 = new CartItem();
                cart1.Product_id = cart.Product_id;
                cart1.LoginUser = cart.LoginUser;
                cart1.QuantityToBuy = cart.QuantityToBuy;
                foreach(Product item in CartRecord)
                {
                    if (item.Product_id == cart.Product_id)
                    {
                        item.Quantity = item.Quantity - 1;
                    }
                }
                this.context.Add(cart1);
                result = this.context.SaveChanges();

                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CartItem> GetCartItems(string LoggedInUser)
        {
            List<CartItem> products = new List<CartItem>();
            try
            {
                List<CartItem> list = (from e in this.context.cartItems
                                       select new CartItem
                                       {
                                           Product_id = e.Product_id,
                                           QuantityToBuy = e.QuantityToBuy,
                                           LoginUser = e.LoginUser
                                       }).Where(x => x.LoginUser == LoggedInUser).ToList<CartItem>();
                foreach (CartItem item in list)
                {
                    var res = this.context.products.Where(x =>
                                                    x.Product_id == item.Product_id).FirstOrDefault();
                    item.Product = res;
                    products.Add(item);
                }
                return products;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateCart(CartItem cart)
        {
            CartItem existsCart = this.context.cartItems.Where(x =>
                                                   x.Product_id == cart.Product_id &&
                                                   x.LoginUser == cart.LoginUser).FirstOrDefault();
            //CartItem existsCart = (from data in context.cartItems
            //                where data.Product_id == cart.Product_id && data.LoginUser==cart.LoginUser
            //                select data).FirstOrDefault();
            var CartRecord = from p in this.context.products.ToList() select p;
            if (existsCart != null)
            {
                existsCart.Product_id = cart.Product_id;
                existsCart.LoginUser = cart.LoginUser;
                existsCart.QuantityToBuy = existsCart.QuantityToBuy + 1;
                foreach(Product item in CartRecord)
                {
                    if (item.Product_id == existsCart.Product_id)
                    {
                        item.Quantity = item.Quantity - 1;
                        existsCart.Price = item.Price * existsCart.QuantityToBuy;
                    }
                }
                int result = this.context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool RemoveCartItem(CartItem cart)
        {
            try
            {
                CartItem cartItem = this.context.cartItems.Where(x =>
                                                   x.Product_id == cart.Product_id && 
                                                   x.LoginUser == cart.LoginUser).FirstOrDefault();
                var CartRecord = from p in this.context.products.ToList() select p;
                foreach(Product item in CartRecord)
                {
                    if (item.Product_id == cart.Product_id)
                    {
                        item.Quantity = item.Quantity + cartItem.QuantityToBuy;
                    }
                }
                this.context.cartItems.Remove(cartItem);
                int result = this.context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ReduceBookQuantity(CartItem cart)
        {
            try
            {
                CartItem cartItem = this.context.cartItems.Where(x =>
                                                   x.Product_id == cart.Product_id && 
                                                   x.LoginUser == cart.LoginUser).FirstOrDefault();
                var CartRecord = from p in this.context.products.ToList() select p;
                if (cart.QuantityToBuy > 0)
                {
                    cartItem.QuantityToBuy = cartItem.QuantityToBuy - 1;
                    foreach(Product item in CartRecord)
                    {
                        if (item.Product_id == cartItem.Product_id)
                        {
                            item.Quantity = item.Quantity + 1;
                            cartItem.Price = item.Price * cartItem.QuantityToBuy;
                        }
                    }
                    int result = this.context.SaveChanges();
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
