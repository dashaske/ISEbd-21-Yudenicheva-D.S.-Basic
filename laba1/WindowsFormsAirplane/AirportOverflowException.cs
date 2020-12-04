using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAirplane
{
    class AirportOverflowException : Exception
    {
        public AirportOverflowException() : base("В аэропорте нет свободных мест")
        { }
    }
}
