using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryMethods
{
    public static class Geometry
    {
        public static double FindDistanceBetweenTwoPoints(double x1, double y1, double x2, double y2)
        {
            /*
             * a^2+b^2=c^2
             */
           double part1 = Math.Pow(x1 - x2,2) + Math.Pow(y1 - y2,2);
            return Math.Sqrt(part1);
        }

        #region Circle methods
        public static double[] FindPointOnCircleX(double r, double centerX, double centerY, double y)
        {
            /*
             * a^2 + b^2 = c^2
             * (centerX-x)^2+(centerY-y)^2 = r^2
             * (centerX-x)^2 = r^2 - (centerY-y)^2
             * centerX - x = (+-1)*SQRT(r^2 - centerY-y)^2
             * -x = (+-1)*SQRT(r^2 - centerY-y)^2 - centerX
             * x = ((+-1)*SQRT(r^2 - centerY-y)^2 - centerX)*-1
             */
            double[] x = new double[2];
            double part2 = Math.Sqrt(r * r - Math.Pow(centerY - y, 2));
            x[0] = (part2 - centerX) * -1;
            x[1] = (-1*part2 - centerX) * -1;
            return x;
        }

        public static double[] FindPointOnCircleY(double r, double centerX, double centerY, double x)
        {
            /*
             * a^2 + b^2 = c^2
             * (centerX-x)^2+(centerY-y)^2 = r^2
             * (centerY-y)^2 = r^2 - (centerX-x)^2
             * centerY - y = (+-1)*SQRT(r^2 - centerX-x)^2
             * -y = (+-1)*SQRT(r^2 - centerX-x)^2 - centerY
             * y = ((+-1)*SQRT(r^2 - centerX-x)^2 - centerY)*-1
             */
            double[] y = new double[2];
            double part2 = Math.Sqrt(r * r - Math.Pow(centerX - x, 2));
            y[0] = (part2 - centerY) * -1;
            y[1] = (-1 * part2 - centerY) * -1;
            return y;
        }
        #endregion Circle methods
    }
}
