using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF_Zestaw4_z2.ArenaDuelGame
{
    public static class AttemptEvaluator
    {
        public static Random Rng { get; private set; }

        static AttemptEvaluator()
        {
            Rng = new Random();
        }

        public static bool Succeeded(double chance)
        {
            return (Rng.Next(1, 100001) <= (int)(chance * 1000 + 0.5));
        }
    }
}
