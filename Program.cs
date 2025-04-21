using System;

namespace ConnectFourGame
{
    //Interface
    interface IPlayer
    {
        void MakeMove(Board board);
    }

    //Abstract Player class (Abstraction)

    abstract class Player : IPlayer
    { 
        protected string name ;
        protected char symbol;

        public Player(string name, char symbol)
        {
            this.name = name;
            this.symbol = symbol;
        }

        public string Name => name;

        public char Symbol => symbol;

        public abstract void MakeMove(Board board); //Polymorphism

    }

    //Human Player (Inheritance)

     class HumanPlayer : Player
     {
        public HumanPlayer(string name, char symbol) : base(name,symbol) {}

        public override void MakeMove (Board board)
        {
            int column;
            bool validMove;
        do
        {
            Console.Write($"{Name} ({Symbol}), enter column (0-6):");
            string input = Console.ReadLine();
            validMove = int.TryParse(input , out column)&& board.DropDisc(column, Symbol);
             if (!validMove)
             {
                Console.WriteLine("Invalid move.Try again.");
             }
          } while (!validMove);
        }
     }
    
    // Board class (Encapsulation)
     class Board 
     {
        private char [,] grid = new char [6,7];

        public Board ()
        {
            for (int row = 0; row < 6 ; row ++)
              for (int col = 0; col < 7 ; col ++)
                grid [row,col]= ' ';
        }

        public bool DropDisc (int column , char symbol)
        {
            if (column < 0 || column > 6 || grid [0, column ] !=' ') return false ;

            for (int row = 5; row >= 0 ; row--) 
            { 
             if (grid [row,column] == ' ')
             {
                grid[row,column] = symbol ;
                return true;
              }
          }
            return false;
        }
        public void PrintBoard()
        {
            Console.WriteLine();
            for(int row = 0 ; row < 6; row++)
            {
               Console.Write("|");

               for (int col = 0 ; col < 7 ; col++ )
               {
                    Console.Write ($"{grid[row,col]}|")
               }
               Console.WriteLine();
            }
            Console.WriteLine("--------------");
            Console.WriteLine("0 1 2 3 4 5 6");
            Console.WriteLine();
        }
     }
}
