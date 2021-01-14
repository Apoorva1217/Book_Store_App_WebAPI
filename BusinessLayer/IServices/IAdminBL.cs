using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.IServices
{
    public interface IAdminBL
    {
        bool RegisterAdmin(AdminUserRegistration admin);
        AdminUserRegistration AdminLogin(AdminUserLogin login);
    }
}
