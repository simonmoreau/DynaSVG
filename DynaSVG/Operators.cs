using Autodesk.DesignScript.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynaSVG
{
    /// <summary>
    /// Extension methods class
    /// </summary>
    public static class Operators
    {
        /// <summary>
        /// Substract a point to another
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Point Minus(this Point a, Point b)
        {
            Point point = Point.ByCoordinates(a.X - b.X, a.Y - b.Y);

            return point;
        }
    }
}
