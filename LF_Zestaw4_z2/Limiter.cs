using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF_Zestaw4_z2
{
    public static class Limiter
    {
        public static T AtLeast<T>(T min, T val)
            where T : IComparable
        {
            return (val.CompareTo(min) < 0) ? min : val;
        }

        public static T AtMost<T>(T max, T val)
            where T : IComparable
        {
            return (val.CompareTo(max) > 0) ? max : val;
        }

        public static T Between<T>(T min, T max, T val)
            where T : IComparable
        {
            return (val.CompareTo(min) < 0) ? min :
                (val.CompareTo(max) > 0) ? max : val;
        }
    }
}
