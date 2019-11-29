using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FiniteStateMachine
{
    public sealed class Sigma<T,V>
    {
        public Dictionary<int, Dictionary<V, AState<T>>> changeToOtherStateTable { get; private set;}

        public Sigma(Dictionary<int, Dictionary<V, AState<T>>> changeToOtherStateTable)
        {
            this.changeToOtherStateTable = changeToOtherStateTable;
        }

        public Sigma()
        {
            this.changeToOtherStateTable = new Dictionary<int, Dictionary<V, AState<T>>>();
        }

        public AState<T> ChangeStateFunction(AState<T> state, V input)
        {
            if (ContainsKeys(state.ID, input))
            {
                return changeToOtherStateTable[state.ID][input];
            }else
            {
                throw new Exception("Your function is not listed in Function Table!");
            }
        }

        public AState<T> this[AState<T> state, V input]
        {
            get => ChangeStateFunction(state, input);
        }
        
        public Sigma<T,V> AddFunctionToTable(int stateID, Dictionary<V, AState<T>> dictionaryOfStates)
        {
            this.changeToOtherStateTable[stateID] = dictionaryOfStates;
            return this;
        }

        public Sigma<T,V> AddFunctionToTable(int stateID, V input, AState<T> destinationState)
        {
            if (ContainsKeys(stateID, input))
            {
                changeToOtherStateTable[stateID][input] = destinationState;
            }
            else
            {
                if (changeToOtherStateTable.ContainsKey(stateID))
                {
                    changeToOtherStateTable[stateID].Add(input, destinationState);
                }
                else
                {
                    changeToOtherStateTable[stateID] = new Dictionary<V, AState<T>>();
                    changeToOtherStateTable[stateID].Add(input, destinationState);
                }
            
            }
            return this;
        }
        public Sigma<T,V> AddFunctionToTable(AState<T> startState, V input, AState<T> destinationState)
        {
            return AddFunctionToTable(startState.ID, input, destinationState);
        }


        public Sigma<T,V> AddFunctionToTable(string stateToState, V input, AState<T> destinationState)
        {
            // stateID->stateID

            //Remove white spaces then split by "->" symbol, then convert to integers
            int[] statesIDS = Array.ConvertAll(Regex.Split(stateToState.Replace(" ", ""), "->"), id => int.Parse(id));
            if(statesIDS.Length != 2)
            {
                throw new ArgumentException("Two states not provided");
            }

            if(statesIDS[1] != destinationState.ID){
                throw new ArgumentException("Provided wrong destination state");
            }

            AddFunctionToTable(stateToState[0], input, destinationState);

            return this;
        }

        public Sigma<T,V> AddTwoWayFunctionToTable(AState<T> stateA, V input, AState<T> stateB)
        {
            // stateID<->stateID -- two way
            if(stateA == stateB)
            {
                throw new ArgumentException("Objects provided in parameter are identical. Please choose two seperate objects for two way binding.");
            }

            AddFunctionToTable(stateA.ID, input, stateB);
            AddFunctionToTable(stateB.ID, input, stateA);

            return this;
        }


        private bool ContainsKeys(int StateID, V input)
        {
            if (changeToOtherStateTable.ContainsKey(StateID))
            {
                if (changeToOtherStateTable[StateID].ContainsKey(input))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
