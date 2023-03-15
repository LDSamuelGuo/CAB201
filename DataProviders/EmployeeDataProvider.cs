using CAB201_UserInterfaceTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_UserInterfaceTest.DataProviders
{
    /// <summary>
    /// Data provider of the employees
    /// </summary>
    public class EmployeeDataProvider : BaseDataProvider<Employee>
    {
        public Employee Employee
        {
            get => default;
            set
            {
            }
        }

        protected override void checkDataUnique(Employee obj)
        {
            if (GetEntities().Where(clt => clt.Email == obj.Email).Any())
            {
                throw new InvalidOperationException("An employee with that email already exists");
            }
        }
    }
}
