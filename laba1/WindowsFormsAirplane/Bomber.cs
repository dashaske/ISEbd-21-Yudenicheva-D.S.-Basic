using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAirplane
{
    public class Bomber : WarPlane
    {
        /// Признак наличия звезды
        public bool Star { private set; get; }

        /// Признак наличия бомб
        public bool Bomb { private set; get; }

        /// Признак наличия ракет
        public bool Rocket { private set; get; }

        public Color DopColor { protected set; get; }

        // Смена дополнительного цвета
        public void SetDopColor(Color color)
        {
            DopColor = color;
        }
        public Bomber(int maxSpeed, float weight, Color mainColor, Color dopColor,
       bool star, bool bomb, bool rocket) :
        base(maxSpeed, weight, mainColor)
        {
            MaxSpeed = maxSpeed;
            Weight = weight;
            MainColor = mainColor;
            DopColor = dopColor;
            Star = star;
            Bomb = bomb;
            Rocket = rocket;
        }
        //Конструктор для загрузки с файла
        public Bomber(string info) : base(info)
        {
            string[] strs = info.Split(separator);
            if (strs.Length == 7)
            {
                MaxSpeed = Convert.ToInt32(strs[0]);
                Weight = Convert.ToInt32(strs[1]);
                MainColor = Color.FromName(strs[2]);
                DopColor = Color.FromName(strs[3]);
                Star = Convert.ToBoolean(strs[4]);
                Bomb = Convert.ToBoolean(strs[5]);
                Rocket = Convert.ToBoolean(strs[6]);
            }
        }
        /// <summary>
        /// Изменение направления пермещения
        /// </summary>
        /// <param name="direction">Направление</param>
        public void MovePlane(Direction direction)
        {
            float step = MaxSpeed * 100 / Weight;
            switch (direction)
            {
                // вправо
                case Direction.Right:
                    if (_startPosX + step < _pictureWidth - planeWidth)
                    {
                        _startPosX += step;
                    }
                    break;
                //влево
                case Direction.Left:
                    if (_startPosX - step > 0)
                    {
                        _startPosX -= step;
                    }
                    break;
                //вверх
                case Direction.Up:
                    if (_startPosY - step > 0)
                    {
                        _startPosY -= step;
                    }
                    break;
                //вниз
                case Direction.Down:
                    if (_startPosY + step < _pictureHeight - planeHeight)
                    {
                        _startPosY += step;
                    }
                    break;
            }
        }
        public override void DrawFly(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            //рисуем звезду          
            if (Rocket)
            {
                g.DrawRectangle(pen, _startPosX + 80, _startPosY + 10, 40, 10);
                Brush spoiler = new SolidBrush(DopColor);
                g.FillRectangle(spoiler, _startPosX + 80, _startPosY + 10, 40, 10);
                g.DrawRectangle(pen, _startPosX + 80, _startPosY + 80, 40, 10);
                g.FillRectangle(spoiler, _startPosX + 80, _startPosY + 80, 40, 10);
            }
            //рисуем бомбы
            if (Bomb)
            {
                g.DrawEllipse(pen, _startPosX + 90, _startPosY, 30, 10);
                Brush spoiler = new SolidBrush(DopColor);
                g.FillEllipse(spoiler, _startPosX + 90, _startPosY, 30, 10);
                g.DrawEllipse(pen, _startPosX + 90, _startPosY + 90, 30, 10);
                g.FillEllipse(spoiler, _startPosX + 90, _startPosY + 90, 30, 10);
            }

            //отрисовка тела
            if (Star)
            {
                base.DrawFly(g);
                Point point1 = new Point((int)_startPosX + 85, (int)_startPosY + 60);
                Point point2 = new Point((int)_startPosX + 90, (int)_startPosY + 65);
                Point point3 = new Point((int)_startPosX + 95, (int)_startPosY + 60);
                Point point4 = new Point((int)_startPosX + 95, (int)_startPosY + 90);
                Point point5 = new Point((int)_startPosX + 90, (int)_startPosY + 85);
                Point point6 = new Point((int)_startPosX + 85, (int)_startPosY + 90);
                Point[] board = { point1, point2, point3, point4, point5, point6 };
                g.DrawPolygon(pen, board);
                Point point7 = new Point((int)_startPosX + 85, (int)_startPosY + 10);
                Point point8 = new Point((int)_startPosX + 90, (int)_startPosY + 15);
                Point point9 = new Point((int)_startPosX + 95, (int)_startPosY + 10);
                Point point10 = new Point((int)_startPosX + 95, (int)_startPosY + 40);
                Point point11 = new Point((int)_startPosX + 90, (int)_startPosY + 35);
                Point point12 = new Point((int)_startPosX + 85, (int)_startPosY + 40);
                Point[] board1 = { point7, point8, point9, point10, point11, point12 };
                g.DrawPolygon(pen, board1);
            }
        }
        public override string ToString()
        {
            return $"{base.ToString()}{separator}{DopColor.Name}" +
                   $"{separator}{Star}{separator}{Bomb}" +
                   $"{separator}{Rocket}";
        }
    }
}
