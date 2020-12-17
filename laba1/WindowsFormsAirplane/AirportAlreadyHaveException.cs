using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAirplane
{
    public class AirportAlreadyHaveException : Exception
    {
        public AirportAlreadyHaveException() : base("В аэропорте уже есть такой самолет!") { }
    }
}
