using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.IServices
{
    public interface ICartRL
    {
        bool AddCart(CartItem productId);
        bool UpdateCart(CartItem productId);
        List<CartItem> GetCartItems(string LoggedInUser);
        bool RemoveCartItem(CartItem product_id);
        bool ReduceBookQuantity(CartItem product_id);
    }
}
