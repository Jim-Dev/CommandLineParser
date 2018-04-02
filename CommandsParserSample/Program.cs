using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CmdParser;
namespace CommandsParserSample
{
    class Program
    {
        public static bool isRunning;
        static void Main(string[] args)
        {
            CommandsParser cmdParser = new CommandsParser();
            cmdParser.AddCommand(new CloseCommand());

            isRunning = true;

            while (isRunning)
            {
                cmdParser.Execute(Console.ReadLine());
                Console.Write(cmdParser.LastExecutedOutput);
            }
        }
    }

    public class CloseCommand : BaseCommand
    {
        public CloseCommand()
           : base("close",
                 "Close the application",
                 new List<string>() {"exit","shutdown"})
        { }

        public override string[] Help
        {
            get
            {
                return new string[] {
                    "Close the console application"};
            }
        }

        public override string Execute(string[] arguments)
        {
            Program.isRunning = false;
            return string.Empty;
        }
    }
}