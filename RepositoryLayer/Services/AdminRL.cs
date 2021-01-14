using CommonLayer.Models;
using RepositoryLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AdminRL : IAdminRL
    {
        private readonly BookStoreContext context;

        public AdminRL(BookStoreContext context)
        {
            this.context = context;
        }

        public AdminUserRegistration AdminLogin(AdminUserLogin login)
        {
            try
            {
                return this.context.adminUserRegistrations
                                    .Where(x => x.Email == login.Email && x.Password == login.Password)
                                    .Select(o => new AdminUserRegistration
                                    {
                                        FullName = o.FullName,
                                        Email = o.Email,
                                        Password = o.Password,
                                        Phone = o.Phone,
                                        CreatedAt = o.CreatedAt,
                                        UpdatedAt = o.UpdatedAt
                                    }).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public bool RegisterAdmin(AdminUserRegistration o)
        {
            try
            {
                AdminUserRegistration adminObject = new AdminUserRegistration()
                {
                    FullName = o.FullName,
                    Email = o.Email,
                    Password = o.Password,
                    Phone = o.Phone,
                    CreatedAt = o.CreatedAt,
                    UpdatedAt = o.UpdatedAt
                };

                this.context.adminUserRegistrations.Add(adminObject);
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
    }
}
