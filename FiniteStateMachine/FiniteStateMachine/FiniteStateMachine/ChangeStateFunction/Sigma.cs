using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine
{
    class Sigma<T>
    {
        public Dictionary<int, Dictionary<T, AState<T>>> changeToOtherStateTable { get; private set;}

        public Sigma(Dictionary<int, Dictionary<T, AState<T>>> changeToOtherStateTable)
        {
            this.changeToOtherStateTable = changeToOtherStateTable;
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
        
        public virtual void AddFunctionToTable(int stateID, Dictionary<T, AState<T>> dictionaryOfStates)
        {
            this.changeToOtherStateTable[stateID] = dictionaryOfStates;
        }

        public virtual void AddFunctionToTable(int stateId, T input, AState<T> state)
        {
            if (ContainsKeys(stateId, input))
            {
                changeToOtherStateTable[stateId][input] = state;
            }
            else
            {
                if (changeToOtherStateTable.ContainsKey(stateId))
                {
                    changeToOtherStateTable[stateId].Add(input, state);
                }
                else
                {
                    changeToOtherStateTable[stateId] = new Dictionary<T, AState<T>>();
                    changeToOtherStateTable[stateId].Add(input, state);
                }
            
            }
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
