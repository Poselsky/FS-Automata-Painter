using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine.GameOfLife
{
    class Cell : AState<Cell>
    {
        private static Random rand = new Random();
        public bool alive { get; set; }

        //when creating cell, we randomize the world, if cell is alive or not
        public Cell() : base()
        {
            int determineBool = rand.Next(0, 2);

            //will generate True False values
            alive = determineBool == 1;
        }

        public Cell(bool alive)
        {
            this.alive = alive;
        }

        public override Cell Reaction(Func<Cell, Cell> action, Cell funcParameter)
        {
            throw new NotImplementedException();
        }
    }
}
