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

        class Elevator
        {
            //Expressed as seconds per floor
            internal const int travelSpeed = 15;
            //Seconds taken
            internal const int doorOpeningSpeed = 10;
            internal string currentLocation;
            internal List<Job> jobList;
            internal List<string> passengersOnboard = new List<string> { };
            internal byte PassengerCount;
            internal int currentJob;
            
        }

        abstract class MenuItem
        {
            int index;
            string name;
            abstract public void PerformAction();
            virtual public void PrintItem()
            {
                Console.WriteLine($"{index}. {name}");
            }
        }

        class UpMenuItem : MenuItem
        {
            public int index;
            public string name = "Up";

            public UpMenuItem(int i)
            {
                index = i;                
            }
            public override void PerformAction()
            {
                Console.WriteLine("Ahoy from Up");
            }
            
            public override void PrintItem()
            {
                Console.WriteLine($"{index}. {name}");
            }
        }

        class DownMenuItem : MenuItem
        {

            protected int index;
            public string name = "Down";

            public DownMenuItem(int i)
            {
                index = i;              
            }

            public override void PerformAction()
            {
                Console.WriteLine("Ahoy from Down");
            }     

            public override void PrintItem()
            {
                Console.WriteLine($"{index}. {name}");
            }
        }
        class InstructionMenuItem : MenuItem
        {

            public int index;
            public string name = "Instructions";

            public InstructionMenuItem(int i)
            {
                index = i;
            }
            public override void PerformAction() 
            {
                PrintInstructions();
            }

            public override void PrintItem()
            {
                Console.WriteLine($"{index}. {name}");
            }

            public void PrintInstructions()
            {
                Console.WriteLine("How to Play,\n1. Select up or down from the selection menu.\n2.Enter a passenger name, current level and desired level when prompted." +
                    "\n3.Watch for updates as your passenger travels on the elevator.\n4. Keep adding passengers. As many as you can!.\nPRESS ANY BUTTON TO CONTINUE.");
                Console.Read();
            }
        }

        class ExitMenuItem : MenuItem
        {
            int index;
            string name = "Exit";

            public ExitMenuItem(int i)
            {
                index = i;
            }

            public override void PerformAction() { }

            public override void PrintItem()
            {
                Console.WriteLine($"{index}. {name}");
            }
        }

        class Menu
        {
            private List<MenuItem> menuItems;

            public Menu(List<MenuItem> mItems)
            {
                menuItems = mItems;
            }

            public void PrintMenu()
            {
                Console.WriteLine("Please make a selection. (1, 2 or 3)");
                foreach(MenuItem item in menuItems)
                {
                    item.PrintItem();
                }
            }

            public void CallMenuItem(int index)
            {
                menuItems[index - 1].PerformAction(); 
            }

            public bool selectionHandler()
            {
                int selection = 0;         

                while (selection == 0)
                {
                    try
                    {
                        int possibleSelection = Convert.ToInt32(Console.ReadLine());
                        if (possibleSelection > 4 || possibleSelection < 1) { throw new ArgumentException(); }
                        else
                        {                            
                            selection = possibleSelection;                            
                        }                        
                    }
                    catch
                    {
                        Console.Clear();
                        Console.WriteLine("only numbers 1, 2 or 3 followed by enter.");
                        PrintMenu();
                    }
                }

                if (selection == 4) { return false; }
                CallMenuItem(selection);       
                return true;
            }
        }    

        static void Main(string[] args)
        {
            List<MenuItem> menuItemList = new List<MenuItem> { new UpMenuItem(1), new DownMenuItem(2), new InstructionMenuItem(3), new ExitMenuItem(4)};            
            Menu menu = new Menu(menuItemList);
            bool play;
            do
            {                
                menu.PrintMenu();                
                play = menu.selectionHandler();
            } while (play);            
        }
    }
}
