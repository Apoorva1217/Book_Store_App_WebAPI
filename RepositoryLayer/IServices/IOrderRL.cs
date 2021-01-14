using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.IServices
{
    public interface IOrderRL
    {
        NewOrder PlaceOrder(string LoggedInUser);
    }
}
