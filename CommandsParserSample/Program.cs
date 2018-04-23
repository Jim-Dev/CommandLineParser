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
            ClearConsoleCommand clsCommand = new ClearConsoleCommand(cmdParser);
            cmdParser.AddCommand(clsCommand);
            cmdParser.AddCommand(new CloseCommand(cmdParser));

            cmdParser.OnOutputAvailable += StdOutput_OnOutputAvailable;
            clsCommand.CommandExecuted += ClsCommand_CommandExecuted;

            isRunning = true;

            while (isRunning)
            {
                string input = Console.ReadLine();
                cmdParser.Execute(input);
            }
        }

        private static void ClsCommand_CommandExecuted(object sender, CommandsParser.Events.CommandExecutedEventArgs e)
        {
            Console.WriteLine("output cleared!!!");
        }

        private static void StdOutput_OnOutputAvailable(object sender, CommandsParser.Events.OutputAvailableEventArgs e)
        {
            if (e.Output.Trim() != CmdParser.COMMAND_START_OUTPUT && e.Output.Trim() != CmdParser.COMMAND_FINISH_OUTPUT)
                Console.Write(e.Output);
        }
    }

    
}