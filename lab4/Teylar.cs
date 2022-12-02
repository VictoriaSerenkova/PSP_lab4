using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace lab4
{
    class Teylar
    {
        double x;

        public Teylar(string text)
        {
            this.x = Convert.ToDouble(text);
        }

        public string Start()
        {
            double e = 0.001;
            if (x == 0.0)
            {
                return "0";
            }
            if (x < 0.0)
                x = -x;
            double t = x - 1;
            double u = t;
            double lnabsx = u;
            int n = 1;
            do
            {
                n++;
                u *= -((n - 1) * t) / n;
                lnabsx += u;
            } while (u > e || u < -e);
            return lnabsx.ToString();
        }
    }
}
