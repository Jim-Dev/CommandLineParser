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

            cmdParser.AddCommand(new ClearConsoleCommand());
            cmdParser.AddCommand(new CloseCommand());

            cmdParser.OnOutputAvailable += StdOutput_OnOutputAvailable;

            isRunning = true;

            while (isRunning)
            {
                cmdParser.Execute(Console.ReadLine());
            }
        }

        private static void StdOutput_OnOutputAvailable(object sender, CommandsParser.Events.OutputAvailableEventArgs e)
        {
            Console.WriteLine(e.Output);
        }
    }

    
}