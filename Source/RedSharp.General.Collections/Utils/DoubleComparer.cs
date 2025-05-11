using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedSharp.General.Collections.Abstracts;
using RedSharp.General.Helpers;

namespace RedSharp.General.Collections.Utils
{
    public class DoubleComparer : ComparerBase<double>
    {
        /// <summary>
        /// Default instance of the class with <see cref="double.Epsilon/> and ascending order.
        /// </summary>
        public static readonly DoubleComparer Ascending;

        /// <summary>
        /// Default instance of the class with <see cref="double.Epsilon"/> and descending order.
        /// </summary>
        public static readonly DoubleComparer Descending;

        static DoubleComparer()
        {
            Ascending = new DoubleComparer(double.Epsilon, true);
            Descending = new DoubleComparer(double.Epsilon, false);
        }

        public DoubleComparer(double approximationValue, bool isAscending = true) : base(isAscending)
        {
            ApproximationValue = approximationValue;
        }

        public double ApproximationValue { get; private set; }

        protected override int InternalCompare(double first, double second)
        {
            if (Math.Abs(first - second) < ApproximationValue)
                return 0;
            else if (first > second)
                return 1;
            else
                return -1;
        }
    }
}
