﻿using System;
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

        public AState<T> ChangeStateFunction(int StateID, T input)
        {
            if (ContainsKeys(StateID, input))
            {
                return changeToOtherStateTable[StateID][input];
            }else
            {
                throw new Exception("Your function is not listed in Function Table!");
            }


        }
        public virtual void AddFunctionToTable(int StateID, Dictionary<T, AState<T>> dictionaryOfStates)
        {
            this.changeToOtherStateTable[StateID] = dictionaryOfStates;
        }

        public virtual void AddFunctionToTable(int StateID, T input, AState<T> state)
        {
            if (ContainsKeys(StateID, input))
            {
                changeToOtherStateTable[StateID][input] = state;
            }
            else
            {
                if (changeToOtherStateTable.ContainsKey(StateID))
                {
                    changeToOtherStateTable[StateID][input] = state;
                }
                else
                {
                    changeToOtherStateTable[StateID] = new Dictionary<T, AState<T>>();
                    changeToOtherStateTable[StateID][input] = state;
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