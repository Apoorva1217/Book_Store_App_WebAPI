using BusinessLayer.IServices;
using CommonLayer.Models;
using RepositoryLayer.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;

        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public UserRegistration UserLogin(UserLogin login)
        {
            try
            {
                return this.userRL.UserLogin(login);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool RegisterUser(UserRegistration admin)
        {
            try
            {
                return this.userRL.RegisterUser(admin);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
