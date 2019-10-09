using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine
{
    public class FinalState<T> : BasicState<T>, IFinalState<T>
    {
        public FinalState():base()
        {

        }

        public bool ReturnIf(Predicate<T> predicate, T predicateInput)
        {
            return predicate(predicateInput);
        }

    }
}
