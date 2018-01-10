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
        static bool isRunning;
        static void Main(string[] args)
        {
            CommandsParser cmdParser = new CommandsParser();
            /*
            cmdParser.AddCommand("close", delegate (object sender, CommandEventArgs e)
            {
                isRunning = false;
            }, "Close the application");
            cmdParser.AddCommand("test", delegate (object sender, CommandEventArgs e)
            {
                e.CommandOutput.AppendLine("TEST COMMAND OUTPUT");
            }, "Test command");
            */
          
            isRunning = true;

            while (isRunning)
            {
                cmdParser.Execute(Console.ReadLine());
                Console.Write(cmdParser.LastExecutedOutput);
            }
        }
    }
    public class TestCommand : BaseCommand
    {
        public TestCommand()
            : base("test")
        {
        }

        public override string Execute(string[] arguments)
        {
            Console.WriteLine("TEST COMMAND!!!===");
            return "TEST COMMAND!!!===";
        }
    }
}