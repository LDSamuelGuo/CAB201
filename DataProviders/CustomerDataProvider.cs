using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAB201_UserInterfaceTest.Models;

namespace CAB201_UserInterfaceTest.DataProviders
{
    /// <summary>
    /// Data provider of the customers
    /// </summary>
    public class CustomerDataProvider : BaseDataProvider<Customer>
    {
        public CustomerDataProvider(List<Customer> data):base(data)
        {

        }

        public Customer Customer
        {
            get => default;
            set
            {
            }
        }

        protected override void checkDataUnique(Customer obj)
        {
            if (GetEntities().Where(clt => clt.Email == obj.Email).Any())
            {
                throw new InvalidOperationException("An customer with that email already exists");
            }
        }
    }
}
