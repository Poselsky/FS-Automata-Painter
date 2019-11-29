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
    class World : AStateMachine<Cell,bool>
    {
        GridOfCells gridOne;
        GridOfCells gridTwo;
        public int width { get; set; } = 0;
        public int height { get; set; } = 0;

        int x;
        int y;

        //FinalStates - > null , what's the final state in this context anyways?
        public World(int x, int y,List<AState<Cell>> allStates,  Sigma<bool, Cell> changeStateFunction) 
            : base(allStates, null, changeStateFunction, null, null)
        {
            gridOne = new GridOfCells(x, y, new Tuple<Cell, Cell>((Cell)allStates[0], (Cell)allStates[1]));
            gridTwo = new GridOfCells(x, y, new Tuple<Cell, Cell>((Cell)allStates[0], (Cell)allStates[1]));
            this.x = x;
            this.y = y;
            
            /*
            gridOne.cells[0, 0].alive = false;
            gridOne.cells[1, 0].alive = false;
            gridOne.cells[2, 0].alive = false;
            gridOne.cells[3, 0].alive = false;
            gridOne.cells[0, 1].alive = false;
            gridOne.cells[1, 1].alive = false;
            gridOne.cells[2, 1].alive = false;
            gridOne.cells[3, 1].alive = false;
            gridOne.cells[0, 2].alive = false;
            gridOne.cells[1, 2].alive = false;
            gridOne.cells[2, 2].alive = false;
            gridOne.cells[3, 2].alive = false;
            gridOne.cells[0, 3].alive = false;
            gridOne.cells[1, 3].alive = false;
            gridOne.cells[2, 3].alive = false;
            gridOne.cells[3, 3].alive = false;
            */
        }


        public Bitmap NextFrame(string stayAliveRule, string reviveRule)
        {
            int[] stayAlive = Array.ConvertAll(stayAliveRule.ToCharArray(), c => (int)Char.GetNumericValue(c));
            int[] revive = Array.ConvertAll(reviveRule.ToCharArray(), c => (int)Char.GetNumericValue(c));

            Array.Sort(stayAlive);
            Array.Sort(revive);

            if (width > 0 && height > 0)
            {
                for (int i = 0; i < x; i++)
                    for (int j = 0; j < y; j++)
                    {
                        Tuple<int, int> neighboursInfo = gridOne[i, j];
                        Cell currentCell = gridOne.cells[i, j];
                        Cell futureCell = gridTwo.cells[i, j];
                        if (currentCell.alive && !stayAlive.Contains(neighboursInfo.Item1))
                        {
                            futureCell = (Cell)changeStateTableReversed.ChangeStateFunction(currentCell, false);
                        } else if (!currentCell.alive && revive.Contains(neighboursInfo.Item2) || stayAlive.Length == 0)
                        {
                            futureCell = (Cell)changeStateTableReversed.ChangeStateFunction(currentCell, true);
                        }
                    }

                Bitmap res = MapGridToBitmap(gridTwo);

                //swapping "Generations"
                GridOfCells temp = gridOne;
                gridOne = gridTwo;
                gridTwo = temp;

                return res; //.ResizeBitmap(width, height);
            }
            else
            {
                throw new InvalidOperationException("Width and height is not SET!");
            }

        }


        //To use for each syntax easily
        public IEnumerable<Bitmap> NextFrames(string stayAliveRule, string reviveRule, int numberOfIterations)
        {
            for (int i = 0; i < numberOfIterations; i++)
                yield return NextFrame(stayAliveRule, reviveRule);
        }

        private Bitmap MapGridToBitmap(GridOfCells grid)
        {
            Bitmap cellsToMap = new Bitmap(width, height);


            using (Graphics img = Graphics.FromImage(cellsToMap))
            {
                int first = -1;
                int second = -1;
                for (int i = 0; i < width; i += width / x)
                {
                    first++;
                    for (int j = 0; j < height; j+=height/y)
                    {
                        second++;
                        if (grid.cells[first,second].alive)
                        {
                            img.FillRectangle(new SolidBrush(Color.Black), i, j, i+width, j+height);
                        }
                        else
                        {
                            img.FillRectangle(new SolidBrush(Color.White), i, j, i + width, j + height);
                        }
                        
                    }
                    second = -1;
                }
            }
            return cellsToMap;
        }
    }
}
