using BusinessLayer.IServices;
using CommonLayer.Models;
using RepositoryLayer.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AdminBL : IAdminBL
    {
        private readonly IAdminRL adminRL;

        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public AdminUserRegistration AdminLogin(AdminUserLogin login)
        {
            try
            {
                return this.adminRL.AdminLogin(login);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool RegisterAdmin(AdminUserRegistration admin)
        {
            try
            {
                return this.adminRL.RegisterAdmin(admin);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
