using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine.GameOfLife
{
    class GridOfCells
    {
        Cell[,] cells;
        int width;
        int height;

        public GridOfCells(int numberOfCellsOnX, int numberOfCellsOnY)
        {
            this.width = numberOfCellsOnX -1;
            this.height = numberOfCellsOnY - 1;

            cells = new Cell[numberOfCellsOnX, numberOfCellsOnY];
            for (int i = 0; i < numberOfCellsOnX; i++)
                for (int j = 0; j < numberOfCellsOnX; j++)
                    cells[i, j] = new Cell();
        }

        private int[] NumberOfNeighbours(int i, int j)
        {
            int alive = 0;
            int dead = 0;
            if (width > 0 && height > 0)
            {
                for (int x = Math.Max(0, i - 1); x <= Math.Min(i + 1, width); x++)
                {
                    for (int y = Math.Max(0, j - 1); y <= Math.Min(j + 1, height); y++)
                    {
                        if (x != i || y != j)
                        {
                            if (cells[x, y].alive)
                            {
                                alive++;
                            }
                            else
                            {
                                dead++;
                            }
                        }
                    }
                }
            }

            return new int[] { alive, dead };
        }

        public int[] SearchCellPosition(Cell cell)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (cells[i, j] == cell)
                        return new int[] { i, j };
                }
            }
            throw new ArgumentException("Cell is not in this grid");
        }

        
        
        //Number of alive,dead neighbours
        public int[] this[int x, int y]
        {
            get => NumberOfNeighbours(x, y);
        }

        //If we don't know the position of the cell
        public int[] this[Cell searchedCell]
        {
            get {
                int[] position = SearchCellPosition(searchedCell);
                return NumberOfNeighbours(position[0], position[1]);
            }
        }
        

    }
}
