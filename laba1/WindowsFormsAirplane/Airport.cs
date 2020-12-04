using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using NLog;

namespace WindowsFormsAirplane
{// Параметризованный класс для хранения набора объектов от интерфейса ITransport
    public class Airport<T> where T : class, ITransport
    {
        //Массив объектов
        private readonly List<T> _places;

        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        // Максимальное количество мест на парковке
        private readonly int _maxCount;

        private readonly int pictureWidth;

        private readonly int pictureHeight;

        //Высота парковочного места
        private readonly int _placeSizeWidth = 210;

        //Ширина парковочного места
        private readonly int _placeSizeHeight = 133;

        public Airport(int picWidth, int picHeight)
        {
            int width = picWidth / _placeSizeWidth;
            int height = picHeight / _placeSizeHeight;
            _places = new List<T>();
            pictureWidth = picWidth;
            pictureHeight = picHeight;
            _maxCount = width * height;
        }
        public static bool operator +(Airport<T> p, T bomber)
        {
            if (p._places.Count >= p._maxCount)
            {
                p.logger.Warn("Вызвано исключение AirportOverflowException");
                throw new AirportOverflowException();
            }
            p._places.Add(bomber);
            return true;
        }
        public static T operator -(Airport<T> p, int index)
        {
            if (index >= p._places.Count)
            {
                p.logger.Warn("Вызвано исключение AirportNotFoundException");
                throw new AirportNotFoundException(index);
            }
            T bomber = p._places[index];
            p._places.RemoveAt(index);
            return bomber;
        }
        //Метод отрисовки парковки
        public void Draw(Graphics g)
        {
            DrawMarking(g);
            for (int i = 0; i < _places.Count; i++)
            {
                _places[i].SetPosition(4 + i / 4 * _placeSizeWidth + 4, i % 4 *
                   _placeSizeHeight + 15, pictureWidth, pictureHeight);
                _places[i]?.DrawFly(g);
            }
        }
        //Метод отрисовки разметки
        private void DrawMarking(Graphics g)
        {
            Pen pen = new Pen(Color.Black, 3);
            for (int i = 0; i < pictureWidth / _placeSizeWidth; i++)
            {
                for (int j = 0; j < pictureHeight / _placeSizeHeight + 1; ++j)
                {
                    g.DrawLine(pen, i * _placeSizeWidth, j * _placeSizeHeight, i *
                    _placeSizeWidth + _placeSizeWidth / 2, j * _placeSizeHeight);
                }
                g.DrawLine(pen, i * _placeSizeWidth, 0, i * _placeSizeWidth,
                (pictureHeight / _placeSizeHeight) * _placeSizeHeight);
            }
        }
        public T GetNext(int index)
        {
            if (index < 0 || index >= _places.Count)
            {
                return null;
            }
            return _places[index];
        }
    }
}
