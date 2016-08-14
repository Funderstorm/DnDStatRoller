using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DnDStatRollerProgram
{
    class DnDStatRollerProgram
    {
        static void Main(string[] args)
        {
            string input = String.Empty;

            string welcomeMessage = "DnD 5th Edition Stat Generator";
            string optionMessage = "-g [raw,std,] -normalize" + Environment.NewLine + "Type exit, q, or quit to quit";

            Console.WriteLine(welcomeMessage);
            Console.WriteLine(optionMessage);

            var statGenerator = new StatGenerator();
            bool execute = true;
            while (execute)
            {
                input = Console.ReadLine();

                //using a switch for now
                switch (input)
                {
                    case "1":
                        List<int> test = statGenerator.RollStats(6, statGenerator.DefaultNormalize);
                        Console.WriteLine("Output:" + string.Join(",", test));
                        break;
                    case "exit":
                    case "q":
                    case "quit":
                        execute = false;
                        break;
                    default:
                        Console.WriteLine("Unrecognized selection.");
                        break;
                }

            }
        }
    }

    public class StatGenerator
    {
        public StatGenerator() 
        {
            randomObject = new Random(Guid.NewGuid().GetHashCode());
            minimumAcceptableStatSum = 65;
            minimumAcceptableIndividualStat = 9;
            diceType = 6;
        }

        public StatGenerator(int seed)
        {
            randomObject = new Random(seed);
            minimumAcceptableStatSum = 65;
            minimumAcceptableIndividualStat = 9;
            diceType = 6;
        }

        private int minimumAcceptableStatSum;
        private int minimumAcceptableIndividualStat;
        private int negativeModifierMaximum;

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

            //check for # of negative stats (8 or lower)

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
    }
}
