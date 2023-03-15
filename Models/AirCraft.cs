using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_UserInterfaceTest.Models
{
    public class AirCraft
    {
        public string DeparturePlace { get; set; }
        public string ArrivalPlace { get; set; }
        public string DepartureTime { get; set; }
        public int Distance { get; set; }
        public string AirCraftType { get; protected set; } = "";
        /// <summary>
        /// The cost will be the flying hours multipling the price.
        /// </summary>
        public int Cost { get { return (int)((double)Distance / speed * price); } }
        protected int price { get; set; }
        protected int speed { get; set; } = 1;
        /// <summary>
        /// If the passengers count equals to the passengersLimit， it will be not allowed to add new passenger
        /// </summary>
        protected int passengersLimit {  get; set; } = 0;
        public List<Customer> Passengers { get; } = new List<Customer>();

        public AirCraft()
        {

        }
        public AirCraft(AirCraft obj)
        {
            DeparturePlace = obj.DeparturePlace;
            ArrivalPlace = obj.ArrivalPlace;
            DepartureTime = obj.DepartureTime;
            Distance = obj.Distance;
        }

        public virtual int additionalTimeCount()
        {
            return 30;
        }
       
        /// <summary>
        /// the function to add passenger
        /// </summary>
        /// <param name="passenger">the entity of passenger</param>
        public void AddPassengers(Customer passenger)
        {
            if (Passengers.Where(psg => psg.Email == passenger.Email).Any())
            {
                throw new InvalidOperationException("The passenger has already been added into this flight");
            } //duplicated passenger is not allowed

            if (Passengers.Count>=passengersLimit)
            {
                throw new InvalidOperationException("Flying machine is full");
            }//passengers larger than limit is not allowed
            Passengers.Add(passenger);
        }

        public override string ToString()
        {
            return $"{AirCraftType} from {DeparturePlace} to {ArrivalPlace}";
        }

        public string ToInfo()
        {
            return $"{AirCraftType} {DeparturePlace} {ArrivalPlace} {Distance}";
        }

        public string ToTimes()
        {
            return $"{AirCraftType} {DeparturePlace} {ArrivalPlace} {DepartureTime} {(int)(((double)Distance / speed) * 60) + additionalTimeCount()} minutes";
        }
    }
}
