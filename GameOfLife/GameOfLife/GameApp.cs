using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GameOfLife
{
    class GameApp
    {

        // properites
        public short WindowWidth
        {
            get
            {
                return windowWidth;
            }
            set
            {
                if (value >= 50 && value <= Console.LargestWindowWidth)
                {
                    windowWidth = value;
                }
                else
                {
                    windowWidth = (short) Default.Width;
                }
            }
        }
        public short WindowHeight
        {
            get
            {
                return windowHeight;
            }
            set
            {
                if (value >= 50 && value <= Console.LargestWindowHeight)
                {
                    windowHeight = value;
                }
                else
                {
                    windowHeight = (short) Default.Height;
                }
            }
        }
        public short CursorDown
        {
            get
            {
                return cursorDown;
            }
            set
            {
                if (value >= 0)
                {
                    cursorDown = value;
                }
                else
                {
                    cursorDown = (short) Default.Down;
                }

            }
        }
       
        // fields
        private short windowWidth, windowHeight, cursorDown;
        private Game life;

        // constructors
        public GameApp(Game game) : this(game, (short) Default.Width, (short) Default.Height, (short) Default.Down) 
        {
            life = game;
        }


        public GameApp(Game game, short windowWidth, short windowHeight, short cursorDown)
        {
            WindowWidth = windowWidth;
            WindowHeight = windowHeight;
            CursorDown = cursorDown;
            life = game;
        }

        // enum
        private enum Default : short
        {
            Down = 5,
            Width = 77,
            Height = 50
        }


        // methods
        private void PrepareWindow()
        {
            Console.SetWindowSize(WindowWidth, WindowHeight);
            Console.CursorVisible = false;
            Console.Title = "Game Of Life from John Conway";

        }


        public void PrintMessage(string message, int downCursor, ConsoleColor foreground)
        {
            try
            {
                Console.ForegroundColor = foreground;
                Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, downCursor);
                Console.WriteLine(message);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Message too long for window windowWidth");
                Console.WriteLine(e.Message);
            }

        }


        private void WelcomeScreen()
        {
            PrintMessage("Welcome to the Game Of Life!", CursorDown, ConsoleColor.Magenta);
            PrintMessage("From John Conway", CursorDown + 2, ConsoleColor.Blue);
            PrintMessage("Press Enter to Activate", CursorDown + 4, ConsoleColor.Magenta);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(0, CursorDown + 9);
            life.PrintGrid();

        }


        private void PrintInfo()
        {
            Console.Clear();

            PrintMessage("Game of Life by Thuy Nguyen", CursorDown, ConsoleColor.Magenta);
            PrintMessage("Press Any Key to Stop", CursorDown + 10 + life.Row, ConsoleColor.DarkBlue);
            PrintMessage("*".PadRight(40, '*'), CursorDown + 11 + life.Row, ConsoleColor.DarkBlue);
        }


        public void StartGame(int speed = 1500)
        {
            PrepareWindow();
            WelcomeScreen();

            bool life = true;

            while (life)
            {
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    PrintInfo();
                    Activate(speed);
                    life = false;
                }
            }

        }


        private void Activate(int speed)
        {
            int generation = 0;

            while (!Console.KeyAvailable)
            {

                Console.Title = "Conway's Game of Life Generation: " + ++generation;
                Console.SetCursorPosition(0, CursorDown + 5);

                life.PrintGrid();
                life.NextGeneration();

                Thread.Sleep(speed);

            }
        }
   
    }
}
