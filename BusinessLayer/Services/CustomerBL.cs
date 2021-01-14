using BusinessLayer.IServices;
using CommonLayer.Models;
using RepositoryLayer.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CustomerBL : ICustomerBL
    {
        private readonly ICustomerRL customerRL;

        public CustomerBL(ICustomerRL customerRL)
        {
            this.customerRL = customerRL;
        }
        public bool AddCustomerDetails(CustomerDetails customerDetails)
        {
            try
            {
                return this.customerRL.AddCustomerDetails(customerDetails);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
