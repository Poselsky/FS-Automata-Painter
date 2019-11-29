using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FiniteStateMachine
{
    public sealed class Sigma<Tin, Tout>
    {
        public Dictionary<int, Dictionary<Tin, AState<Tout>>> changeToOtherStateTable { get; private set;}

        public Sigma(Dictionary<int, Dictionary<Tin, AState<Tout>>> changeToOtherStateTable)
        {
            this.changeToOtherStateTable = changeToOtherStateTable;
        }

        public Sigma()
        {
            this.changeToOtherStateTable = new Dictionary<int, Dictionary<Tin, AState<Tout>>>();
        }

        public AState<Tout> ChangeStateFunction(AState<Tout> state, Tin input)
        {
            if (ContainsKeys(state.ID, input))
            {
                return changeToOtherStateTable[state.ID][input];
            }else
            {
                throw new Exception("Your function is not listed in Function Table!");
            }
        }

        public AState<Tout> this[AState<Tout> state, Tin input]
        {
            get => ChangeStateFunction(state, input);
        }
        
        public Sigma<Tin, Tout> AddFunctionToTable(int stateID, Dictionary<Tin, AState<Tout>> dictionaryOfStates)
        {
            this.changeToOtherStateTable[stateID] = dictionaryOfStates;
            return this;
        }

        public Sigma<Tin, Tout> AddFunctionToTable(int stateID, Tin input, AState<Tout> destinationState)
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
                    changeToOtherStateTable[stateID] = new Dictionary<Tin, AState<Tout>>();
                    changeToOtherStateTable[stateID].Add(input, destinationState);
                }
            
            }
            return this;
        }
        public Sigma<Tin, Tout> AddFunctionToTable(AState<Tout> startState, Tin input, AState<Tout> destinationState)
        {
            return AddFunctionToTable(startState.ID, input, destinationState);
        }


        public Sigma<Tin, Tout> AddFunctionToTable(string stateToState, Tin input, AState<Tout> destinationState)
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

        public Sigma<Tin, Tout> AddTwoWayFunctionToTable(AState<Tout> stateA, Tin input, AState<Tout> stateB)
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


        private bool ContainsKeys(int StateID, Tin input)
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
