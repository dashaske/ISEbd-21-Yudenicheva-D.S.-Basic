using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WindowsFormsAirplane
{ //Класс-коллекция парковок
    class AirportCollection
    {
        //Словарь (хранилище) с парковками
        readonly Dictionary<string, Airport<Plane>> airportStages;

        //Возвращение списка названий праковок
        public List<string> Keys => airportStages.Keys.ToList();

        private readonly char separator = ':';

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
        public void SaveData(string filename)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.Write($"AirportCollection");

                    foreach (var level in airportStages)
                    {
                        //Начинаем парковку
                        sw.WriteLine($"Airport{separator}{level.Key}");
                        foreach (ITransport bomber in level.Value)
                        {
                            if (bomber.GetType().Name == "WarPlane")
                            {
                                sw.Write($"WarPlane{separator}");
                            }
                            if (bomber.GetType().Name == "Bomber")
                            {
                                sw.Write($"Bomber{separator}");
                            }
                            //Записываемые параметры
                            sw.WriteLine(bomber);
                        }
                    }
                }
            }
        }
        public void LoadData(string filename)
        {
            {
                if (!File.Exists(filename))
                {
                    throw new FileNotFoundException();
                }
                using (StreamReader sr = new StreamReader(filename))
                {
                    string line = sr.ReadLine();

                    if (line.Contains("AirportCollection"))
                    {
                        airportStages.Clear();
                    }
                    else
                    {
                        throw new FormatException("Неверный формат файла");
                    }
                    line = sr.ReadLine();
                    Plane _plane = null;
                    string key = string.Empty;
                    while (line != null && line.Contains("Airport"))
                    {
                        key = line.Split(separator)[1];
                        airportStages.Add(key, new Airport<Plane>(pictureWidth, pictureHeight));
                        line = sr.ReadLine();

                        while (line != null && (line.Contains("WarPlane") || line.Contains("Bomber")))
                        {
                            if (line.Split(separator)[0] == "WarPlane")
                            {
                                _plane = new WarPlane(line.Split(separator)[1]);
                            }
                            else if (line.Split(separator)[0] == "Bomber")
                            {
                                _plane = new Bomber(line.Split(separator)[1]);
                            }
                            var result = airportStages[key] + _plane;
                            if (!result)
                            {
                                throw new NullReferenceException();
                            }
                            line = sr.ReadLine();
                        }
                    }
                }
            }
        }
    }
}