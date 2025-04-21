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
                    Console.Write ($"{grid[row,col]}|");
               }
               Console.WriteLine();
            }
            Console.WriteLine("---------------");
            Console.WriteLine("0 1 2 3 4 5 6");
            Console.WriteLine();
        }
     

public bool IsFull()
{
    for(int col= 0; col<7; col++)
    {
        if(grid[0,col] == ' ') return false;
    }
    return true;
}  
public bool CheckWin(char symbol) 
{
    //Horizontal 
    for (int row= 0; row < 6; row++)
        for(int col= 0; col < 4 ; col++)
            if(grid[row , col] == symbol && grid[row , col+1] == symbol && grid[row , col+2] == symbol && grid[row , col+3]==symbol)
                return true;
    //Vertical
    for (int col= 0; col < 7; col++)
        for(int row= 0; row < 3 ; row++)
            if(grid[row , col] == symbol && grid[row+1 , col] == symbol && grid[row+2 , col] == symbol && grid[row+3 , col]==symbol)
                return true;
    //Diagonal 1
    for (int row= 3; row < 6; row++)
        for(int col= 0; col < 4 ; col++)
            if(grid[row , col] == symbol && grid[row-1 , col+1] == symbol && grid[row-2 , col+2] == symbol && grid[row-3 , col+3]==symbol)
                return true;
    //Diagonal 2
    for (int row= 3; row < 6; row++)
        for(int col= 3; col < 7 ; col++)
            if(grid[row , col] == symbol && grid[row-1 , col-1] == symbol && grid[row-2 , col-2] == symbol && grid[row-3 , col-3]==symbol)
                return true;

    return false;
  }
}
//Game Controller 
class GameController
{
    private Player player1;
    private Player player2;
    private Board board;

    public GameController(Player p1, Player p2)
    {
        player1 = p1;
        player2 = p2;
        board = new Board();
    }

    public void PlayGame()
    {
        Player current = player1;
        while(true)
        {
            board.PrintBoard();
            current.MakeMove(board);
            if(board.CheckWin(current.Symbol))
            {
                board.PrintBoard();
                Console.WriteLine($"{current.Name} ({current.Symbol}) wins!");
                break;
            }
            if (board.IsFull())
            {
                board.PrintBoard();
                Console.WriteLine("Its a draw!");
                break;
            }
            current = (current == player1) ? player2 : player1;
        }
    }

    public static void Main()
    {
        Console.WriteLine("Welcome to Connect Four!");
        Player p1= new HumanPlayer("Rajbir" , 'X');
        Player p2 = new HumanPlayer ("Sejal" , 'O');

        GameController game = new GameController(p1,p2);
        game.PlayGame();
    }
}
    
}

