using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine
{
    public class FinalState<T> : AFinalState<T>
    {
        public FinalState():base()
        {

        }

        public override T Reaction(Func<T, T> action, T funcParameter)
        {
            throw new NotImplementedException();
        }

        public override bool ReturnIf(Predicate<T> predicate, T predicateInput)
        {
            return predicate(predicateInput);
        }

    }
}
