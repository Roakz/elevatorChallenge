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
            virtual public int Index { get; }
            abstract public void PerformAction();
            abstract public void PrintItem();
        }

        class UpMenuItem : MenuItem
        {
            public override int Index { get; }
            private readonly string name;
            public UpMenuItem(int i)
            {
                Index = i;
                name = "Up";
            }
            public override void PerformAction()
            {
                Console.WriteLine("Ahoy from Up");
            }
            
            public override void PrintItem()
            {
                Console.WriteLine($"{Index}. {name}");
            }
        }

        class DownMenuItem : MenuItem
        {
            public override int Index {get;}           
            private readonly string name;

            public DownMenuItem(int i)
            {
                Index = i;
                name = "Down";
            }

            public override void PerformAction()
            {
                Console.WriteLine("Ahoy from Down");
            }     

            public override void PrintItem()
            {
                Console.WriteLine($"{Index}. {name}");
            }
        }
        class InstructionMenuItem : MenuItem
        {
            public override int Index { get; }
            private readonly string name;

            public InstructionMenuItem(int i)
            {
                Index = i;
                name = "Instructions";
            }
            public override void PerformAction() 
            {
                PrintInstructions();
            }

            public override void PrintItem()
            {
                Console.WriteLine($"{Index}. {name}");
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
            public override int Index { get; }
            private readonly string name;

            public ExitMenuItem(int i)
            {
                Index = i;
                name = "Exit";
            }

            public override void PerformAction() { }

            public override void PrintItem()
            {
                Console.WriteLine($"{Index}. {name}");
            }
        }

        class Menu
        {
            private List<MenuItem> menuItems;

            public Menu(List<MenuItem> mItems)
            {
                List<MenuItem> sortedList = new List<MenuItem> { };
                IEnumerable<MenuItem> menuItemQuery =
                    from item in mItems
                    orderby item.Index
                    select item;
                foreach (MenuItem i in menuItemQuery)
                {
                    sortedList.Add(i);
                    Console.WriteLine(i.Index);
                }
                menuItems = sortedList;
            }

            public void PrintMenu()
            {
                Console.WriteLine("Please make a selection. (1, 2 or 3)");
                foreach(MenuItem item in menuItems)
                {
                    item.PrintItem();
                }
            }

            public void CallMenuItem(int Index)
            {
                menuItems[Index - 1].PerformAction(); 
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
            List<MenuItem> menuItemList = new List<MenuItem> { new InstructionMenuItem(3), new DownMenuItem(2), new UpMenuItem(1), new ExitMenuItem(4)};            
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
