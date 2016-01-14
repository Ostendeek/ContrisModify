using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    class Line : FigureBase
    {
        //создание фигуры
        public Line()
        {
            for (int i = 0; i < 4; i++)
                _matrix[0, i] = true;
            _type = FigType.line;
            _position = 1;
        }

        //вращение фигуры
        public override void Rotate()
        {
            base.Rotate();
            int k;
            if (_matrix[0, 0])
            {
                Clear(_matrix);
                for (k = 0; k < 4; k++)
                    _matrix[k, 1] = true;
            }
            else
            {
                Clear(_matrix);
                for (k = 0; k < 4; k++)
                    _matrix[0, k] = true;
            }
        }

        //проверка возможности смещения влево
        public override bool CheckLeft(Field field)
        {
            if (_position == 1 || _position == 3)
            {
                if (field.TetrisField[field.CurY, field.CurX - 1] || field.CurX == 1) return false;
                else return true;
            }

            else
            {
                if (_position == 2 || _position == 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (field.TetrisField[field.CurY + i, field.CurX] || field.CurX == 0) return false;
                    }
                    return true;
                }
            }
            return false;
        }

        //проверка возможности смещения вправо
        public override bool CheckRight(Field field)
        {
            if (_position == 1 || _position == 3)
            {
                if (field.TetrisField[field.CurY, field.CurX + 4]  || field.CurX == field.Width - 5) return false;
                else return true;
            }

            else
            {
                if (_position == 2 || _position == 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (field.TetrisField[field.CurY + i, field.CurX + 2] || field.CurX == field.Width - 3) return false;
                    }
                    return true;
                }
            }
            return false;
        }

        //проверка возможности смещения вниз
        public override bool CheckDown(Field field)
        {
            if (_position == 1 || _position == 3)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (field.TetrisField[field.CurY + 1, field.CurX + i] || (field.CurY + 1) == field.Height - 1) 
                        return false;
                }
                return true;
            }
            else
            {
                if (_position == 2 || _position == 4)
                {
                    if (field.TetrisField[field.CurY + 4, field.CurX + 1] || (field.CurY + 4) == field.Height - 1) 
                        return false;
                }
                return true;
            }
            return false;
        }

        //проверка достижения "потолка"
        public override bool IsAtBottom(Field field)
        {
            for (int i = 0; i < 4; i++)
            {
                if (field.TetrisField[1, field.CurX + i] ) return true;
            }
            return false;
        }

        public override bool IsCanRotate(Field field)
        {
            return true;
        }
    }
}
