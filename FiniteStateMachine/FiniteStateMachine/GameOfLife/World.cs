using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiniteStateMachine;


namespace FiniteStateMachine.GameOfLife
{
    //Basicaly game logic
    class World : AStateTransducer<Cell,bool>
    {
        GridOfCells gridOne;
        GridOfCells gridTwo;
        public int width { get; set; } = 0;
        public int height { get; set; } = 0;

        int x;
        int y;

        //FinalStates - > null , what's the final state in this context anyways?
        public World(int x, int y,List<AState<Cell>> allStates, List<Cell> finiteInputSymbols, List<Cell> outputAlphabet, Sigma<Cell,bool> changeStateFunction) 
            : base(allStates, finiteInputSymbols,outputAlphabet, changeStateFunction, null, null)
        {
            gridOne = new GridOfCells(x, y, new Tuple<Cell, Cell>((Cell)allStates[0], (Cell)allStates[1]));
            gridTwo = new GridOfCells(x, y, new Tuple<Cell, Cell>((Cell)allStates[0], (Cell)allStates[1]));
            this.x = x;
            this.y = y;
        }


        public Bitmap NextFrame(string stayAliveRule, string reviveRule)
        {
            int[] stayAlive = Array.ConvertAll(stayAliveRule.Split(), int.Parse);
            int[] revive = Array.ConvertAll(reviveRule.Split(), int.Parse);

            Array.Sort(stayAlive);
            Array.Sort(revive);

            if (width > 0 && height > 0)
            {
                Bitmap cellsToMap = new Bitmap(width, height);

                for (int i = 0; i < x; x++)
                    for (int j = 0; j < y; y++)
                    {
                        Tuple<int, int> neighboursInfo = gridOne[i, j];
                        Cell currentCell = gridOne.cells[i, j];
                        Cell futureCell = gridTwo.cells[i, j];
                        if (currentCell.alive && !stayAlive.Contains(neighboursInfo.Item1))
                        {
                            futureCell = (Cell)changeStateFunction.ChangeStateFunction(currentCell, true);
                        } else if (!currentCell.alive && revive.Contains(neighboursInfo.Item2))
                        {
                            futureCell = (Cell)changeStateFunction.ChangeStateFunction(currentCell, false);
                        }
                    }

                GridOfCells temp = gridOne;
                gridOne = gridTwo;
                gridTwo = temp;
                
                //cellsToMap.
                

                //Console.WriteLine(((Cell)startState).alive);

                return null;

            }
            else
            {
                throw new InvalidOperationException("Width and height is not SET!");
            }

        }
    }
}
