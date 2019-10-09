using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine
{
    public class BasicState<T> : IState<T>
    {
        public int ID { get => ID; set => ID = value; }

        public BasicState()
        {
            Random ran = new Random();
            int numbersInName = 8;
            string stringifiedID = "";
            for(int i = 0; i < numbersInName; i++)
            {
                stringifiedID += ran.Next(10);
            }
            this.ID = int.Parse(stringifiedID);
        }


        public T Reaction(Func<T, T> action, T funcParameter)
        {
            return action(funcParameter);
        }
    }
}
