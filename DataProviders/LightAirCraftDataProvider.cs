using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAB201_UserInterfaceTest.Models;

namespace CAB201_UserInterfaceTest.DataProviders
{
    /// <summary>
    /// Data provider of the helicopter, only to create the new entity of the light aircraft
    /// </summary>
    public class LightAirCraftDataProvider : BaseDataProvider<LightAirCraft>
    {
        protected override void checkDataUnique(LightAirCraft obj)
        {
            throw new NotImplementedException();
        }
    }
}
