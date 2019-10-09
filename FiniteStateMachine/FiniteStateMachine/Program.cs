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
            Console.WriteLine(state.ID);
            FinalState<int> final = new FinalState<int>();

            List<IState<int>> allStates = new List<IState<int>>() { state, state2, final };

            Dictionary<int, Dictionary<int, IState<int>>> table = new Dictionary<int, Dictionary<int, IState<int>>>();


            allStates.ForEach(el =>
            {
                table[el.ID] = new Dictionary<int, IState<int>>();
            });

            table[state.ID][0] = state;
            table[state.ID][1] = state2;

            table[state2.ID][0] = state;
            table[state2.ID][1] = final;

            table[state.ID][0] = state;
            table[state.ID][1] = null;

            Sigma<int> sigma = new Sigma<int>(table);


            List<int> inputs = new List<int>() { 0, 0, 0, 0, 0, 1, 0, 1, 1, 1};



            AStateMachine<int> stateMachine = new Machine(allStates,inputs, sigma, state, new List<IFinalState<int>>() { final } );

        }
    }

    class Machine: AStateMachine<int>, IActionStateMachine<int>
    {
        public Machine(List<IState<int>> allStates, List<int> finiteInputSymbols, Sigma<int> changeStateFunction, IState<int> startState, List<IFinalState<int>> finalStates)
            :base(allStates, finiteInputSymbols, changeStateFunction, startState, finalStates)
        {

        }

        public void DoAction(Action<int[]> functions)
        {
            throw new NotImplementedException();
        }

        public void DoAction(Action<int> functions)
        {
            throw new NotImplementedException();
        }

        public void DoAction(Action<List<int>> functions)
        {
            throw new NotImplementedException();
        }

    }
}
