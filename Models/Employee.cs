using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_UserInterfaceTest.Models
{
    public class Employee
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<AirCraft> AirCrafts { get; set; } = new List<AirCraft>();
        
        public Employee()
        {

        }
        public Employee(Employee obj)
        {
            FullName = obj.FullName;
            Password = obj.Password;
            Email = obj.Email;
        }

        public override string ToString()
        {
            return $"{FullName} ({Email})";
        }
    }
}
