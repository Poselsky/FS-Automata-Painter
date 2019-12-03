using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine
{
    public class BasicState<T> : AState<T>
    {

        public BasicState():base()
        {
            
        }


        public override T Reaction(Func<T, T> action, T funcParameter)
        {
            return action(funcParameter);
        }
    }
}
