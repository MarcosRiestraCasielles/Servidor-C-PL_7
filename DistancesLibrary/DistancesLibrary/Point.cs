using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistancesLibrary
{
    public class Point
    {
        private double _x;
        private double _y;

        // Propiedad para acceder a x
        // Equivalente a Getters y Setters
        public double x
        {
            get { return _x; }
            set { _x = value; }
        }

        public double y
        {
            get { return _y; }
            set { _y = value; }
        }

        public Point(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public Point ()
        { 
        
        }

        public static bool Distance(Point p1, string lat, string lon, string criteria)
        {
            int r = 6371000;
            double lat1 = p1._x;
            double lat2 = Convert.ToDouble(lat);
            double long1 = p1._y;
            double long2 = Convert.ToDouble(lon);
            double c = Math.PI / 180;
            double _criteria = Convert.ToDouble(criteria);
            return ( 2 * r * Math.Asin(Math.Sqrt(Math.Sin((c * (lat2 - lat1) / 2) * (c * (lat2 - lat1) / 2)) + Math.Cos(c * lat1) * Math.Cos(c * lat2) * Math.Sin((c * (long2 - long1) / 2) * (c * (long2 - long1) / 2))))<_criteria);
        }
    }
}
