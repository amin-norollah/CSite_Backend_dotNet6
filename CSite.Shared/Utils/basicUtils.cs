using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitActivity.utils
{
    public class basicUtils
    {
        //adding new functions that can used in our other projects
        public basicUtils() { }

        public double CalculateAverage(List<double> numbers)
{
    if (numbers == null || numbers.Count == 0)
    {
        throw new ArgumentException("Cannot calculate average of empty list");
    }

    double sum = 0;

    foreach (double num in numbers)
    {
        sum += num;
    }

    return sum / numbers.Count;
}

 /**/
    }
}
