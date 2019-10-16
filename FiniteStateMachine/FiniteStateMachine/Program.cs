using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiniteStateMachine;

namespace MainProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicState<int> state = new BasicState<int>();
            BasicState<int> state2 = new BasicState<int>();
            FinalState<int> final = new FinalState<int>();

            List<AState<int>> allStates = new List<AState<int>>() { state, state2, final };

            Dictionary<int, Dictionary<int, AState<int>>> table = new Dictionary<int, Dictionary<int, AState<int>>>();

            /*
            Console.WriteLine(state.ID);
            Console.WriteLine(state2.ID);
            Console.WriteLine(final.ID);
            */

            allStates.ForEach(el =>
            {
                table.Add(el.ID,new Dictionary<int, AState<int>>());
            });

            table[state.ID].Add(0, state);
            table[state.ID].Add(1, state2);

            table[state2.ID].Add(0, state);
            table[state2.ID].Add(1,final);

            table[final.ID].Add(0, state);
            table[final.ID].Add(1, null);

            Sigma<int> sigma = new Sigma<int>(table);



            List<int> inputs = new List<int>() { 0, 1, 1, 1, 0, 0, 1, 0, 0, 0};


            int sum = 0;

            Machine stateMachine = new Machine(allStates,inputs, sigma, state, new List<FinalState<int>>() { final } );

            for(int i= 0; i< inputs.Count; i++)
            {
                stateMachine.DoAction(new Action<int>(el =>
                {
                    sum += el;
                }));
            }

            Console.WriteLine(sum);

        }
    }

    class Machine : AStateMachine<int>, IActionStateMachine<int>
    {
        private int innerIndexPointerOfInputs;
        private AState<int> currentState;
        //public int Output { get; private set; }


        public Machine(List<AState<int>> allStates, List<int> finiteInputSymbols, Sigma<int> changeStateFunction, AState<int> startState, List<FinalState<int>> finalStates)
            : base(allStates, finiteInputSymbols, changeStateFunction, startState, finalStates)
        {
            this.innerIndexPointerOfInputs = 0;

            currentState = startState;
            //Output = 0;
        }

        public void DoAction(Action<int[]> functions)
        {

        }

        public void DoAction(Action<int> functions)
        {
            //Null means that it's the end of states
            if (innerIndexPointerOfInputs == ((List<int>)orderedList[1]).Count - 1 || currentState == null)
            {
                return;
            }else
            {
                int symbol = ((List<int>)orderedList[1])[innerIndexPointerOfInputs];
                functions(symbol);
                //Output += symbol;
             
                Console.WriteLine(currentState.GetType());
                currentState = ((Sigma<int>)orderedList[2]).ChangeStateFunction(currentState.ID, symbol);
                innerIndexPointerOfInputs++;      
            }
        }

        public void DoAction(Action<List<int>> functions)
        {
            throw new NotImplementedException();
        }

    }
}
