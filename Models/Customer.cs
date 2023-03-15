using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_UserInterfaceTest.Models
{
    public class Customer
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }

        public Customer()
        {

        }
        public Customer(Customer obj)
        {
            FullName = obj.FullName;
            Address = obj.Address;
            Email = obj.Email;
            MobilePhone = obj.MobilePhone;
        }

        public override string ToString()
        {
            return $"{FullName} ({Email},{MobilePhone},{Address})";
        }
    }
}
