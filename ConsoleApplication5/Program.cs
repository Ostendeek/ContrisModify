using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    internal delegate void Up();

    internal delegate void Down();

    internal delegate void Left();

    internal delegate void Right();

    internal class EventUp
    {
        // Объявляем событие
        public event Up UpEvent;

        // Используем метод для запуска события
        public void UpUserEvent()
        {
            UpEvent();
        }
    }


    internal class EventDown
    {
        // Объявляем событие
        public event Down DownEvent;

        // Используем метод для запуска события
        public void DownUserEvent()
        {
            DownEvent();
        }
    }

    internal class EventLeft
    {
        // Объявляем событие
        public event Left LeftEvent;

        // Используем метод для запуска события
        public void LeftUserEvent()
        {
            LeftEvent();
        }
    }

    internal class EventRight
    {
        // Объявляем событие
        public event Right RightEvent;

        // Используем метод для запуска события
        public void RightUserEvent()
        {
            RightEvent();
        }
    }

    public enum FigType
    {
        line,
        square,
        rightL,
        leftL,
        pyramide,
        leftZ,
        rightZ
    }; //перечисление возможных фигур


    //класс игрового поля
    public class Field
    {
        private FigureBase fig;// = new Figura(); //фигура
        private int width; //ширина поля
        private int height; //высота поля
        private bool[,] tetrisField; //игровое поле
        private int curY; //текущая координата у
        private int curX; //текущая координата х
        private int scores; //очки за игру
        private int level;

        public FigureBase Fig
        {
            get { return fig; }
        }

        public int Scores
        {
            get { return scores; }
        }

        public int Level
        {
            get { return level; }
        }

        public bool[,] TetrisField
        {
            get { return tetrisField; }
        }

        public int Width
        {
            get { return width; }
           
        }

        public int Height
        {
            get { return height; }

        }

        public int CurX
        {
            get { return curX; }

        }

        public int CurY
        {
            get { return curY; }

        }

        public Field(int w, int h)
        {
            width = w;
            height = h;
            tetrisField = new bool[height, width];
            level = 0;
            scores = 0;
        }

        //отрисовка поля
        public void DrawField()
        {

            for (int i = 0; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    Console.CursorLeft = j;
                    Console.CursorTop = i;
                    if (tetrisField[i, j] == false) 
                        Console.WriteLine(" ");
                    else 
                        Console.WriteLine("0");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n   Level " + this.level);
            Console.WriteLine("\n  Scores " + this.scores);
        }

        //отбражение фигуры на игровом поле
        public void Copy()
        {
            var x = curX; //временные переменные координаты для заполнения части поля
            int y = curY;

            for (var i = 0; i < 4; i++)
            {
                x = curX;

                for (var j = 0; j < 4; j++)
                {
                    if (fig.Matrix[i, j]) 
                        tetrisField[y, x] = true;
                    x++;
                }
                y++;
            }
        }

        //создание новой фигуры
        public void NewFig()
        {
            var r = new Random();
            curY = 0;
            curX = 5;

            //FigType.rightL;//
            var t = (FigType) r.Next(0, 7);
            switch(t)
            {
                case FigType.line: 
                    fig = new Line();
                    break;
                case FigType.pyramide: 
                    fig = new Pyramide();
                    break;
                case FigType.leftZ:
                    fig = new LeftZ();
                    break;
                case FigType.leftL:
                    fig = new LeftL();
                    break;
                case FigType.rightZ:
                    fig = new RightZ();
                    break;
                case FigType.rightL:
                    fig = new RightL();
                    break;
                case FigType.square:
                    fig = new Square();
                    break;

            }

            Copy(); //отображение         

        }

        //движение фигуры вниз
        public void Move()
        {
            ClearPrevious();
            curY++;
            Copy();
            DrawField();

        }

        //стирание предыдущего шага
        public void ClearPrevious()
        {
            int m = 0;
            int n = 0;

            for (int i = curY; i < curY + 4; i++)
            {
                for (int j = curX; j < curX + 4; j++)
                {
                    if (fig.Matrix[m, n]) 
                        tetrisField[i, j] = false;
                    n++;
                }
                m++;
                n = 0;
            }

        }

        //проверка возможности вращения фигуры
        public bool CheckRotation()
        {

            return true;
        }


        //проверка линии
        public bool CheckLine()
        {
            int counter = 0; //счетчик занятых ячеек в линии
            int k = 0;

            for (int i = 0; i < height; i++)
            {
                counter = 0;
                for (int j = 0; j < width; j++)
                {
                    if (tetrisField[i, j]) 
                        counter++; //подсчет занятых ячеек в линии

                    if (counter == 10)
                    {
                        k = i; //запоминаем линию в которой все ячейки заняты
                        break;
                    }
                }
            }

            if (k == 0) 
                return false;
            else
            {
                for (int i = 0; i < width; i++)
                {
                    tetrisField[k, i] = false;
                }

                for (int i = k; i > 0; i--)
                {
                    for (int j = 0; j < width; j++)
                    {
                        tetrisField[i, j] = tetrisField[i - 1, j];
                    }
                }
                scores += 100;
                if (scores == 1000)
                {
                    level++;
                    scores = 0;
                }
                return true;
            }

        }

        //обработчик события вверх: поворот фигуры
        public void UpFig()
        {
            ClearPrevious();
            fig.Rotate();
            Copy(); //отображение
        }

        //обработчик события вниз: падение фигуры
        public void DownFig()
        {
            while (fig.CheckDown(this))
            {
                Move();
            }
        }

        //обработчик события влево: смещение фигуры влево
        public void LeftFig()
        {
            if (!fig.CheckLeft(this))
                return;

            ClearPrevious();
            curX--;
            Copy();
        }

        //обработчик события вправо: смещение фигуры вправо
        public void RightFig()
        {
            if (!fig.CheckRight(this))
                return;

            ClearPrevious();
            curX++;
            Copy();

        }

    }


    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            for (int i = 0; i < 20; i++)
            {
                Console.CursorLeft = 0;
                Console.CursorTop = i;
                Console.WriteLine("0");
            }

            for (int i = 1; i < 12; i++)
            {
                Console.CursorLeft = i;
                Console.CursorTop = 19;
                Console.WriteLine("0");
            }

            for (int i = 0; i < 20; i++)
            {
                Console.CursorLeft = 11;
                Console.CursorTop = i;
                Console.WriteLine("0");
            }

            Console.BackgroundColor = ConsoleColor.Black;

            Field f = new Field(12, 20);

            f.NewFig();

            f.DrawField();

            //создание объектов событий нажатия клавиш
            EventUp up = new EventUp(); //вверх
            EventDown down = new EventDown(); //вниз
            EventLeft left = new EventLeft(); // влево
            EventRight right = new EventRight(); //вправо

            up.UpEvent += f.UpFig;
            down.DownEvent += f.DownFig;
            left.LeftEvent += f.LeftFig;
            right.RightEvent += f.RightFig;

            ConsoleKeyInfo cki;
            //Скорее всего это можно поднять наверх, логика не идеально
            while (true)
            {
                if (f.Fig.CheckDown(f))
                {
                    f.Move();
                }
                else
                {
                    while (true)
                    {
                        bool flag = f.CheckLine();
                        if (flag == false) 
                            break;
                    }
                    f.NewFig();
                    if (f.Fig.IsAtBottom(f) == true) 
                        break;
                }


                for (int i = 0; i < 10 - f.Level; i++) //количество итераций цикла имитирует скорость
                {
                    System.Threading.Thread.Sleep(50);
                    if (Console.KeyAvailable)
                    {
                        cki = Console.ReadKey();


                        switch (cki.Key)
                        {
                            case ConsoleKey.UpArrow: //стрелка вверх
                            {
                                if (!f.Fig.IsCanRotate(f)) 
                                    break;
                                up.UpUserEvent(); //обработчик события
                                f.DrawField(); //перерисовка поля
                                break;
                            }

                            case ConsoleKey.DownArrow:
                            {
                                down.DownUserEvent();
                                break;
                            }

                            case ConsoleKey.LeftArrow:
                            {
                                left.LeftUserEvent();
                                f.DrawField(); //перерисовка поля
                                break;
                            }

                            case ConsoleKey.RightArrow:
                            {
                                right.RightUserEvent();
                                f.DrawField();
                                break;
                            }

                            default:
                            {
                                break;
                            }
                        }
                    }
                }
            }

            Console.Clear();

            Console.WriteLine("\n\n\n      GAME OVER");
            Console.WriteLine("\n   TOTAL SCORES " + (f.Level*1000 + f.Scores) + "\n\n\n\n\n\n\n\n\n");
            Console.ReadLine();

        }
    }
}
