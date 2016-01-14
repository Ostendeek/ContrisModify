using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    class Square : FigureBase
    {
         //создание фигуры
        public Square()
        {
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    _matrix[i, j] = true;
            _type = FigType.square;
            _position = 1;
        }

        //вращение фигуры
        public override void Rotate()
        {
             return;
        }
 
        //проверка возможности смещения влево
        public override bool CheckLeft(Field field)
        {

            if (field.TetrisField[field.CurY + 1, field.CurX - 1] || field.TetrisField[field.CurY + 1, field.CurX - 1] || field.CurX == 1)
                return false;
            return true;

        }

        //проверка возможности смещения вправо
        public override bool CheckRight(Field field)
        {
            if (field.TetrisField[field.CurY, field.CurX + 2] == true || field.TetrisField[field.CurY + 1, field.CurX + 2] == true || field.CurX == field.Width - 3)
                return false;
            else 
                return true;
            return false;
        }

        //проверка возможности смещения вниз
        public override bool CheckDown(Field field)
        {
            if (field.TetrisField[field.CurY + 2, field.CurX] == true || field.TetrisField[field.CurY + 2, field.CurX + 1] == true ||
                (field.CurY + 2) == field.Height - 1) return false;

            return false;
        }

        //проверка достижения "потолка"
        public override bool IsAtBottom(Field field)
        {
            if (field.TetrisField[2, field.CurX] == true || field.TetrisField[2, field.CurX + 1] == true) 
                return true;
            return false;
        }

        public override bool IsCanRotate(Field field)
        {
            return true;
        }
    }
}
