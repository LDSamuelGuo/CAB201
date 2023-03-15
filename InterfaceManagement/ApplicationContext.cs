using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAB201_UserInterfaceTest.DataProviders;
using CAB201_UserInterfaceTest.Models;

namespace CAB201_UserInterfaceTest.InterfaceManagement
{
    /// <summary>
    /// Contains the status and the data of the running application.
    /// </summary>
    public class ApplicationContext
    {
        /// <summary>
        /// indicates the application running status.
        /// </summary>
        public bool ApplicationRunning = true;
        
        /// <summary>
        /// Provide the employee data
        /// </summary>
        public EmployeeDataProvider EmployeeData { get; protected set; } = new EmployeeDataProvider();
        /// <summary>
        /// Provide the customer data, will be initialized by the user logged in.
        /// </summary>
        public CustomerDataProvider CustomerData { get; protected set; } = null;
        /// <summary>
        /// Provide the aircraft data, will be initialized by the user logged in.
        /// </summary>
        public AirCraftDataProvider AirCraftData { get; protected set; } = null;

        /// <summary>
        /// The entity of the user logged in
        /// </summary>
        public Employee LoggedInEmployee { get; protected set; } = null;

        /// <summary>
        /// The user interface that being currently used.
        /// </summary>
        private UserInterface _current = null;

        /// <summary>
        /// returns the current user interface.
        /// </summary>
        public UserInterface currentUserInterface
        {
            get
            {
                if(_current==null)
                {
                    _current= new Interfaces.AuthenticationInterface(this);                   
                }
                return _current;
                
            }
        }

        /// <summary>
        /// Intializes the customer data and aircraft data by the user logged in.
        /// </summary>
        public void InitializeDataForLoggedInEmployee()
        {
            if(LoggedInEmployee!=null)
            {
                CustomerData = new CustomerDataProvider(LoggedInEmployee.Customers);
                AirCraftData = new AirCraftDataProvider(LoggedInEmployee.AirCrafts);            
            }
        }

        /// <summary>
        /// Authenticates the credential of the employee
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        public void LoginEmployee(string email, string password)
        {
            IEnumerable<Employee> matches = EmployeeData.GetEntities().Where(client => client.Email.ToLower() == email.ToLower());
            if (!matches.Any()) { throw new Exception("Invalid email or password, please try again"); } // Any matches found with email provided, no then return false
            if (matches.Count() > 1) { throw new Exception("Deuplicate user in data source"); } // More then 1 match an error has occured, throw exception

            Employee employee = matches.First(); // Get the match

            if (password == employee.Password) //Check the password
            {
                LoggedInEmployee = employee; 
                InitializeDataForLoggedInEmployee(); //Initialize the customer and aircraft data by the user logged in
                _current = new Interfaces.MainInterface(this); //Change the current user interface to main interface that provides the full access of the application
            }
        }

        /// <summary>
        /// Log out the employee
        /// </summary>
        public void LogoutEmployee()
        {
            LoggedInEmployee = null;
            CustomerData = null;
            AirCraftData = null; //Clear the data
            _current = new Interfaces.AuthenticationInterface(this); //Change the current user interface to authentication interface
        }
        /// <summary>
        /// Switch the ApplicationRunning property to false so that the main process can end the loop and exit.
        /// </summary>
        public void Exit()
        {
            ApplicationRunning = false;
                    
        }

        public EmployeeDataProvider EmployeeDataProvider
        {
            get => default;
            set
            {
            }
        }

        public CustomerDataProvider CustomerDataProvider
        {
            get => default;
            set
            {
            }
        }

        public AirCraftDataProvider AirCraftDataProvider
        {
            get => default;
            set
            {
            }
        }
    }
}
