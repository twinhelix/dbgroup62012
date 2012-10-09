using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BuyLocalBondLibrary.BOL;
using BuyLocalBondLibrary.DAL;

namespace BuyLocalBondLibrary.BLL
{
    public class CustomerBL
    {
        private ICustomerDA _dao;


        #region CONSTRUCTORS

        private CustomerBL(ICustomerDA dao)
        { _dao = dao; }

        //Constructor injection - explicitly declares the dependencies of this class
        public CustomerBL()
            : this(new CustomerDA())
        { }

        #endregion

        #region PUBLIC METHODS

        public Customer GetCustomer(int id)
        { return _dao.GetCustomer(id); }

        #endregion

    }
}
