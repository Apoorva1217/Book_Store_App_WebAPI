using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.IServices
{
    public interface ICartBL
    {
        bool AddCart(CartItem productId);
        bool UpdateCart(CartItem productId);
        List<CartItem> GetCartItems(string LoggedInUser);
        bool RemoveCartItem(CartItem productId);
        bool ReduceBookQuantity(CartItem productId);
    }
}
