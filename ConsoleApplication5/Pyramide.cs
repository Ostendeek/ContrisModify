using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    class Pyramide : FigureBase
    {
         //создание фигуры
       public Pyramide()
        {
            for (int i = 0; i < 3; i++)
            _matrix[1, i] = true;
             _matrix[0, 1] = true;
        }

        //вращение фигуры
        public override void Rotate()
        {
            if (_position == 4)
                _position = 1; //в начальном положении
            _position++; //меняем положение
            bool[,] tempFig = new bool[4, 4];
            Clear(tempFig);

            for (int j = 3 - 1, c = 0; j >= 0; j--, c++)
                for (int i = 0; i < 3; i++)
                    tempFig[c, i] = _matrix[i, j];

            Clear(_matrix);

            for (int f = 0; f < 3; f++)
                for (int d = 0; d < 3; d++)
                    _matrix[f, d] = tempFig[f, d];
        }

        //проверка возможности смещения влево
        public override bool CheckLeft(Field field)
        {
            if (_position == 1)
            {
                if (field.TetrisField[field.CurY, field.CurX] || field.TetrisField[field.CurY + 1, field.CurX - 1]  || field.CurX == 1)
                    return false;
                else return true;
            }

            else
            {
                if (_position == 2)
                {
                    if (field.TetrisField[field.CurY, field.CurX] || field.TetrisField[field.CurY + 1, field.CurX - 1]  ||
                        field.TetrisField[field.CurY + 2, field.CurX]  || field.CurX == 1) return false;
                    else return true;
                }

                if (_position == 3)
                {
                    if (field.TetrisField[field.CurY + 1, field.CurX - 1]  || field.TetrisField[field.CurY + 2, field.CurX]  || field.CurX == 1)
                        return false;
                    else return true;
                }

                if (_position == 4)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (field.TetrisField[field.CurY + i, field.CurX]  || field.CurX == 0) return false;
                    }
                    return true;
                }


            }
            return false;
        }

        //проверка возможности смещения вправо
        public override bool CheckRight(Field field)
        {
            if (_position == 1)
            {
                if (field.TetrisField[field.CurY, field.CurX + 2]  || field.TetrisField[field.CurY + 1, field.CurX + 3]  ||
                    field.CurX == field.Width - 4) return false;
                else return true;
            }

            else
            {
                if (_position == 2)
                {
                    if (field.TetrisField[field.CurY, field.CurX + 2]  || field.TetrisField[field.CurY + 1, field.CurX + 2]  ||
                        field.TetrisField[field.CurY + 2, field.CurX + 2]  || field.CurX == field.Width - 3) return false;
                    else return true;
                }

                if (_position == 3)
                {
                    if (field.TetrisField[field.CurY + 1, field.CurX + 3] || field.TetrisField[field.CurY + 2, field.CurX + 2]  ||
                        field.CurX == field.Width - 4) return false;
                    else return true;
                }

                if (_position == 4)
                {
                    if (field.TetrisField[field.CurY, field.CurX + 2]  || field.TetrisField[field.CurY + 1, field.CurX + 3]  ||
                        field.TetrisField[field.CurY + 2, field.CurX + 2]  || field.CurX == field.Width - 4) return false;
                    else return true;
                }


            }
            return false;
        }

        //проверка возможности смещения вниз
        public override bool CheckDown(Field field)
        {
            if (_position == 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (field.TetrisField[field.CurY + 2, field.CurX + i]  || (field.CurY + 2) == field.Height - 1) return false;
                }
                return true;
            }

            else
            {
                if (_position == 2)
                {
                    if (field.TetrisField[field.CurY + 2, field.CurX] || field.TetrisField[field.CurY + 3, field.CurX + 1] ||
                        (field.CurY + 3) == field.Height - 1) 
                        return false;
                    return true;
                }

                if (_position == 3)
                {
                    if (field.TetrisField[field.CurY + 2, field.CurX] || field.TetrisField[field.CurY + 3, field.CurX + 1] ||
                        field.TetrisField[field.CurY + 2, field.CurX + 2] || (field.CurY + 3) == field.Height - 1) 
                        return false;
                    return true;
                }

                if (_position == 4)
                {
                    if (field.TetrisField[field.CurY + 3, field.CurX + 1] || field.TetrisField[field.CurY + 2, field.CurX + 2] ||
                        (field.CurY + 3) == field.Height - 1) 
                        return false;
                    return true;
                }

            }
            return false;
        }

        //проверка достижения "потолка"
        public override bool IsAtBottom(Field field)
        {
            for (int i = 0; i < 3; i++)
            {
                if (field.TetrisField[2, field.CurX + i] ) return true;
            }
            return false;
        }

        public override bool IsCanRotate(Field field)
        {
            return true;
        }
    }
}
