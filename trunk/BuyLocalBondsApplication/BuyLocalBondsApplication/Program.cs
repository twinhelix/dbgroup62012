using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BuyLocalBondLibrary.DAL;
using BuyLocalBondLibrary.BOL;
using BuyLocalBondLibrary.BLL;


namespace BuyLocalBondsApplication
{
    public class Program
    {
        static void Main(string[] args)
        {
            CustomerBL custMngr = new CustomerBL();

            Customer customer = custMngr.GetCustomer(5555);

            Console.WriteLine("\nID: {0}, Name: {1}, Value: {2}", customer.Id, customer.Name, customer.PortfolioValue);
            Console.WriteLine("\n\nEnd of Console Test Program");
            Console.ReadLine();
        }
    }
}
