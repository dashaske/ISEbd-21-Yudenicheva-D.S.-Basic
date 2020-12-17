using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAirplane
{
    public class PlaneComparer : IComparer<Plane>
    {
        public int Compare(Plane x, Plane y)
        {
            if (x is Bomber && y is Bomber)
            {
                return ComparerWarship((Bomber)x, (Bomber)y);
            }
            if (x is Bomber && y is WarPlane)
            {
                return -1;
            }
            if (x is WarPlane && y is Bomber)
            {
                return 1;
            }
            if (x is WarPlane && y is WarPlane)
            {
                return ComparerShip((WarPlane)x, (WarPlane)y);
            }
            return 0;
        }

        private int ComparerShip(WarPlane x, WarPlane y)
        {
            if (x.MaxSpeed != y.MaxSpeed)
            {
                return x.MaxSpeed.CompareTo(y.MaxSpeed);
            }
            if (x.Weight != y.Weight)
            {
                return x.Weight.CompareTo(y.Weight);
            }
            if (x.MainColor != y.MainColor)
            {
                return x.MainColor.Name.CompareTo(y.MainColor.Name);
            }
            return 0;
        }

        private int ComparerWarship(Bomber x, Bomber y)
        {
            var res = ComparerShip(x, y);
            if (res != 0)
            {
                return res;
            }
            if (x.DopColor != y.DopColor)
            {
                return x.DopColor.Name.CompareTo(y.DopColor.Name);
            }
            if (x.Star != y.Star)
            {
                return x.Star.CompareTo(y.Star);
            }
            if (x.Bomb != y.Bomb)
            {
                return x.Bomb.CompareTo(y.Bomb);
            }
            if (x.Rocket != y.Rocket)
            {
                return x.Rocket.CompareTo(y.Rocket);
            }
            return 0;
        }
    }
}
