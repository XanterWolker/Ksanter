using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Змеюка
{
    class Program
    {
       static public int height = 20;
       static public int width = 30;
        public ConsoleKeyInfo keypress = new ConsoleKeyInfo();
        static public bool gameOver, horizontal, vertical, isprinted;

      static public int[] TailX = new int[50];
      static public int[] TailY = new int[50];
        int speed = 0;
       
       

        static public int foodX, foodY, headX, headY, score,w, h, x, y, nTail;
         
        public enum Direction { Stop, Left, Right, Up, Down};
        static public Direction dir;
        static Direction Stop = 0;

        static public void ssset()
        {
            dir = Stop;
            gameOver = false;

            w = width / 2 - 1;
            h = height / 2 - 1;

            headX = width / 2;
            headY = height / 2;

            var rnd = new Random();
            foodX = rnd.Next() % width;
            foodY = rnd.Next() % height;

            score = 0;
            nTail = 0;
        }




        public void WriteBoard()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    if (i == 0 || i == height - 1)
                    {
                        Console.Write("*");
                    }
                    else if (j == 0 || j == width - 1)
                    {
                        Console.Write("*");
                    }
                    else if (j == foodX && i == foodY)
                    {
                        Console.Write("P");
                    }
                    else if (j == headX && i == headY)
                    {
                        Console.Write("$");
                    }
                    else
                    {
                        isprinted = false;
                        for (int k = 0; k < nTail; ++k)
                        {
                            if (TailX[k] == j && TailY[k] == i)
                            {
                                Console.Write("o");
                                isprinted = true;
                            }
                        }
                        if (!isprinted)
                        {
                            Console.Write(" ");
                        }
                    }



                }
                Console.WriteLine();

            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("        Shake Game              ");
            Console.WriteLine();
            Console.WriteLine("Your score:" + score);
        }
        public void Input()
        {

            ConsoleKey keypress = Console.ReadKey(true).Key;
            switch (keypress)
            {
                case ConsoleKey.UpArrow:
                    dir = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    dir = Direction.Down;
                    break;
                case ConsoleKey.LeftArrow:
                    dir = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    dir = Direction.Right;
                    break;
                case ConsoleKey.Escape:
                    gameOver = true;
                    break;
            }
        }

        public void difficult()
        {

            Console.WriteLine("                        Приветствую в игре повелитель змея");
            Console.WriteLine("                            Выбери уровень сложности:" +

            "\n                                  1 - Easy peasy" +

            "\n                                   2 - Medium" +

            "\n                                    3 - Death");




            int D = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            switch (D)
            {
                case 1:
                    speed = 1;
                    Thread.Sleep(100);
                    height = 10;
                    width = 10;
                    
                    break;

                case 2:
                    speed = 2;
                    height = 15;
                    width = 15;
                   
                    break;

                case 3:
                    speed = 3;
                    height = 20;
                    width = 20;
                   
                    break;

            }
            Console.Clear();

        }



        public void WritePoint(int x, int y)//рисует один элемент змеи
        {
            Console.SetCursorPosition(x, y);//ставит курсор
            Console.Write("$");
        }






        public void Logic()
        {
            int PreX = TailX[0];
            int PreY = TailY[0];
            int tempX, tempY;

            if (dir != Stop)
            {
                TailX[0] = headX;
                TailY[0] = headY;              
            }
            for (int i = 1; i<nTail; ++i )
            {
                tempX = TailX[i];
                tempY = TailY[i];
                TailX[i] = PreX;
                TailY[i] = PreY;
                PreX = tempX;
                PreY = tempY;
            }


            if (headX <= 0 || headX >= width - 1 || headY <= 0 || headY >= height - 1)
            {
                gameOver = true;
            }
            else
            {
                gameOver = false;
            }

            if (headX == foodX && headY == foodY)
            {

                    score += 10;
                    nTail++;
                    var rnd = new Random();
                    foodX = rnd.Next() % width;//размещение еды
                    foodY = rnd.Next() % height;//размещение еды
                
            }
            if (((dir == Direction.Up && dir != Direction.Left) && (dir == Direction.Up && dir != Direction.Right)) || ((dir == Direction.Down && dir != Direction.Left) && (dir == Direction.Down && dir != Direction.Right)))
            {
                horizontal = true;
            }
            else
            {
                horizontal = false;
            }

            if (((dir == Direction.Up && dir != Direction.Left) && (dir == Direction.Up && dir != Direction.Right)) || ((dir == Direction.Down && dir != Direction.Left) && (dir == Direction.Down && dir != Direction.Right)))
            {
                vertical = true;
            }
            else
            {
                vertical = false;
            }

            for (int i = 1; i < nTail; ++i)
            {
                if (TailX[i] == headX && TailY[i] == headY)
                {
                    if (horizontal || vertical)
                    {
                        gameOver = false;
                    }
                    else
                    {
                        gameOver = true;
                    }
                }
                var rnd = new Random();
                if (TailX[i] == foodX && TailY[i] == foodY)
                {
                    foodX = rnd.Next(1, width - 1);
                    foodY = rnd.Next(1, height - 1);
                }



            }



            for (int i = nTail; i > 1; i--)//установка последовательности элементов змеи
            {
                TailX[i - 1] = TailX[i - 2];
                TailY[i - 1] = TailY[i - 2];
            }

            for (int i = 0; i <= (nTail - 1); i++)//появление змеюки
            {
                WritePoint(TailX[i], TailY[i]);
                WritePoint(foodX, foodY);

            }
            

            switch (dir)
            {
                case Direction.Stop:
                    dir = 0;
                    break;
                case Direction.Up:
                    headY-=speed;
                    break;
                case Direction.Down:
                    headY+=speed;
                    break;
                case Direction.Right:
                    headX+=speed;
                    break;
                case Direction.Left:
                    headX-=speed;
                    break;
            }
           
            
          





        }
        void Update()
        {
            while(!gameOver)
            {
                WriteBoard();
                Logic();
                Input();
                
                
            }
        }

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;



            Program program = new Program();
            program.difficult();
            
            while (true)
            {
               
                ssset();
                program.Update();
                Console.Clear();
               
                



            }
            
        }
    }
}