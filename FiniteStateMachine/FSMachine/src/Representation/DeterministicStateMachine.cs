using FSMachine;
using FSMachine.State;
using FSMachine.TransitionTable;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FSMachine.Representation
{
    public abstract class DeterministicStateMachine<Input,State> 
        where State : struct, IState
    {
        //Save reference of the last state, entry point and moving by "State tree"
        protected State StartWith;
        private State LastState;
        protected HashSet<Input> Alphabet;
        protected HashSet<State> AllStates;
        protected HashSet<State> FiniteStates;

        private DeterministicTransitionTable<Input> Table;
        private bool IsTableBuilt = false;

        public DeterministicStateMachine(State StartWith, HashSet<Input> Alphabet, HashSet<State> AllStates, HashSet<State> FiniteStates)
        {
            this.StartWith = StartWith;
            this.LastState = StartWith;
            Table = new DeterministicTransitionTable<Input>();
            this.AllStates = AllStates;
            this.Alphabet = Alphabet;
            this.FiniteStates = FiniteStates;
            CreateTable();
        }

        private void CreateTable()
        {
            StateChanger<Input, State>[] TableBuilder = BuildTable();
            foreach (StateChanger<Input,State> changer in TableBuilder)
            {
                if (!Alphabet.Contains(changer.Input) && !AllStates.Contains(changer.From) && !AllStates.Contains(changer.To))
                    throw new Exception("Automaton can't recognize states or/and inputs");

                Table.AddFunctionToTable(changer.Input, changer.From, changer.To);
            }
            IsTableBuilt = true;
        }

        protected abstract StateChanger<Input,State>[] BuildTable();

        public IEnumerable GetEnumerator(Input[] Inputs)
        {
            if (!IsTableBuilt)
                throw new Exception("Table is not built, please before running machine, build your table first.");
            else
            {
                yield return StartWith;
                foreach(Input singleInput in Inputs)
                {
                    this.LastState = (State)Table.ChangeToState(singleInput, LastState);
                    if(!this.FiniteStates.Contains(this.LastState))
                        yield return this.LastState;
                }
            }
        }

    }
}
