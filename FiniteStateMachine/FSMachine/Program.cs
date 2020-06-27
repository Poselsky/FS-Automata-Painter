using System;
using System.Collections.Generic;
using System.Linq;

namespace FSMachine
{
    class Program
    { 
        static void Main(string[] args)
        {
            CountingState start = new CountingState(0);

            CountingMachine machine = new CountingMachine(start, 
                new HashSet<int>() { 
                    -1,0,1
                },
                new HashSet<CountingState>() { 
                    new CountingState(-1),
                    new CountingState(0),
                    new CountingState(1)
                },
                new HashSet<CountingState>() { new CountingState(-1), }
            );

            foreach(CountingState s in machine.GetEnumerator(new int[] { 0, 1, 0, 1, 0, 1, 1, 1, -1 }))
            {
                Console.WriteLine(s);
            }


        }
    }
}
