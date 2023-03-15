using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_UserInterfaceTest.Models
{
    public class Helicopter:AirCraft
    {
        public Helicopter()
        {
            AirCraftType = "Helicopter";
            speed = 120;
            passengersLimit = 2;
            price = 600;

        }

        public Helicopter(Helicopter obj) : base(obj)
        {
            AirCraftType = "Helicopter";
            speed = 120;
            passengersLimit = 2;
            price = 600;
        }

        public override int additionalTimeCount()
        {
            if (Passengers.Count == 0)
                return 10;
            else if (Passengers.Count == 1)
                return 15;
            else if (Passengers.Count == 2)
                return 20;
            else
            {
                throw new Exception("Invalid Passengers Amount");
            }

        }
    }
}
