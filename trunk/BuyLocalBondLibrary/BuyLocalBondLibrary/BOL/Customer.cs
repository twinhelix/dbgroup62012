using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuyLocalBondLibrary.BOL
{
    public class Customer
    {
        #region PRIVATE MEMBERS

        private int _ID;
        private string _NAME;
        private decimal _PORTFOLIO_VALUE;

        #endregion

        #region PUBLIC PROPERTIES

        public int Id
        { get { return _ID; } set { _ID = value; } }

        public string Name
        { get { return _NAME; } set { _NAME = value; } }

        public decimal PortfolioValue
        { get { return _PORTFOLIO_VALUE; } set {_PORTFOLIO_VALUE = value; } }
        
        #endregion

                #region CONSTRUCTORS

        public Customer()
        {
            Initialize();
        }

        #endregion

        #region HELPER METHODS

        private void Initialize()
        {
            this._ID = 0;
            this._NAME = string.Empty;
            this._PORTFOLIO_VALUE = 0;
        }

        #endregion
    }
}
