using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.IServices
{
    public interface IAdminRL
    {
        bool RegisterAdmin(AdminUserRegistration admin);
        AdminUserRegistration AdminLogin(AdminUserLogin login);
    }
}
