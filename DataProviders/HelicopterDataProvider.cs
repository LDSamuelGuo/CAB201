using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAB201_UserInterfaceTest.Models;

namespace CAB201_UserInterfaceTest.DataProviders
{
    /// <summary>
    /// Data provider of the helicopter, only to create the new entity of the helicopter
    /// </summary>
    public class HelicopterDataProvider : BaseDataProvider<Helicopter>
    {
        protected override void checkDataUnique(Helicopter obj)
        {
            throw new NotImplementedException();
        }
    }
}
