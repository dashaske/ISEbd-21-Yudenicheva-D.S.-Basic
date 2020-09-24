using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAirplane
{
    class Bomber
    { 
        
        private float _startPosX;
       
        private float _startPosY;
      
        private int _pictureWidth;
        
        private int _pictureHeight;
        
        /// Ширина отрисовки самолета
        private const int carWidth = 100;
     
        private const int carHeight = 60;
        public int MaxSpeed { private set; get; }
       
        /// Вес самолета
        public float Weight { private set; get; }
        
        /// Основной цвет кузова
      
        public Color MainColor { private set; get; }
       
        /// Дополнительный цвет
        public Color DopColor { private set; get; }
        
        /// Признак наличия звезды
        public bool Star { private set; get; }
        
        /// Признак наличия бомб
        public bool Bomb { private set; get; }
        
        /// Признак наличия ракет
        public bool Rocket { private set; get; }


        
        /// <param name="maxSpeed">Максимальная скорость</param>
        /// <param name="weight">Вес автомобиля</param>
        /// <param name="mainColor">Основной цвет кузова</param>
        /// <param name="dopColor">Дополнительный цвет</param>
        /// <param name="star">Признак наличия звезды</param>
        /// <param name="bomb">Признак наличия бомб</param>
        /// <param name="rocket">Признак наличия ракет</param>
        public Bomber(int maxSpeed, float weight, Color mainColor, Color dopColor,
       bool star, bool bomb, bool rocket)
        {
            MaxSpeed = maxSpeed;
            Weight = weight;
            MainColor = mainColor;
            DopColor = dopColor;
            Star = star;
            Bomb = bomb;
            Rocket = rocket;
        }
        
        public void SetPosition(int x, int y, int width, int height)
        {
            _startPosX = x + 50;
            _startPosY = y + 50;
            _pictureWidth = width;
            _pictureHeight = height;
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
                    if (_startPosX + step < _pictureWidth - carWidth)
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
                    if (_startPosY + step < _pictureHeight - carHeight)
                    {
                        _startPosY += step;
                    }
                    break;
            }
        }
        

        public void DrawAirplane(Graphics g)
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
            // теперь отрисуем фюзеляж самолета
            //границы самолета
            Brush brGray = new SolidBrush(MainColor);
            g.FillEllipse(brGray, _startPosX + 80, _startPosY + 40, 90, 20);
            g.DrawEllipse(pen, _startPosX + 80, _startPosY + 40, 90, 20);

            g.DrawRectangle(pen, _startPosX, _startPosY + 40, 130, 20);
            g.DrawRectangle(pen, _startPosX + 75, _startPosY - 10, 25, 120);
            Brush brRed = new SolidBrush(MainColor);
            g.FillRectangle(brRed, _startPosX, _startPosY + 40, 130, 20);
            Brush brGreen = new SolidBrush(MainColor);
            g.FillRectangle(brGreen, _startPosX + 75, _startPosY - 10, 25, 120);
            g.DrawRectangle(pen, _startPosX + 5, _startPosY + 25, 15, 50);
            g.FillRectangle(brGreen, _startPosX + 5, _startPosY + 25, 15, 50);

            Brush brBrown = new SolidBrush(MainColor);
            g.FillEllipse(brBrown, _startPosX + 95, _startPosY, 10, 30);
            g.DrawEllipse(pen, _startPosX + 95, _startPosY, 10, 30);
            g.FillEllipse(brBrown, _startPosX + 95, _startPosY + 70, 10, 30);
            g.DrawEllipse(pen, _startPosX + 95, _startPosY + 70, 10, 30);
            Brush brBlue = new SolidBrush(MainColor);
            g.FillEllipse(brBlue, _startPosX + 70, _startPosY + 42, 30, 15);
            g.DrawEllipse(pen, _startPosX + 70, _startPosY + 42, 30, 15);
            
            
            
            if (Star)
            {
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

    }
}
