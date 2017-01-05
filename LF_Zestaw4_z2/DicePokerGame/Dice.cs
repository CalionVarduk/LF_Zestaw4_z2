using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF_Zestaw4_z2.DicePokerGame
{
    public class Dice
    {
        private byte[] values;
        private bool[] locks;

        public bool CanLock { get; set; }
        public int Count { get { return values.Length; } }

        public Rank Rank
        {
            get
            {
                byte[] counts = new byte[6];
                for (int i = 0; i < Count; ++i)
                    ++counts[values[i] - 1];

                int[] iPairs = new int[] { -1, -1 };
                int pairCount = 0;
                int iTriple = -1;
                int iQuadruple = -1;
                int iQuintuple = -1;

                for (int i = 0; i < counts.Length; ++i)
                {
                    switch (counts[i])
                    {
                        case 2: iPairs[pairCount++] = i; break;
                        case 3: iTriple = i; break;
                        case 4: iQuadruple = i; break;
                        case 5: iQuintuple = i; break;
                    }
                }

                if (iQuintuple != -1) return QuintupleRank(iQuintuple);
                if (iQuadruple != -1) return QuadrupleRank(iQuadruple);
                if (iTriple != -1)
                {
                    if (pairCount == 1) return TripleAndPairRank(iTriple, iPairs[0]);
                    return TripleRank(iTriple);
                }
                if (pairCount == 2) return DoublePairRank(iPairs[0], iPairs[1]);
                if (pairCount == 1) return PairRank(iPairs[0]);
                return StraightOrNorthingRank(counts);
            }
        }

        public string RankString
        {
            get
            {
                Rank r = Rank;
                if (r == Rank.Nothing) return "Nothing";
                if (r <= Rank.PairSix) return "Pair";
                if (r <= Rank.DoublePairSixFive) return "Two Pairs";
                if (r <= Rank.TripleSix) return "Triple";
                if (r == Rank.FiveHighStraight) return "Five High Straight";
                if (r == Rank.SixHighStraight) return "Six High Straight";
                if (r <= Rank.TripleSixPairFive) return "Full House";
                if (r <= Rank.QuadrupleSix) return "Quadruple";
                return "Quintuple";
            }
        }

        public Dice()
        {
            CanLock = false;
            values = new byte[] { 1, 1, 1, 1, 1 };
            locks = new bool[5];
        }

        public byte Value(int i)
        {
            return values[i];
        }

        public bool IsLocked(int i)
        {
            return locks[i];
        }

        public void Lock(int i)
        {
            if(CanLock)
                locks[i] = true;
        }

        public void Unlock(int i)
        {
            if(CanLock)
                locks[i] = false;
        }

        public void UnlockAll()
        {
            if (CanLock)
                for (int i = 0; i < Count; ++i)
                    locks[i] = false;
        }

        public void Roll(Random rng)
        {
            for (int i = 0; i < Count; ++i)
                if (!locks[i]) values[i] = (byte)rng.Next(1, 7);
        }

        private Rank QuintupleRank(int index)
        {
            return (Rank)(Rank.QuintupleOne + index);
        }

        private Rank QuadrupleRank(int index)
        {
            return (Rank)(Rank.QuadrupleOne + index);
        }

        private Rank TripleAndPairRank(int tripleIndex, int pairIndex)
        {
            Rank rank = (Rank)(Rank.TripleOnePairTwo + tripleIndex * 5);
            if (pairIndex > tripleIndex) --pairIndex;
            return (Rank)(rank + pairIndex);
        }

        private Rank TripleRank(int index)
        {
            return (Rank)(Rank.TripleOne + index);
        }

        private Rank DoublePairRank(int index1, int index2)
        {
            if (index1 == 0)
            {
                if (index2 == 1) return Rank.DoublePairTwoOne;
                if (index2 == 2) return Rank.DoublePairThreeOne;
                if (index2 == 3) return Rank.DoublePairFourOne;
                if (index2 == 4) return Rank.DoublePairFiveOne;
                return Rank.DoublePairSixOne;
            }
            if (index1 == 1)
            {
                if (index2 == 2) return Rank.DoublePairThreeTwo;
                if (index2 == 3) return Rank.DoublePairFourTwo;
                if (index2 == 4) return Rank.DoublePairFiveTwo;
                return Rank.DoublePairSixTwo;
            }
            if (index1 == 2)
            {
                if (index2 == 3) return Rank.DoublePairFourThree;
                if (index2 == 4) return Rank.DoublePairFiveThree;
                return Rank.DoublePairSixThree;
            }
            if (index1 == 3)
            {
                if (index2 == 4) return Rank.DoublePairFiveFour;
                return Rank.DoublePairSixFour;
            }
            return Rank.DoublePairSixFive;
        }

        private Rank PairRank(int index)
        {
            return (Rank)(Rank.PairOne + index);
        }

        private Rank StraightOrNorthingRank(byte[] counts)
        {
            if (counts[0] > 0)
            {
                for (int i = 1; i < 5; ++i)
                    if (counts[i] == 0) return Rank.Nothing;
                return Rank.FiveHighStraight;
            }

            if (counts[1] > 0)
            {
                for (int i = 2; i < 6; ++i)
                    if (counts[i] == 0) return Rank.Nothing;
                return Rank.SixHighStraight;
            }
            return Rank.Nothing;
        }
    }
}
