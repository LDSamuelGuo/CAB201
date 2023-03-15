using System;
using System.Collections.Generic;

namespace CAB201_UserInterfaceTest.InterfaceManagement
{
    /// <summary>
    /// Static support methods for a simple interactive menu syste,
    /// </summary>
    public class UserInterface
    {
        /// <summary>
        /// Contains the menu of this interface
        /// </summary>
        protected List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
        /// <summary>
        /// Text will be displayed when entering the interface 
        /// </summary>
        protected string WelcomeText { get; set; } = "";
        /// <summary>
        /// Contains the data and the status of this application
        /// </summary>
        protected ApplicationContext CurrentContext { get; set; }

        public MenuItem MenuItem
        {
            get => default;
            set
            {
            }
        }

        public ApplicationContext ApplicationContext
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// Constructor function
        /// </summary>
        /// <param name="current">The application context</param>
        public UserInterface(ApplicationContext current)
        {
            CurrentContext = current;
        }

        /// <summary>
        /// Displays the menu and waiting for selecting, when the menu item has been selected the action of the menu item will be wired.
        /// </summary>
        public void DisplayMenu()
        {
            try
            {
                if(WelcomeText!="")
                {
                    DisplayGreeting(WelcomeText);
                    WelcomeText = "";
                }
                var selectedItem = UserInterface.ChooseFromList<MenuItem>("Please select one of the following:", MenuItems);
                selectedItem.WireAction();
            }
            catch(Exception e)
            {
                DisplayError(e.Message);
            }
        }

        /// <summary>
        /// Choose an item from a list by simply displayed.
        /// </summary>
        /// <typeparam name="T">The kind of items stored in the list.</typeparam>
        /// <param name="prompt">The label displayed to request input.</param>
        /// <param name="itemList">A list of items which will be displayed.</param>
        /// <param name="emptyPrompt">The label displayed when list is empty.</param>
        /// <returns>The selected item.</returns>
        public static T ChooseFromSimpleList<T>(string prompt, IList<T> itemList, string emptyPrompt = "The specified list is empty")
        {
            //System.Diagnostics.Debug.Assert( itemList.Count > 0 );
            if (itemList.Count == 0)
            {
                throw new Exception(emptyPrompt);
            }
            DisplaySimpleList(itemList);
            var option = UserInterface.GetOption(prompt, 1, itemList.Count);
            return itemList[option];
        }




        /// <summary>
        /// Choose an item from a list.
        /// </summary>
        /// <typeparam name="T">The kind of items stored in the list.</typeparam>
        /// <param name="title">The text to display at the start of the list.</param>
        /// <param name="prompt">The label displayed to request input.</param>
        /// <param name="itemList">A list of items which will be displayed.</param>
        /// <param name="emptyPrompt">The label displayed when list is empty.</param>
        /// <returns>The selected item.</returns>
        public static T ChooseFromList<T>( string title,string prompt, IList<T> itemList,string emptyPrompt="The specified list is empty")
        {
            //System.Diagnostics.Debug.Assert( itemList.Count > 0 );
            if(itemList.Count==0)
            {
                throw new Exception(emptyPrompt);
            }
            DisplayList( title, itemList );
            var option = UserInterface.GetOption(prompt, 1, itemList.Count);
            return itemList[option];
        }

        /// <summary>
        /// Choose an item from a list.
        /// </summary>
        /// <typeparam name="T">The kind of items stored in the list.</typeparam>
        /// <param name="title">The text to display at the start of the list.</param>
        /// <param name="itemList">A list of items which will be displayed.</param>
        /// <returns>The selected item.</returns>
        public static T ChooseFromList<T>(string title, IList<T> itemList)
        {
            System.Diagnostics.Debug.Assert(itemList.Count > 0);
            DisplayList(title, itemList);
            var option = UserInterface.GetOption(1, itemList.Count);
            return itemList[option];
        }

        /// <summary>
        /// Display a list of items in simple forms.
        /// </summary>
        /// <typeparam name="T">The kind of items stored in the list.</typeparam>
        /// <param name="list">A list of items which will be displayed.</param>
        public static void DisplaySimpleList<T>(IList<T> list)
        {

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine("{0} {1}", i + 1, list[i].ToString());
            }
        }


        /// <summary>
        /// Display a list of items.
        /// </summary>
        /// <typeparam name="T">The kind of items stored in the list.</typeparam>
        /// <param name="title">The text to display at the start of the list.</param>
        /// <param name="list">A list of items which will be displayed.</param>
        public static void DisplayList<T>( string title, IList<T> list )
        {
            Console.WriteLine(title);
            
            if ( list.Count == 0 )
            {
                Console.WriteLine( "  None" );
            }
            else
            {
                for ( int i = 0; i < list.Count; i++ )
                {
                    Console.WriteLine( "  {0}) {1}", i + 1, list[i].ToString() );
                }
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Prompts the user for an integer between lower and upper bounds, inclusive.
        /// </summary>
        /// <param name="min">The lower bound.</param>
        /// <param name="max">The upper bound.</param>
        /// <returns>Returns ('value entered by user' - 1).</returns>
        public static int GetOption( int min, int max )
        {
            if (max >= 10)
            {
                while (true)
                {
                    var key = GetInput($"Please enter an option between {min} and {max}");

                    if (int.TryParse(key, out var option))
                    {
                        if (min <= option && option <= max)
                            return option - 1;
                    }

                    UserInterface.Error("Invalid option");
                }
            }
            else
            {
                while (true)
                {
                    var key = Console.ReadKey(intercept: true);

                    if (int.TryParse(key.KeyChar.ToString(), out var option))
                    {
                        if (min <= option && option <= max)
                            return option - 1;
                    }

                    UserInterface.Error("Invalid option");
                }
            }
        }

        /// <summary>
        /// Prompts the user for an integer between lower and upper bounds, inclusive.
        /// </summary>
        /// <param name="prompt">The label displayed to request input.</param>
        /// <param name="min">The lower bound.</param>
        /// <param name="max">The upper bound.</param>
        /// <returns>Returns ('value entered by user' - 1).</returns>
        public static int GetOption(string prompt,int min, int max)
        {
            while (true)
            {
                var key = GetInput($"{prompt},the number should between {min} and {max}");

                if (int.TryParse(key, out var option))
                {
                    if (min <= option && option <= max)
                        return option - 1;
                }

                UserInterface.Error("Invalid option");
            }
        }


        /// <summary>
        /// Displays a menu, with the options numbered from 1 to options.Length,
        /// the gets a validated integer in the range 1..options.Length. 
        /// Subtracts 1, then returns the result. If the supplied list of options 
        /// is empty, returns an error value (-1).
        /// </summary>
        /// <param name="title">A heading to display before the menu is listed.</param>
        /// <param name="options">The list of objects to be displayed.</param>
        /// <returns>Return value is either -1 (if no options are provided) or a 
        /// value in 0 .. (options.Length-1).</returns>
        public static int GetOption(string title, params object[] options)
        {
            if (options.Length == 0)
            {
                return -1;
            }

            int digitsNeeded = (int)(1 + Math.Floor(Math.Log10(options.Length)));

            Console.WriteLine(title);

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{(i + 1).ToString().PadLeft(digitsNeeded)} {options[i]}");
            }

            int option = GetInteger($"Please enter a choice between 1 and {options.Length}", 1, options.Length);

            return option - 1;
        }


        /// <summary>
        /// Prompts user to enter a line of text.
        /// </summary>
        /// <param name="prompt">The label displayed to request input.</param>
        /// <returns>The next line of input from the standard input stream.</returns>
        public static string GetInput( string prompt )
        {
            Console.Write( "{0}: ", prompt );
            return Console.ReadLine();
        }


        /// <summary>
        /// Prompts user to get an integer value, blocking until valid input is obtained.
        /// </summary>
        /// <param name="prompt">The label displayed to request input.</param>
        /// <returns>The next line of input from the standard input stream, parsed into an int.</returns>
        public static int GetInteger( string prompt )
        {
            while ( true )
            {
                var response = UserInterface.GetInput(prompt);

                if ( int.TryParse( response, out int intVal ) )
                {
                    return intVal;
                }
                else
                {
                    DisplayInlineError( "Invalid number" );
                }
            }
        }

        /// <summary>
        /// Prompts user to get time string(e.g 10:00) , blocking until valid input is obtained.
        /// </summary>
        /// <param name="prompt">The label displayed to request input.</param>
        /// <returns>The next line of input from the standard input stream, parsed into an int.</returns>
        public static string GetTime(string prompt)
        {
            while (true)
            {
                var response = UserInterface.GetInput(prompt);

                if (DateTime.TryParse($"2022-1-1 {response}:00", out DateTime dateVal))
                {
                    return dateVal.ToString("HH:mm");
                }
                else
                {
                    DisplayInlineError("Invalid time");
                }
            }
        }


        /// <summary>
        /// Gets a validated integer between the designated lower and upper bounds.
        /// </summary>
        /// <param name="prompt">Text used to ask the user for input.</param>
        /// <param name="min">The lower bound.</param>
        /// <param name="max">The upper bound.</param>
        /// <returns>A value x such that lowerBound <= x <= upperBound.</returns>
        public static int GetInteger(string prompt, int min, int max)
        {
            if (min > max)
            {
                int t = min;
                min = max;
                max = t;
            }

            while (true)
            {
                int result = GetInteger(prompt);

                if (min <= result && result <= max)
                {
                    return result;
                }
                else
                {
                    DisplayInlineError("Supplied value is out of range");
                }
            }
        }

        /// <summary>
        /// Gets a validated Boolean value.
        /// </summary>
        /// <param name="prompt">Text used to ask the user for input.</param>
        /// <returns>A Boolean value supplied by the user.</returns>
        public static bool GetBoolean(string prompt)
        {
            while (true)
            {
                string response = GetInput(prompt);

                bool result;

                if (bool.TryParse(response, out result))
                {
                    return result;
                }
                else
                {
                    DisplayInlineError("Supplied value is not a boolean");
                }
            }
        }


        /// <summary>
        /// Gets a validated floating point value.
        /// </summary>
        /// <param name="prompt">Text used to ask the user for input.</param>
        /// <returns>A floating point value supplied by the user.</returns>
        public static double GetDouble(string prompt)
        {
            while (true)
            {
                string response = GetInput(prompt);

                double result;

                if (double.TryParse(response, out result))
                {
                    return result;
                }
                else
                {
                    DisplayInlineError("Supplied value is not numeric");
                }
            }
        }


        /// <summary>
        /// Gets a validated floating point between the designated lower and upper bounds.
        /// </summary>
        /// <param name="prompt">Text used to ask the user for input.</param>
        /// <param name="min">The lower bound.</param>
        /// <param name="max">The upper bound.</param>
        /// <returns>A value x such that lowerBound <= x <= upperBound.</returns>
        public static double GetDouble(string prompt, double min, double max)
        {
            if (min > max)
            {
                double t = min;
                min = max;
                max = t;
            }

            while (true)
            {
                double result = GetDouble(prompt);

                if (min <= result && result <= max)
                {
                    return result;
                }
                else
                {
                    DisplayInlineError("Supplied value is out of range");
                }
            }
        }



        /// <summary>
        /// Prompts user to get password.
        /// </summary>
        /// <param name="prompt">The label displayed to request input.</param>
        /// <returns>The next line of input from the standard input stream.</returns>
        public static string GetPassword( string prompt )
        {
            Console.Write( "{0}: ", prompt );
            var password = new System.Text.StringBuilder();
            while ( true )
            {
                var keyInfo = Console.ReadKey(intercept: true);
                var key = keyInfo.Key;

                if ( key == ConsoleKey.Enter )
                {
                    break;
                }
                else if ( key == ConsoleKey.Backspace )
                {
                    if ( password.Length > 0 )
                    {
                        Console.Write( "\b \b" );
                        password.Remove( password.Length - 1, 1 );
                    }
                }
                else
                {
                    Console.Write( "*" );
                    password.Append( keyInfo.KeyChar );
                }
            }

            Console.WriteLine();
            return password.ToString();
        }

        /// <summary>
        /// Displays a message followed by the text ", please try again".
        /// </summary>
        /// <param name="msg">The message to display.</param>
        public static void Error( string msg )
        {
            Console.WriteLine( $"{msg}, please try again" );
            Console.WriteLine();
        }

        /// <summary>
        /// Displays a message.
        /// </summary>
        /// <param name="msg">The message to display.</param>
        public static void Message( object msg )
        {
            Console.WriteLine( msg );
            Console.WriteLine();
        }

        /// <summary>
        /// Displays a generic success message to the console in green with padding below
        /// </summary>
        /// <param name="msg">The message to display to the console</param>
        public static void DisplaySuccess(string msg)
        {
            ConsoleColor previousConsoleColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{msg}");
            Console.ForegroundColor = previousConsoleColor;
            Console.WriteLine();
        }

        /// <summary>
        /// Displays a generic warning message to the console in yellow/orange with padding below
        /// </summary>
        /// <param name="msg">The message to display to the console</param>
        /// <param name="noNextLine">Indicate whether or not there will be direct content below the warning</param>
        public static void DisplayWarning(string msg, bool nextLine = true)
        {
            ConsoleColor previousConsoleColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{msg}");
            Console.ForegroundColor = previousConsoleColor;
            if (nextLine) Console.WriteLine();
        }

        /// <summary>
        /// Displays a generic error message to the console in red with padding below
        /// </summary>
        /// <param name="msg">The message to display to the console</param>
        /// <param name="trailingTryAgain">To determine if the error a plausable cause and can be retried</param>
        public static void DisplayError(string msg, bool trailingTryAgain = false)
        {
            ConsoleColor previousConsoleColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{msg}" + (trailingTryAgain ? ", try again." : "."));
            Console.ForegroundColor = previousConsoleColor;
            Console.WriteLine();
        }

        /// <summary>
        /// Displays a generic error message to the console in red on the same line the error has occured in
        /// Used primarily in asking for input
        /// </summary>
        /// <param name="msg">The message to display to the console</param>
        /// <param name="trailingTryAgain">To determine if the error a plausable cause and can be retried</param>
        public static void DisplayInlineError(string msg, bool trailingTryAgain = false)
        {
            ConsoleColor previousConsoleColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("".PadLeft(3) + $" | {msg}" + (trailingTryAgain ? ", try again." : "."));
            Console.ForegroundColor = previousConsoleColor;
        }

        /// <summary>
        /// Display a welcome greeting of the application to the user (Technical interlude)
        /// </summary>
        public static void DisplayGreeting(string greetings)
        {
            ConsoleColor prevConsoleFg = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("====================================");
            Console.WriteLine(greetings);  
            Console.WriteLine("====================================");
            Console.ForegroundColor = prevConsoleFg;
        }

        

        /// <summary>
        /// Displays the final fairwell message to the user before the applications finally exits
        /// </summary>
        public static void DisplayFarewell()
        {
            ConsoleColor prevConsoleFg = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Program exited, press any key to continue");         
            Console.ForegroundColor = prevConsoleFg;
            Console.ReadKey(intercept: true);
        }

    }

    /// <summary>
    /// Class which represents a menu item. Use subclass to represent an actual
    /// menu item.
    /// </summary>
    public abstract class MenuItem
    {
        /// <summary>
        /// The text to be displayed in the menu item.
        /// </summary>
        public string Text { get; protected set; }

        /// <summary>
        /// Initialise a new MenuItem,
        /// </summary>
        /// <param name="text">The text to be displayed.</param>
        public MenuItem( string text )
        {
            Text = text;
        }
        /// <summary>
        /// delegate the function that will be wired when the menu item has been selected
        /// </summary>
        protected Action menuAction { get; set; }

        /// <summary>
        /// Wires the menu action.
        /// </summary>
        public void WireAction()
        {
            if (menuAction != null)
                menuAction();
        }

        /// <summary>
        /// Get the text of the menu item for display.
        /// </summary>
        /// <returns>The text of the menu item for display.</returns>
        public override string ToString()
        {
            return Text;
        }

       
    }
}
