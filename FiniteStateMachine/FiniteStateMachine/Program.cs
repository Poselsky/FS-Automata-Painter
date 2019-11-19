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
            /*
            table[state.ID].Add(0, state);
            table[state.ID].Add(1, state2);

            table[state2.ID].Add(0, state);
            table[state2.ID].Add(1,final);

            table[final.ID].Add(0, state);
            table[final.ID].Add(1, null);
            */
            //TWO Different ways how to write change to other state table
            //Sigma<int> sigma = new Sigma<int>(table);
            
            Sigma<int> sigma = new Sigma<int>();
            
            sigma.AddFunctionToTable(state.ID, 0, state)
                .AddFunctionToTable(state.ID, 1, state2)
                .AddFunctionToTable(state2.ID, 0, state)
                .AddFunctionToTable(state2.ID, 1, final)
                .AddFunctionToTable(final.ID, 0, state)
                .AddFunctionToTable(final.ID, 1, null);
            



            List<int> inputs = new List<int>() { 0, 1, 1, 0, 1, 1, 1, 1, 1, 1};


            List<bool> coded = new List<bool>();

            Machine stateMachine = new Machine(allStates,inputs, sigma, state, new List<FinalState<int>>() { final } );
            int rand = 0;

            for(int i= 0; i< inputs.Count; i++)
            {
                stateMachine.DoAction(new Action<int>(el =>
                {
                    coded.Add(el != 0);
                }));
            }

            coded.ForEach(el => Console.WriteLine(el));

        }
    }

    public class Machine : AStateMachine<int>, IActionStateMachine<int>
    {
        private int innerIndexPointerOfInputs;

        public Machine(List<AState<int>> allStates, List<int> finiteInputSymbols, Sigma<int> changeStateFunction, AState<int> startState, List<FinalState<int>> finalStates)
            : base(allStates, finiteInputSymbols, changeStateFunction, startState, finalStates)
        {
            this.innerIndexPointerOfInputs = 0;
            //Output = 0;
        }

        public void DoAction(Action<int[]> functions)
        {
            throw new NotImplementedException();
        }

        public void DoAction(Action<int> functions)
        {
            List<int> finiteInputSymbols = (List<int>)orderedList[1];

            //Null means that it's the end of states
            if (innerIndexPointerOfInputs == finiteInputSymbols.Count - 1 || currentState == null)
            {
                return;
            }else
            {
                int symbol = finiteInputSymbols[innerIndexPointerOfInputs];
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
