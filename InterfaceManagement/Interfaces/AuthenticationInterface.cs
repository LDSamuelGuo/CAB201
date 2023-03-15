using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAB201_UserInterfaceTest.Components.Menus;
using CAB201_UserInterfaceTest.DataProviders;


namespace CAB201_UserInterfaceTest.InterfaceManagement.Interfaces
{
    /// <summary>
    /// User interface for authentication and user register
    /// </summary>
    public class AuthenticationInterface:UserInterface
    {
        /// <summary>
        /// Constructor function
        /// </summary>
        /// <param name="current">The application context</param>
        public AuthenticationInterface(ApplicationContext current):base(current)
        {
            WelcomeText = "Welcome to Aircraft Booking System!";
            MenuItems.Add(new CommonMenu("Register as new employee", RegistUser));
            MenuItems.Add(new CommonMenu("Login as existing employee", AuthenticateUser));
            MenuItems.Add(new CommonMenu("Exit", ApplicationExit));
        }
        /// <summary>
        /// Register an employee
        /// </summary>
        public void RegistUser()
        {
            var employee = EmployeeDataProvider.NewEntity();
            employee.FullName= GetInput("Full Name");
            employee.Email = GetInput("Email");
            employee.Password = GetPassword("Password");
            CurrentContext.EmployeeData.AddEntity(employee);
            DisplaySuccess($"{employee.FullName} registered successfully");

        }
        /// <summary>
        /// Call the login function in the application context and execute the process of login
        /// </summary>
        public void AuthenticateUser()
        {
            string email = GetInput("Email");
            string password = GetPassword("Password");
            CurrentContext.LoginEmployee(email, password);
        }

        /// <summary>
        /// Call the exit function in the application context to exit the program
        /// </summary>
        public void ApplicationExit()
        {
            CurrentContext.Exit();
            DisplayFarewell();
        }
    }
}
