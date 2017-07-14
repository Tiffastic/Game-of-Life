using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GameOfLife
{

    public enum Cell : byte
    {
        Dead,
        Alive
    }

    class Game
    {
        // properties
        public byte Row
        {
            get;
            private set;
        }
        public byte Column
        {
            get;
            private set;
        }
        public Cell[,] Grid
        {
            get
            {
                return grid;
            }

        }

        // fields
        private Cell[,] grid, temp;
        private ConsoleColor[] colors = { ConsoleColor.DarkMagenta, ConsoleColor.Cyan, ConsoleColor.DarkGreen };
        private Random random = new Random();

        // enum
        private enum State : byte
        {
            Lonliness = 1,
            Stable = 3,
            Overcrowded = 4
        }
        
        private enum Default : byte
        {
            Row = 25, 
            Column = 40
        }

        // constructors
        public Game()
            : this((byte) Default.Row, (byte) Default.Column) 
        {
           SetDefaultPattern(grid); // seed grid with alive cells according to default pattern
        }


        public Game(byte rows, byte cols)
        {
            grid = new Cell[rows, cols];
            temp = new Cell[rows, cols];
            Row = rows;
            Column = cols;

            // users will seed the grid according to their wants
        }


        // methods
        private void SetDefaultPattern(Cell[,] board)
        {
            // seed grid with living cells
            for (byte index = 14; index <= 26; index++)
            {
                // horizontal block of alive cells
                board[13, index] = Cell.Alive;
            }

            for (byte index = 8; index <= 10; index++)
            {
                // vertical blocks of alive cells
                board[index, 12] = Cell.Alive;
                board[index, 20] = Cell.Alive;
                board[index, 28] = Cell.Alive;
            }
        }


        public void PrintGrid()
        {
            Console.ForegroundColor = colors[random.Next(colors.Length)];

            for (byte i = 0; i < Row; i++)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" ".PadRight((Console.WindowWidth - Column) / 2));

                for (byte j = 0; j < Column; j++)
                {
                    if (Grid[i, j] == Cell.Alive)
                    {
                        // print alive cells
                        Console.BackgroundColor = colors[random.Next(colors.Length)];


                        Console.Write("+");
                    }
                    else
                    {
                        // print dead cells
                        Console.BackgroundColor = ConsoleColor.Black;

                        Console.Write("-");
                    }

                }

                Console.WriteLine();

            }
        }


        public void NextGeneration()
        {
            for (byte i = 0; i < Row; i++)
            {
                for (byte j = 0; j < Column; j++)
                {
                    // count the neighbors of each cell
                    byte neighbors = CountNeighbors(i, j);
                    // update cell's state
                    UpdateState(neighbors, i, j);
                }
            }

            grid = temp;
            temp = new Cell[Row, Column];
        }


        private bool HasNeighbor(int rowI, int colJ)
        {
            if (rowI < 0 || rowI >= Row || colJ < 0 || colJ >= Column)
            {
                // out of bounds of array, no neighbors
                return false;
            }

            return Grid[rowI, colJ] == Cell.Alive;

        }


        private byte CountNeighbors(byte rowI, byte colJ)
        {
            byte neighbors = 0;

            // leftCursor neighbor
            if (HasNeighbor(rowI, colJ - 1))
            {
                neighbors++;
            }

            // downCursor neighbor
            if (HasNeighbor(rowI, colJ + 1))
            {
                neighbors++;
            }

            // above neighbor
            if (HasNeighbor(rowI - 1, colJ))
            {
                neighbors++;
            }

            // below neighbor
            if (HasNeighbor(rowI + 1, colJ))
            {
                neighbors++;
            }

            // northwest neighbor
            if (HasNeighbor(rowI - 1, colJ - 1))
            {
                neighbors++;
            }

            // northeast neighbor
            if (HasNeighbor(rowI - 1, colJ + 1))
            {
                neighbors++;
            }

            // southwest neighbor
            if (HasNeighbor(rowI + 1, colJ - 1))
            {
                neighbors++;
            }

            // southeast neighbor
            if (HasNeighbor(rowI + 1, colJ + 1))
            {
                neighbors++;
            }


            return neighbors;
        }


        private void UpdateState(byte neighbors, byte rowI, byte colJ)
        {
            // dead cell with 3 neighbors lives again
            if (Grid[rowI, colJ] == Cell.Dead && neighbors == (byte) State.Stable)
            {
                temp[rowI, colJ] = Cell.Alive;
                return;
            }

            if (Grid[rowI, colJ] == Cell.Dead)
            {
                return;

            }

            // cell with 0 or 1 neighbor dies
            if (neighbors <= (byte) State.Lonliness)
            {
                temp[rowI, colJ] = Cell.Dead;
            }
            // cell with 2 or 3 neighbors lives
            else if (neighbors <= (byte) State.Stable)
            {
                temp[rowI, colJ] = Cell.Alive;
                //return;
            }
            // cell with 4 or more neighbors dies
            else if (neighbors >= (byte) State.Overcrowded)
            {
                temp[rowI, colJ] = Cell.Dead;
            }

        }

    }
}
