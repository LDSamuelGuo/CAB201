using CAB201_UserInterfaceTest.Components.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAB201_UserInterfaceTest.DataProviders;

namespace CAB201_UserInterfaceTest.InterfaceManagement.Interfaces
{
    /// <summary>
    /// User interface of main functions 
    /// </summary>
    public class MainInterface : UserInterface
    {
        /// <summary>
        /// Constructor function
        /// </summary>
        /// <param name="current">The application context</param>
        public MainInterface(ApplicationContext current):base(current)
        {
            WelcomeText = $"Welcome back, {current.LoggedInEmployee.FullName}";
            MenuItems.Add(new CommonMenu("Register a customer", RegistCustomer));
            MenuItems.Add(new CommonMenu("Register a new light aircraft", RegistLightAircraft));
            MenuItems.Add(new CommonMenu("Register a new helicopter", RegistHelicopter));
            MenuItems.Add(new CommonMenu("View existing flying service", ViewFlyingService));
            MenuItems.Add(new CommonMenu("View existing times", ViewTimes));
            MenuItems.Add(new CommonMenu("Add a customer to a flying service", AddCustomer2Service));
            MenuItems.Add(new CommonMenu("View flight Passengers", ViewFlightPassengers));
            MenuItems.Add(new CommonMenu("Logout", Logout));
            //MenuItems.Add(new CommonMenu("Create Test Data", MakeFakeData)); //Menu for generation of the test data when needed
        }
        /// <summary>
        /// Manipulate the console interface to ask for input to register a new customer
        /// </summary>
        public void RegistCustomer()
        {
            var customer = CustomerDataProvider.NewEntity();
            customer.FullName = GetInput("FullName");
            customer.Email = GetInput("Email");
            customer.Address = GetInput("Address");
            customer.MobilePhone = GetInput("MobilePhone");
            CurrentContext.CustomerData.AddEntity(customer);
            DisplaySuccess($"{customer.FullName} registered successfully");
        }
        /// <summary>
        /// Manipulate the console interface to ask for input to register a new light aircraft
        /// </summary>
        public void RegistLightAircraft()
        {
            var aircraft = LightAirCraftDataProvider.NewEntity();
            aircraft.DeparturePlace = GetInput("Departure Place");
            aircraft.ArrivalPlace = GetInput("Arrival Palace");
            aircraft.DepartureTime = GetTime("Departure Time");
            aircraft.Distance = GetInteger("Distance");
            CurrentContext.AirCraftData.AddEntity(aircraft);
            DisplaySuccess($"{aircraft.ToString()} added");
        }

        /// <summary>
        /// Manipulate the console interface to ask for input to register a new helicopter
        /// </summary>
        public void RegistHelicopter()
        {
            var aircraft = HelicopterDataProvider.NewEntity();
            aircraft.DeparturePlace = GetInput("Departure Place");
            aircraft.ArrivalPlace = GetInput("Arrival Palace");
            aircraft.DepartureTime = GetTime("Departure Time");
            aircraft.Distance = GetInteger("Distance");
            CurrentContext.AirCraftData.AddEntity(aircraft);
            DisplaySuccess($"{aircraft.ToString()} added");
        }

        /// <summary>
        /// Manipulate the console interface to list all the aircraft with distance infomation
        /// </summary>
        public void ViewFlyingService()
        {
            foreach(var aircraft in CurrentContext.AirCraftData.GetEntities())
            {
                Console.WriteLine(aircraft.ToInfo());
            }
            DisplayWarning("flying services has been completely listed, press any key to continue");
            Console.ReadKey(intercept: true);
        }

        /// <summary>
        /// Manipulate the console interface to list all the aircraft with time infomation
        /// </summary>
        public void ViewTimes()
        {
            foreach (var aircraft in CurrentContext.AirCraftData.GetEntities())
            {
                Console.WriteLine(aircraft.ToTimes());
            }
            DisplayWarning("flying times has been completely listed, press any key to continue");
            Console.ReadKey(intercept: true);
        }

        /// <summary>
        /// Manipulate the console interface to list all the customers and flights and to ask for input to add a customer into a flight service
        /// </summary>
        public void AddCustomer2Service()
        {
            
            var customers = CurrentContext.CustomerData.GetEntities().ToList();

            var selectedCustomer = ChooseFromSimpleList<Models.Customer>("Get the customer number", customers, "None of customers has been added");

            var flights = CurrentContext.AirCraftData.GetList();
           
            var selectedFlight = ChooseFromSimpleList<Models.AirCraft>("Get the flight number", flights, "None of flights has been added");

            selectedFlight.AddPassengers(selectedCustomer);
            DisplaySuccess($"Customer {selectedCustomer.FullName} added");
            DisplaySuccess($"Cost is {selectedFlight.Cost}");


        }

        /// <summary>
        /// Manipulate the console interface to list all the customers in specified flight
        /// </summary>
        public void ViewFlightPassengers()
        {
            var flights = CurrentContext.AirCraftData.GetList();

            var selectedFlight = ChooseFromSimpleList<Models.AirCraft>("Get the flight number", flights, "None of flights has been added");
            var customers = selectedFlight.Passengers;
            if(customers.Count==0)
            {
                Console.WriteLine("No customers");
            }
            else
            {
                DisplayList<Models.Customer>("", customers);
                DisplayWarning("Customers of this flight has been completely listed, press any key to continue");
                Console.ReadKey(intercept: true);
            }

        }

        /// <summary>
        /// Call the log out function in application context
        /// </summary>
        public void Logout()
        {
            string employeeName = CurrentContext.LoggedInEmployee.FullName;
            CurrentContext.LogoutEmployee();
            DisplaySuccess($"Employee {employeeName} has been successfully logged out");
        }

        #region Test data generation
        public void MakeFakeData()
        {
            string[] names = new string[] { "Tom", "Nick", "Jack", "Micheal", "Emma", "Steve", "Peter", "Tony", "Lang", "Eric", "Xavier" };
            string[] cities = new string[] { "Sydney", "Melbourne", "Brisbane", "Perth", "Cnaberra", "Wollongong", "Gold Coast", "Adelaide" };
            List<string> namelst = new List<string>();
            for(int i=0;i<5;i++)
            {
                
                while(namelst.Count==i)
                {
                    string name = names[new Random().Next(0, names.Length)];
                    if (!namelst.Contains(name))
                    {
                        namelst.Add(name);
                    }
                }
            }
            List<string> cityLst = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                
                while (cityLst.Count == i)
                {
                    string city = cities[new Random().Next(0, cities.Length)] + "-" + cities[new Random().Next(0, cities.Length)];
                    if (!cityLst.Contains(city))
                    {
                        cityLst.Add(city);
                    }
                }
            }
            foreach(var name in namelst)
            {
                var customer = CustomerDataProvider.NewEntity();
                customer.FullName = name;
                customer.Email = name + "@gmail.com";
                customer.Address = "fake address";
                customer.MobilePhone = "123";
                CurrentContext.CustomerData.AddEntity(customer);
            }
            foreach(var city in cityLst)
            {
                var aircraft = LightAirCraftDataProvider.NewEntity();
                aircraft.DeparturePlace = city.Split('-')[0];
                aircraft.ArrivalPlace = city.Split('-')[1];
                aircraft.DepartureTime = new Random().Next(0, 24).ToString() + ":00";
                aircraft.Distance = new Random().Next(8, 80) * 100;
                CurrentContext.AirCraftData.AddEntity(aircraft);
            }
            cityLst.Clear(); 
            for (int i = 0; i < 3; i++)
            {
               
                while (cityLst.Count == i)
                {
                    string city = cities[new Random().Next(0, cities.Length)] + "-" + cities[new Random().Next(0, cities.Length)];
                    if (!cityLst.Contains(city))
                    {
                        cityLst.Add(city);
                    }
                }
            }
            foreach (var city in cityLst)
            {
                var aircraft = HelicopterDataProvider.NewEntity();
                aircraft.DeparturePlace = city.Split('-')[0];
                aircraft.ArrivalPlace = city.Split('-')[1];
                aircraft.DepartureTime = new Random().Next(0, 24).ToString() + ":00";
                aircraft.Distance = new Random().Next(8, 80) * 100;
                CurrentContext.AirCraftData.AddEntity(aircraft);
            }
        }
        #endregion

    }
}
