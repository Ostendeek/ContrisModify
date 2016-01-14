using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    public abstract class FigureBase
    {
        protected bool[,] _matrix = new bool[4, 4]; //матрица контейнер для размещения фигур

        protected FigType _type; //тип фигуры

        protected int _position; //положение фигуры

        public bool[,] Matrix
        {
            get { return _matrix; }
        } 

        public void Clear(bool[,] m)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    m[i, j] = false;
        }

        public virtual void Rotate()
        {
            if (_position == 4)
                _position = 1; //в начальном положении
            else
                _position++; //меняем положение
        }

        public abstract bool CheckLeft(Field field);

        public abstract bool CheckRight(Field field);

        public abstract bool CheckDown(Field field);

        public abstract bool IsAtBottom(Field field);

        public abstract bool IsCanRotate(Field field);
    }
}
