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
            string optionMessage = "-g [raw,std,] type exit to quit";

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
                        int[] test = statGenerator.Generate(6, 6);
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

            
            /*
            Console.Write("Raw Roll:");

            Console.Write(string.Join(",", arr) + Environment.NewLine);

            arr.Remove(arr.Min());

            Console.Write("Modified Roll:");
            int count = 0;
            arr.ForEach(x => { Console.Write(x); count += x; });
            Console.WriteLine("Total:" + count.ToString());*/
        }
    }

    public class StatGenerator
    {
        public StatGenerator() 
        {
            randomObject = new Random(Guid.NewGuid().GetHashCode());
        }

        public StatGenerator(int seed)
        {
            randomObject = new Random(seed);
        }

        private Random randomObject;

        public int[] Generate(int numStats, int diceType)
        {
            int[] stats = new int[numStats];
            for (int i = 0; i < numStats; i++)
            {
                int[] tmp = new int[4];
                tmp[0] = randomObject.Next(1, diceType);
                tmp[1] = randomObject.Next(1, diceType);
                tmp[2] = randomObject.Next(1, diceType);
                tmp[3] = randomObject.Next(1, diceType);

                stats[i] = tmp.OrderBy(x => x).Skip(1).Sum();
            }
            return stats;
        }
    }
}
