using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FiniteStateMachine
{
    class Sigma<T>
    {
        public Dictionary<int, Dictionary<T, AState<T>>> changeToOtherStateTable { get; private set;}

        public Sigma(Dictionary<int, Dictionary<T, AState<T>>> changeToOtherStateTable)
        {
            this.changeToOtherStateTable = changeToOtherStateTable;
        }

        public Sigma()
        {
            this.changeToOtherStateTable = new Dictionary<int, Dictionary<T, AState<T>>>();
        }

        public AState<T> ChangeStateFunction(int stateID, T input)
        {
            if (ContainsKeys(stateID, input))
            {
                return changeToOtherStateTable[stateID][input];
            }else
            {
                throw new Exception("Your function is not listed in Function Table!");
            }
        }
        
        public Sigma<T> AddFunctionToTable(int stateID, Dictionary<T, AState<T>> dictionaryOfStates)
        {
            this.changeToOtherStateTable[stateID] = dictionaryOfStates;
            return this;
        }

        public Sigma<T> AddFunctionToTable(int stateId, T input, AState<T> destinationState)
        {
            if (ContainsKeys(stateId, input))
            {
                changeToOtherStateTable[stateId][input] = destinationState;
            }
            else
            {
                if (changeToOtherStateTable.ContainsKey(stateId))
                {
                    changeToOtherStateTable[stateId].Add(input, destinationState);
                }
                else
                {
                    changeToOtherStateTable[stateId] = new Dictionary<T, AState<T>>();
                    changeToOtherStateTable[stateId].Add(input, destinationState);
                }
            
            }
            return this;
        }

        public Sigma<T> AddFunctionToTable(string stateToState, T input, AState<T> destinationState)
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

        public Sigma<T> AddFunctionToTable(AState<T> fromState, AState<T> destinationState, T input)
        {
            // stateID<->stateID -- two way

            AddFunctionToTable(fromState.ID, input, destinationState);
            AddFunctionToTable(destinationState.ID, input, fromState);

            return this;
        }


        private bool ContainsKeys(int StateID, T input)
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
