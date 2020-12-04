using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NLog;

namespace WindowsFormsAirplane
{ //Класс-коллекция парковок
    class AirportCollection
    {
        //Словарь (хранилище) с парковками
        readonly Dictionary<string, Airport<ITransport>> airportStages;

        private readonly Logger logger;

        //Возвращение списка названий праковок
        public List<string> Keys => airportStages.Keys.ToList();

        private readonly char separator = ':';

        private readonly int pictureWidth;

        private readonly int pictureHeight;

        public AirportCollection(int pictureWidth, int pictureHeight)
        {
            airportStages = new Dictionary<string, Airport<ITransport>>();
            this.pictureWidth = pictureWidth;
            this.pictureHeight = pictureHeight;
            logger = LogManager.GetCurrentClassLogger();
        }
        //Добавление парковки
        public void AddAirport(string name)
        {
            if (airportStages.ContainsKey(name))
            {
                return;
            }
            airportStages.Add(name, new Airport<ITransport>(pictureWidth, pictureHeight));
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
        public Airport<ITransport> this[string ind]
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
        public bool SaveData(string filename)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            using (StreamWriter sw = new StreamWriter(filename, true))
            {
                sw.Write($"AirportCollection{Environment.NewLine}");
                foreach (var level in airportStages)
                {
                    //Начинаем парковку
                    sw.Write($"Airport{separator}{level.Key}{Environment.NewLine}");
                    ITransport plane = null;
                    for (int i = 0; (plane = level.Value.GetNext(i)) != null; i++)
                    {
                        if (plane != null)
                        {
                            //если место не пустое
                            //Записываем тип машины
                            if (plane.GetType().Name == "WarPlane")
                            {
                                sw.Write($"WarPlane{separator}");
                            }
                            if (plane.GetType().Name == "Bomber")
                            {
                                sw.Write($"Bomber{separator}");
                            }
                            //Записываемые параметры
                            sw.Write(plane + Environment.NewLine);
                        }
                    }
                }
            }
            return true;
        }
        public bool LoadData(string filename)
        {               
            if (!File.Exists(filename))
            {
                logger.Warn("Выбран неверный файл для загрузки");
                throw new FileNotFoundException();
            }
            using (StreamReader sr = new StreamReader(filename))
            {
                Plane _plane = null;
                string line = sr.ReadLine();
                string key = string.Empty;

                if (line.Contains("AirportCollection"))
                {
                    airportStages.Clear();
                }
                else
                {
                    logger.Warn("Выбран неверный формат файла");
                    throw new FormatException("Неверный формат файла");
                }
                line = sr.ReadLine();
                while (line != null)
                {
                    if (line.Contains("Airport"))
                    {
                        key = line.Split(separator)[1];
                        airportStages.Add(key, new Airport<ITransport>(pictureWidth, pictureHeight));
                        line = sr.ReadLine();
                        continue;
                    }
                    if (string.IsNullOrEmpty(line))
                    {
                        line = sr.ReadLine();
                        continue;
                    }
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
                        logger.Warn("При загрузке файла вызвано исключение AirportOccupiedPlaceException");
                        throw new AirportOccupiedPlaceException();
                    }
                    line = sr.ReadLine();
                }
                return true;
            }          
        }
    }
}
