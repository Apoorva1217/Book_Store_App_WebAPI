﻿using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.IServices
{
    public interface IUserRL
    {
        bool RegisterUser(UserRegistration employee);
        UserRegistration UserLogin(UserLogin login);
    }
}
