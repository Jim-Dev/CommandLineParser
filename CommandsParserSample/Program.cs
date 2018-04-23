using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommandsParser;
using CommandsParserSample.Commands;

namespace CommandsParserSample
{
    class Program
    {
        public static bool isRunning;
        static CmdParser cmdParser = new CmdParser();
        static void Main(string[] args)
        {

            cmdParser.AddCommand(new ClearConsoleCommand(cmdParser));
            cmdParser.AddCommand(new CloseCommand(cmdParser));

            cmdParser.OnOutputAvailable += StdOutput_OnOutputAvailable;

            isRunning = true;

            while (isRunning)
            {
                string input = Console.ReadLine();
                cmdParser.Execute(input);
            }
        }

        private static void StdOutput_OnOutputAvailable(object sender, CommandsParser.Events.OutputAvailableEventArgs e)
        {
            Console.WriteLine(e.Output);
        }
    }

    
}