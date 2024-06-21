using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator
{
    class Program
    {

        class Job
        {
            private int jobNumber;
            internal byte Priority { get; set; }
            internal  List<Passenger> Passengers  {get; set; }
            public byte LiftAllocation { get; private set; }
            internal List<int> pickUps = new List<int> { };
            internal List<int> dropOffs = new List<int> { };

            public Job(int jNumb, byte priority, List<Passenger>passengers, byte liftAllocation, int[] passPicks, int[] passDrops )
            {
                jobNumber = jNumb;
                Priority = priority;
                Passengers = passengers;
                LiftAllocation = liftAllocation;
                foreach (int p in passPicks) { pickUps.Add(p); }
                foreach (int d in passDrops) { dropOffs.Add(d); }
            }

        }

        class Passenger
        {
            internal string Name { get; private set; }
            private byte pickUp;
            private byte dropOff;
            internal string CurrentLocation { get; private set; }
 
            
            public  Passenger(string name, byte pUp, byte dOff, string level)
            {
                Name = name;
                pickUp = pUp;
                dropOff = dOff;
                CurrentLocation = level;
            }

        }
        static void Main(string[] args)
        {
        }
    }
}
