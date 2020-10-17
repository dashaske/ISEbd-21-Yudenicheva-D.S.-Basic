using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAirplane
{
    public partial class WarPlane : Plane
    {
        /// <summary>
        /// Ширина отрисовки самолета
        /// </summary>
        protected readonly int planeWidth = 180;
        /// <summary>
        /// Ширина отрисовки самолета
        /// </summary>
        protected readonly int planeHeight = 60;

        public Color DopColor { protected set; get; }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="maxSpeed">Максимальная скорость</param>
        /// <param name="weight">Вес самолета</param>
        /// <param name="mainColor">Основной цвет</param>
        public WarPlane(int maxSpeed, float weight, Color mainColor, Color dopColor)
        {
            MaxSpeed = maxSpeed;
            Weight = weight;
            MainColor = mainColor;
            DopColor = dopColor;

        }

        //Конструктор с изменением размеров самолета
        protected WarPlane(int maxSpeed, float weight, Color mainColor, int planeWidth, int planeHeight)
        {
            MaxSpeed = maxSpeed;
            Weight = weight;
            MainColor = mainColor;
            this.planeWidth = planeWidth;
            this.planeHeight = planeHeight;
        }

        public override void MoveTransport(Direction direction)
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

            //границы самолета
            Brush brGray = new SolidBrush(MainColor);
            g.FillEllipse(brGray, _startPosX + 80, _startPosY + 40, 90, 20);
            g.DrawEllipse(pen, _startPosX + 80, _startPosY + 40, 90, 20);   //нос

            g.DrawRectangle(pen, _startPosX, _startPosY + 40, 130, 20);
            g.DrawRectangle(pen, _startPosX + 75, _startPosY - 10, 25, 120);   //границы самолета

            Brush brRed = new SolidBrush(MainColor);
            g.FillRectangle(brRed, _startPosX, _startPosY + 40, 130, 20);    //закрас тела

            Brush brGreen = new SolidBrush(MainColor);
            g.FillRectangle(brGreen, _startPosX + 75, _startPosY - 10, 25, 120);
            g.DrawRectangle(pen, _startPosX + 5, _startPosY + 25, 15, 50);           //тело
            g.FillRectangle(brGreen, _startPosX + 5, _startPosY + 25, 15, 50);

            Brush brBrown = new SolidBrush(MainColor);
            g.FillEllipse(brBrown, _startPosX + 95, _startPosY, 10, 30);
            g.DrawEllipse(pen, _startPosX + 95, _startPosY, 10, 30);
            g.FillEllipse(brBrown, _startPosX + 95, _startPosY + 70, 10, 30);     //круги около бомб
            g.DrawEllipse(pen, _startPosX + 95, _startPosY + 70, 10, 30);

            Brush brBlue = new SolidBrush(MainColor);
            g.FillEllipse(brBlue, _startPosX + 70, _startPosY + 42, 30, 15);           //центральный круг
            g.DrawEllipse(pen, _startPosX + 70, _startPosY + 42, 30, 15);
        }
    }
}
