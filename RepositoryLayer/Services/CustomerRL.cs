using CommonLayer.Models;
using RepositoryLayer.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CustomerRL : ICustomerRL
    {
        private readonly BookStoreContext context;

        public CustomerRL(BookStoreContext context)
        {
            this.context = context;
        }
        public bool AddCustomerDetails(CustomerDetails customerDetails)
        {
            try
            {
                this.context.customerDetails.Add(customerDetails);
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
