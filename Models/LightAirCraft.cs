using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_UserInterfaceTest.Models
{
    public class LightAirCraft:AirCraft
    {
        public LightAirCraft()
        {
            AirCraftType = "Light Aircraft";
            speed = 800;
            passengersLimit = 6;
            price = 250;
        }

        public LightAirCraft(LightAirCraft obj) : base(obj)
        {
            AirCraftType = "Light Aircraft";
            speed = 800;
            passengersLimit = 6;
            price = 250;
        }

        public override int additionalTimeCount()
        {
            return 30;
        }
    }
}
