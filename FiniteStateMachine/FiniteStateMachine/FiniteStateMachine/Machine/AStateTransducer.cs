﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine
{
    abstract public class AStateTransducer<T>
    {
        //definition says : ordered 6 objects -- again, makes life harder
        //protected List<object> orderedList { get; set; } = new List<object>();

        List<AState<T>> allStates;
        List<T> finiteInputSymbols;
        public IList<T> outputAlphabet { get; private set; }
        Sigma<T> changeStateFunction;
        AState<T> startState;
        List<FinalState<T>> finalStates;


        public T this[int i] => outputAlphabet[i];

        public AStateTransducer(List<AState<T>> allStates, List<T> finiteInputSymbols ,IList<T> outputAlphabet, Sigma<T> changeStateFunction, AState<T> startState, List<FinalState<T>> finalStates)
        {
            this.allStates = allStates;
            this.finiteInputSymbols = finiteInputSymbols;
            this.changeStateFunction = changeStateFunction;
            this.startState = startState;
            this.finalStates = finalStates;

            this.outputAlphabet = outputAlphabet;
            
        }
    }
}
