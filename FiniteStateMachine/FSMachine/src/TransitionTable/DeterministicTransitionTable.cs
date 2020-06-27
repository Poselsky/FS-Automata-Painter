using System;
using System.Collections.Generic;
using FSMachine.State;

namespace FSMachine.TransitionTable
{
    /// <summary>
    /// Start object by building it with adding functions to table and using it with ChangeToState func
    /// </summary>
    /// <typeparam name="Input"></typeparam>
    public class DeterministicTransitionTable<Input>
    {
        //Weird representation, but imagine you represent columns and later get specific state on previous state
        /* 
         *  state/input | 0  | 1
         *  S0          | S0 | S1 
         *  S1          | S2 | S0
         *  S2          | S1 | S2
         */
        // You get specific column first f.e TransitionTableRepresentation[0] you get key value pairs 
        // {[S0 => S0], [S1 => S2], [S2 => S1]}
        // Then you can get your desired state
        Dictionary<Input, Dictionary<IState, IState>> TransitionTableRepresentation;

        public DeterministicTransitionTable()
        {
            TransitionTableRepresentation = new Dictionary<Input, Dictionary<IState, IState>>();
        }

        /// <summary>
        /// Create a table by adding input and states
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <returns></returns>
        public DeterministicTransitionTable<Input> AddFunctionToTable(Input Input, IState From, IState To)
        {
            if (!KeyValueOnInputExists(Input))
                TransitionTableRepresentation[Input] = new Dictionary<IState, IState>() { { From, To } };
            else
            {
                if (!KeyValueOnStateExists(Input, From))
                    TransitionTableRepresentation[Input][From] = To;
                else
                    throw new Exception("Overriding states is not allowed");
            }
              
            return this;
        }

        /// <summary>
        /// Change to state based on previous state
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public IState ChangeToState(Input Input, IState LastState)
        {
            if (KeyValueOnInputExists(Input) && KeyValueOnStateExists(Input, LastState))
            {
                return TransitionTableRepresentation[Input][LastState];
            }
            else
                throw new Exception("Input or state not recognized");
        }

        private bool KeyValueOnInputExists(Input Input)
        {
            return TransitionTableRepresentation.ContainsKey(Input);
        }

        private bool KeyValueOnStateExists(Input Input, IState State)
        {
            return TransitionTableRepresentation[Input].ContainsKey(State);
        }
        /// <summary>
        /// Same as change to state
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public IState this[Input Input, IState LastState]
        {
            get => ChangeToState(Input, LastState);
        }
    }
}
