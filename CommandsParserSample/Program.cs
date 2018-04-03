using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommandsParser;
namespace CommandsParserSample
{
    class Program
    {
        public static bool isRunning;
        static void Main(string[] args)
        {
            CmdParser cmdParser = new CmdParser();
            cmdParser.AddCommand(new CloseCommand());

            cmdParser.OnOutputAvailable += StdOutput_OnOutputAvailable;

            isRunning = true;

            while (isRunning)
            {
                cmdParser.Execute(Console.ReadLine());
                //Console.Write(cmdParser.LastExecutedOutput);
            }
        }

        private static void StdOutput_OnOutputAvailable(object sender, CommandsParser.Events.StdOutputAvailableEventArgs e)
        {
            Console.WriteLine(e.Output);
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