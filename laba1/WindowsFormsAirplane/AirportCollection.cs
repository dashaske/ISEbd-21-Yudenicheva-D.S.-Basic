using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAirplane
{ //Класс-коллекция парковок
    class AirportCollection
    {
        //Словарь (хранилище) с парковками
        readonly Dictionary<string, Airport<Plane>> airportStages;

        //Возвращение списка названий праковок
        public List<string> Keys => airportStages.Keys.ToList();

        private readonly int pictureWidth;

        private readonly int pictureHeight;

        public AirportCollection(int pictureWidth, int pictureHeight)
        {
            airportStages = new Dictionary<string, Airport<Plane>>();
            this.pictureWidth = pictureWidth;
            this.pictureHeight = pictureHeight;
        }

        //Добавление парковки
        public void AddAirport(string name)
        {
            if (airportStages.ContainsKey(name))
            {
                return;
            }
            airportStages.Add(name, new Airport<Plane>(pictureWidth, pictureHeight));
        }

        //Удаление парковки
        public void DelAirport(string name)
        {
            if (airportStages.ContainsKey(name))
            {
                airportStages.Remove(name);
            }
        }

        //Доступ к парковке
        public Airport<Plane> this[string ind]
        {
            get
            {
                if (airportStages.ContainsKey(ind))
                {
                    return airportStages[ind];
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
