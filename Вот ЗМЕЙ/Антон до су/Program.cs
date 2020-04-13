using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Security;

namespace Змеюка
{
    class Program
    {
        static public int height = 40;
        static public int width = 80;
        public ConsoleKeyInfo keypress = new ConsoleKeyInfo();
        static public bool gameOver, horizontal, vertical, isprinted;

        static public int[] TailX = new int[50];
        static public int[] TailY = new int[50];
        int speed = 0;



        static public int foodX, foodY, headX, headY, score, w, h, x, y, nTail;

        public enum Direction { Stop, Left, Right, Up, Down };
        static public Direction dir;
        static Direction Stop = 0;

        static public void ssset()
        {
            dir = Direction.Right;
            gameOver = false;

            w = width / 2 - 1;
            h = height / 2 - 1;                //Установка начальных параметров

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
                    else if (j == 0 || j == width - 1)                  //Создание поля
                    {
                        Console.Write("*");
                    }
                    else if (j == foodX && i == foodY)
                    {
                        Console.Write("o");
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
            Console.WriteLine("        Игра Змейка              ");
            Console.WriteLine();
            Console.WriteLine("Ваш счет:" + score);
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
                case ConsoleKey.LeftArrow:                          //Установка движений по нажатию стрелок
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
            Console.SetWindowSize(width, height);



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


                    height = 10;
                    width = 10;

                    break;

                case 2:

                    height = 15;
                    width = 15;                              //Выбор сложности

                    break;

                case 3:

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
            for (int i = 1; i < nTail; ++i)
            {
                tempX = TailX[i];
                tempY = TailY[i];                               //Построение логики игры
                TailX[i] = PreX;
                TailY[i] = PreY;
                PreX = tempX;
                PreY = tempY;
            }


            if (headX <= 0 || headX >= width - 1 || headY <= 0 || headY >= height - 1)
            {
                gameOver = true;
            }
            else                                                     //Логика проигрыша
            {
                gameOver = false;
            }

            if (headX == foodX && headY == foodY)
            {

                score += 10;                        //Поедание и начисление очков
                nTail++;
                var rnd = new Random();
                foodX = rnd.Next() % width;//размещение еды по новой
                foodY = rnd.Next() % height;//размещение еды по новой

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
                    foodX = rnd.Next(1, width - 3);
                    foodY = rnd.Next(1, height - 3);
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

                case Direction.Up:
                    headY--;
                    break;
                case Direction.Down:
                    headY++;
                    break;
                case Direction.Right:
                    headX++;
                    break;
                case Direction.Left:
                    headX--;
                    break;
                case Direction.Stop:
                    dir = 0;
                    break;

            }


        }

        void Update()
        {
            while (!gameOver)
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