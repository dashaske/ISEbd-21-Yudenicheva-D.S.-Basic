using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAirplane 
{
    class AirportOccupiedPlaceException : Exception
    {
        public AirportOccupiedPlaceException() : base("Не удалось загрузить аэропорт")
        { }
    }
}
