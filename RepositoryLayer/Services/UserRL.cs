using CommonLayer.Models;
using RepositoryLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly BookStoreContext context;

        public UserRL(BookStoreContext context)
        {
            this.context = context;
        }

        public bool RegisterUser(UserRegistration o)
        {
            try
            {
                UserRegistration existUser = this.context.userRegistrations
                                        .Where(x => x.Email == o.Email).FirstOrDefault();
                if (existUser == null)
                {
                    UserRegistration employeeObject = new UserRegistration()
                    {
                        FullName = o.FullName,
                        Email = o.Email,
                        Password = o.Password,
                        Phone = o.Phone,
                        CreatedAt = o.CreatedAt,
                        UpdatedAt = o.UpdatedAt
                    };

                    this.context.userRegistrations.Add(employeeObject);
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
                else
                {
                    throw new Exception("Email already Exist");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public UserRegistration UserLogin(UserLogin login)
        {
            try
            {
                return this.context.userRegistrations
                                    .Where(x => x.Email == login.Email && x.Password == login.Password)
                                    .Select(o => new UserRegistration
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
    }
}