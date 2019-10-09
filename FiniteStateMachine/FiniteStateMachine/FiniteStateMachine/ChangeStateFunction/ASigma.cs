using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine
{
    abstract class ASigma<T>
    {
        public Dictionary<int, Dictionary<T,Func<T, IState<T>>>> changeToOtherStateTable { get; private set; }

        public ASigma(Dictionary<int, Dictionary<T, Func<T, IState<T>>>> changeToOtherStateTable)
        {
            this.changeToOtherStateTable = changeToOtherStateTable;
        }

        public ASigma()
        {
            this.changeToOtherStateTable = new Dictionary<int, Dictionary<T, Func<T, IState<T>>>>();
        }

        public virtual void AddFunctionToTable(int StateID,Dictionary<T, Func<T, IState<T>>> dictionaryOfFunctions)
        {
            this.changeToOtherStateTable[StateID] = dictionaryOfFunctions;
        }

        public virtual void AddFunctionToTable(int StateID,T input,Func<T, IState<T>> functionForState)
        {
            if(ContainsKeys(StateID, input))
            {
                changeToOtherStateTable[StateID][input] = functionForState;
            }else
            {
                changeToOtherStateTable[StateID] = new Dictionary<T, Func<T, IState<T>>>();
                changeToOtherStateTable[StateID][input] = functionForState;

            }         
        }

        public IState<T> ChangeToOtherState(int ID,T input)
        {
            if(ContainsKeys(ID, input))
            {
                return changeToOtherStateTable[ID][input](input);
            } else
            {
                throw new ArgumentException("Provided keys are not found in table!");
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
