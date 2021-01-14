using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.IServices
{
    public interface ICustomerBL
    {
        bool AddCustomerDetails(CustomerDetails customerDetails);
    }
}
