﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsAirplane
{// Параметризованный класс для хранения набора объектов от интерфейса ITransport
    public class Airport<T> : IEnumerator<T>, IEnumerable<T>
        where T : class, ITransport
    {
        //Массив объектов
        private readonly List<T> _places;
       
        private readonly int _maxCount;

        private readonly int pictureWidth;

        private readonly int pictureHeight;

        private readonly int _placeSizeWidth = 210;

        private readonly int _placeSizeHeight = 133;

        private int _currentIndex;
        public T Current => _places[_currentIndex];
        object IEnumerator.Current => _places[_currentIndex];

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
                throw new AirportOverflowException();
            }
            if (p._places.Contains(bomber))
            {
                throw new AirportAlreadyHaveException();
            }
            p._places.Add(bomber);
            return true;
        }
        public static T operator -(Airport<T> p, int index)
        {
            if (index >= p._places.Count)
            {
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
        public void Sort() => _places.Sort((IComparer<T>)new PlaneComparer());

        public void Dispose() { }

        public bool MoveNext()
        {
            _currentIndex++;
            return _currentIndex < _places.Count;
        }

        public void Reset()
        {
            _currentIndex = -1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }
    }
}
