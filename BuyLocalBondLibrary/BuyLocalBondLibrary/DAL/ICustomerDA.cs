using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BuyLocalBondLibrary.BOL;

namespace BuyLocalBondLibrary.DAL
{
    public interface ICustomerDA
    {
        Customer GetCustomer(int id);
    }
}
