using CAB201_UserInterfaceTest.InterfaceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_UserInterfaceTest.Components.Menus
{
    /// <summary>
    /// Implement the menu item class
    /// </summary>
    public class CommonMenu : MenuItem
    {
        /// <summary>
        /// Constructor funtion
        /// </summary>
        /// <param name="text">the menu text</param>
        /// <param name="action">the action method will be wired when the menu item selected</param>
        public CommonMenu(string text,Action action) : base(text)
        {
            menuAction = action;
        }

    }

 
}
