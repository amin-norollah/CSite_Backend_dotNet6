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

        /// <summary>
        /// Calculates the average of a list of double values.
        /// </summary>
        /// <param name="numbers">The list of numbers to calculate the average of.</param>
        /// <returns>The average of the values in the input list.</returns>
        /// <exception cref="ArgumentException">Thrown when the input list is null or empty.</exception>
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
