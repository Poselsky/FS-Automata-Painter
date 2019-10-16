using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine
{
    public abstract class AFinalState<T> : AState<T>
    {

        public abstract bool ReturnIf(Predicate<T> predicate, T predicateInput);
    }
}
