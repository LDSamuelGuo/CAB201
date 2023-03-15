using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAB201_UserInterfaceTest.Models;

namespace CAB201_UserInterfaceTest.DataProviders
{
    /// <summary>
    /// Data provider of the aircraft
    /// </summary>
    public class AirCraftDataProvider : BaseDataProvider<AirCraft>
    {
        public AirCraftDataProvider(List<AirCraft> data) : base(data)
        {

        }

        public AirCraft AirCraft
        {
            get => default;
            set
            {
            }
        }

        protected override void checkDataUnique(AirCraft obj)
        {
            if (GetEntities().Where(clt => (clt.AirCraftType == obj.AirCraftType
            &&clt.DeparturePlace == obj.DeparturePlace
            && clt.ArrivalPlace == obj.ArrivalPlace
            && clt.DepartureTime == obj.DepartureTime
            )).Any())
            {
                throw new InvalidOperationException("An aircraft with the same information already exists");
            }
        }
    }
}
