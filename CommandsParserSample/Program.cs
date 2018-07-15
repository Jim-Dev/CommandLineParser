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
            TextCommand textCommand = new TextCommand(cmdParser);
            UppercaseCommand uppercaseCommand = new UppercaseCommand(cmdParser);
            TickTockCommand tickTockCommand = new TickTockCommand(cmdParser);

            cmdParser.AddCommand(clsCommand);
            cmdParser.AddCommand(new CloseCommand(cmdParser));
            cmdParser.AddCommand(textCommand);
            cmdParser.AddCommand(uppercaseCommand);
            cmdParser.AddCommand(tickTockCommand);

            cmdParser.OnOutputAvailable += StdOutput_OnOutputAvailable;
            clsCommand.CommandExecuted += ClsCommand_CommandExecuted;
            textCommand.CommandExecuted += TextCommand_CommandExecuted;
            uppercaseCommand.CommandExecuted += UppercaseCommand_CommandExecuted;
            tickTockCommand.CommandExecuted += TickTockCommand_CommandExecuted;

            isRunning = true;

            while (isRunning)
            {
                string input = Console.ReadLine();
                cmdParser.Execute(input);
            }
        }

        private static void TickTockCommand_CommandExecuted(object sender, CommandsParser.Events.CommandExecutedEventArgs e)
        {
            Console.WriteLine(e.Output);
        }

        private static void UppercaseCommand_CommandExecuted(object sender, CommandsParser.Events.CommandExecutedEventArgs e)
        {
            Console.Write(e.Output);
        }

        private static void TextCommand_CommandExecuted(object sender, CommandsParser.Events.CommandExecutedEventArgs e)
        {
            cmdParser.Execute(string.Format("upper {0}", e.Output));
            
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