using System;
using System.Threading;
namespace Змеюка
{
    class Program
    {
        int Height = 20;
        int Width = 30;

        int[] X = new int[50];
        int[] Y = new int[50];

        int foodX;
        int foodY;

        int elementsss = 3;

        ConsoleKeyInfo KeyInfo = new ConsoleKeyInfo();
        char key = 'W';

        Random rnd = new Random();

        Program()
        {
            X[0] = 5;
            Y[0] = 5;
            Console.CursorVisible = false;
            foodX = rnd.Next(2, (Width - 2));//размещение еды
            foodY = rnd.Next(2, (Height - 2));//размещение еды
        }

        public void WriteBoard()
        {
            Console.Clear();
            for (int i = 1; i <= (Width + 2); ++i)
            {
                Console.SetCursorPosition(i, 1);
                Console.Write("*");

            }
            for (int i = 1; i <= (Width + 2); ++i)
            {
                Console.SetCursorPosition(i, (Height + 2));
                Console.Write("*");
            }
            for (int i = 1; i <= (Height + 1); i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write("*");
            }
            for (int i = 1; i <= (Height + 1); ++i)
            {
                Console.SetCursorPosition((Width + 2), i);
                Console.Write("*");
            }

        }

        public void Input()
        {
            if (Console.KeyAvailable)//проверка нажатия клавиши
            {
                KeyInfo = Console.ReadKey(true);//нажатие клавиши
                key = KeyInfo.KeyChar;//замена клавиши на значение Char
            }
        }

        public void difficult()
        {

            Console.WriteLine("Выберите уровень сложности:" +

            "\n1 - Easy peasy" +

            "\n2 - Medium" +

            "\n3 - Death");

            int D = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            switch (D)
            {
                case 1:
                    Height = 10;
                    Width = 10;
                    Thread.Sleep(100);
                    break;

                case 2:
                    Height = 15;
                    Width = 15;
                    Thread.Sleep(200);
                    break;

                case 3:
                    Height = 20;
                    Width = 20;
                    Thread.Sleep(400);
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
            if (X[0] == foodX)
            {
                if (Y[0] == foodY)
                {
                    elementsss++;
                    foodX = rnd.Next(2, (Width - 2));//размещение еды
                    foodY = rnd.Next(2, (Height - 2));//размещение еды
                }
            }

            for (int i = elementsss; i > 1; i--)//установка последовательности элементов змеи
            {
                X[i - 1] = X[i - 2];
                Y[i - 1] = Y[i - 2];
            }
            switch (key)
            {
                case 'w':
                    Y[0]--;
                    break;
                case 's':
                    Y[0]++;
                    break;
                case 'd':
                    X[0]++;
                    break;
                case 'a':
                    X[0]--;
                    break;

            }
            for (int i = 0; i <= (elementsss - 1); i++)//появление змеюки
            {
                WritePoint(X[i], Y[i]);
                WritePoint(foodX, foodY);

            }
            Thread.Sleep(100);
        }

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;



            Program program = new Program();
            program.difficult();
            Console.Clear();
            while (true)
            {

                program.WriteBoard();

                program.Input();
                program.Logic();

            }
            Console.ReadKey();
        }
    }
}