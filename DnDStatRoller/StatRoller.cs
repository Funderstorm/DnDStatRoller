using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDStatRollerProgram
{
    public class StatGenerator
    {
        public StatGenerator()
        {
            randomObject = new Random(Guid.NewGuid().GetHashCode());
            minimumAcceptableStatSum = 65;
            minimumAcceptableIndividualStat = 9;
            maximumAcceptableIndividualStat = int.MaxValue;
            NumberOfAcceptableNegativeModifiers = 2;
            NumberOfAcceptablePositiveModifiers = int.MaxValue;
            diceType = 6;
        }

        public StatGenerator(int seed)
        {
            randomObject = new Random(seed);
            minimumAcceptableStatSum = 65;
            minimumAcceptableIndividualStat = 9;
            maximumAcceptableIndividualStat = int.MaxValue;
            NumberOfAcceptableNegativeModifiers = 2;
            NumberOfAcceptablePositiveModifiers = int.MaxValue;
            diceType = 6;
        }

        private int minimumAcceptableStatSum;
        private int minimumAcceptableIndividualStat;
        private int maximumAcceptableIndividualStat;
        private int NumberOfAcceptableNegativeModifiers;
        private int NumberOfAcceptablePositiveModifiers;

        private Random randomObject;
        private int diceType = 6;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numStats"></param>
        /// <returns></returns>
        public List<int> RollStats(int numStats, Func<List<List<int>>, List<int>> normalizationFunction)
        {
            List<int> normalizedStats;
            do
            {
                var stats = Generate(numStats);

                normalizedStats = normalizationFunction(stats);
            }
            while (!Analyze(normalizedStats));

            return normalizedStats;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numStats"></param>
        /// <param name="diceType"></param>
        /// <returns></returns>
        private List<List<int>> Generate(int numStats)
        {
            var stats = new List<List<int>>();
            for (int i = 0; i < numStats; i++)
            {
                var tmp = new List<int>();
                tmp.Add(randomObject.Next(1, diceType));
                tmp.Add(randomObject.Next(1, diceType));
                tmp.Add(randomObject.Next(1, diceType));

                stats.Add(tmp);
            }
            return stats;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stats"></param>
        /// <returns></returns>
        private bool Analyze(List<int> stats)
        {
            if (stats.Sum() < minimumAcceptableStatSum)
                return false;

            if (stats.Min() < minimumAcceptableIndividualStat)
                return false;

            if (stats.Max() > maximumAcceptableIndividualStat)
                return false;

            if (stats.Count(x => x < 10) > NumberOfAcceptableNegativeModifiers)
                return false;

            if (stats.Count(x => x > 11) > NumberOfAcceptablePositiveModifiers)
                return false;

            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="stats"></param>
        /// <returns></returns>
        public List<int> DefaultNormalize(List<List<int>> stats)
        {
            var newStats = new List<int>();

            foreach (List<int> stat in stats)
            {
                stat.Add(randomObject.Next(1, diceType));
                newStats.Add(stat.OrderBy(x => x).Skip(1).Sum());
            }

            return newStats;
        }

        public List<int> DefaultPlusNormalize(List<List<int>> stats)
        {
            var newStats = new List<int>();

            foreach (List<int> stat in stats)
            {
                stat.Add(randomObject.Next(1, diceType));
                stat.Add(randomObject.Next(1, diceType));
                newStats.Add(stat.OrderBy(x => x).Skip(2).Sum());
            }

            return newStats;
        }

        public List<int> HeroicNormalize(List<List<int>> stats)
        {
            var newStats = new List<int>();

            foreach (List<int> stat in stats)
            {
                stat[0] = diceType;
                newStats.Add(stat.Sum());
            }

            return newStats;
        }

        public List<int> Roll12Take6Normalize(List<List<int>> stats)
        {
            var newStats = new List<int>();

            var secondStats = Generate(6);

            foreach (List<int> stat in stats.Concat(secondStats))
            {
                newStats.Add(stat.Sum());
            }

            return newStats.OrderByDescending(x => x).Take(6).ToList();
        }

        public List<int> IronmanNormalize(List<List<int>> stats)
        {
            var newStats = new List<int>();

            foreach (List<int> stat in stats)
            {
                newStats.Add(stat.Sum());
            }

            return newStats;
        }
    }
}
