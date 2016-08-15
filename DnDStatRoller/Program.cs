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
}
