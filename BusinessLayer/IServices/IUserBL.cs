using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.IServices
{
    public interface IUserBL
    {
        bool RegisterUser(UserRegistration admin);
        UserRegistration UserLogin(UserLogin login);
    }
}
