using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace SnakeGame
{
    class Game
    {
        static readonly int width = 80;
        static readonly int height = 40;

        static Walls walls;
        static Snake snake;
        static FoodCreator food;
        static Timer time;

        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetWindowSize(width, height);



            Console.WriteLine("                        Приветствую в игре повелитель змея");
            Console.WriteLine("                            Выбери уровень сложности:" +

            "\n                                  1 - Easy peasy" +

            "\n                                   2 - Medium" +

            "\n                                    3 - Death");
            int n = 0;
            n = Convert.ToInt32(Console.ReadLine());
            Console.Clear();


            if (n == 1)
            {
                int x1 = 10;
                int y1 = 10;
                walls = new Walls(x1, y1, '*');
                snake = new Snake(x1 / 2, y1 / 2, 3);

                food = new FoodCreator(x1, y1, 'o');
            }
            if (n == 2)
            {
                int x1 = 15;
                int y1 = 15;
                walls = new Walls(x1, y1, '*');
                snake = new Snake(x1 / 2, y1 / 2, 3);                            //Выбор сложности размера поля
                 
                food = new FoodCreator(x1, y1, 'o');
            }
            if (n == 3)
            {
                int x1 = 20;
                int y1 = 20;
                walls = new Walls(x1, y1, '*');
                snake = new Snake(x1 / 2, y1 / 2, 3);

                food = new FoodCreator(x1, y1, 'o');

            }


            Console.SetWindowSize(width + 1, height + 1);
            Console.SetBufferSize(width + 1, height + 1);
            Console.CursorVisible = false;


            food.CreateFood();





            switch (n)
            {
                case 1:
                    {
                        time = new Timer(Loop, null, 0, 400);
                        break;
                    }
                case 2:
                    {
                        time = new Timer(Loop, null, 0, 200);          //Выбор сложности скорость

                        break;
                    }
                case 3:
                    {
                        time = new Timer(Loop, null, 0, 100);

                        break;
                    }
            }

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.Rotation(key.Key);
                }
            }
        }

        static void Loop(object obj)//Функция движения
        {
            if (walls.IsHit(snake.Head()) || snake.IsHit(snake.Head()))
            {
                time.Change(0, Timeout.Infinite);                     
            }
            else if (snake.Eat(food.food))
            {
                food.CreateFood();
            }
            else
            {
                snake.Move();
            }
        }
    }

    struct Point
    {
        public int x { get; set; }
        public int y { get; set; }
        public char ch { get; set; }

        public static implicit operator Point((int, int, char) value) =>
              new Point { x = value.Item1, y = value.Item2, ch = value.Item3 };

        public static bool operator ==(Point a, Point b) =>
                (a.x == b.x && a.y == b.y) ? true : false;
        public static bool operator !=(Point a, Point b) =>
                (a.x != b.x || a.y != b.y) ? true : false;

        public void Write()
        {
            WritePoint(ch);
        }
        public void Clear()
        {
            WritePoint(' ');
        }

        private void WritePoint(char _ch)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(_ch);
        }
    }

    class Walls//Создание стен
    {
        private char ch;
        private List<Point> wall = new List<Point>();

        public Walls(int x, int y, char ch)
        {
            this.ch = ch;

            DrawHorizontal(x, 0);
            DrawHorizontal(x, y);
            DrawVertical(0, y);
            DrawVertical(x, y);
        }

        private void DrawHorizontal(int x, int y)
        {
            for (int i = 0; i < x; i++)
            {
                Point p = (i, y, ch);
                p.Write();
                wall.Add(p);
            }
        }

        private void DrawVertical(int x, int y)
        {
            for (int i = 0; i < y; i++)
            {
                Point p = (x, i, ch);
                p.Write();
                wall.Add(p);
            }
        }

        public bool IsHit(Point p)//Поедание
        {
            foreach (var w in wall)
            {
                if (p == w)
                {
                    return true;
                }
            }
            return false;
        }
    }

    enum Direction//установка движений на стрелки
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    class Snake
    {
        private List<Point> snake;

        private Direction direction;
        private int step = 1;
        private Point tail;
        private Point head;

        bool rotate = true;

        public Snake(int x1, int y1, int length)//Сама змея и её вид
        {
            direction = Direction.RIGHT;

            snake = new List<Point>();
            for (int i = x1 - length; i < x1; i++)
            {

                Point p = (i, y1, 'S');
                snake.Add(p);

                p.Write();

            }
        }

        public Point Head() => snake.Last();

        public void Move()//Движение змеи
        {
            head = GetNextPoint();
            snake.Add(head);

            tail = snake.First();
            snake.Remove(tail);

            tail.Clear();
            head.Write();

            rotate = true;
        }

        public bool Eat(Point p)//Кормежка змеи
        {
            head = GetNextPoint();
            if (head == p)
            {
                snake.Add(head);
                head.Write();
                return true;
            }
            return false;
        }

        public Point GetNextPoint()//Движение по направлению стрелки
        {
            Point p = Head();



            switch (direction)
            {
                case Direction.LEFT:
                    p.x -= step;

                    break;
                case Direction.RIGHT:
                    p.x += step;

                    break;
                case Direction.UP:
                    p.y -= step;

                    break;
                case Direction.DOWN:
                    p.y += step;

                    break;
            }
            return p;
        }

        public void Rotation(ConsoleKey key)//Смена направления движений
        {
            if (rotate)
            {
                switch (direction)
                {
                    case Direction.LEFT:
                    case Direction.RIGHT:
                        if (key == ConsoleKey.DownArrow)
                            direction = Direction.DOWN;
                        else if (key == ConsoleKey.UpArrow)
                            direction = Direction.UP;
                        break;
                    case Direction.UP:
                    case Direction.DOWN:
                        if (key == ConsoleKey.LeftArrow)
                            direction = Direction.LEFT;
                        else if (key == ConsoleKey.RightArrow)
                            direction = Direction.RIGHT;
                        break;
                }
                rotate = false;
            }

        }

        public bool IsHit(Point p)
        {
            for (int i = snake.Count - 2; i > 0; i--)
            {
                if (snake[i] == p)
                {
                    return true;
                }
            }
            return false;
        }
    }

    class FoodCreator//Создание еды
    {
        int foodX;
        int foodY;
        char ch;
        public Point food { get; private set; }

        Random rnd = new Random();

        public FoodCreator(int x, int y, char ch)
        {
            this.foodX = x;
            this.foodY = y;
            this.ch = ch;
        }

        public void CreateFood()
        {
            food = (rnd.Next(2, foodX - 2), rnd.Next(2, foodY - 2), ch);
            food.Write();
        }
    }
}