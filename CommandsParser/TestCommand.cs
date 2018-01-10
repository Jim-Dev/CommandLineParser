using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdParser
{
    public class TestCommand : BaseCommand
    {
        public override string[] Help
        {
            get
            {
                return new string[] { "help for test cmd"};
            }
        }

        public TestCommand()
           : base("test", "Displays the list of commands registered")
        {
        }

        public override string Execute(string[] arguments)
        {
          
            Console.WriteLine(CommandsParser.AvailableCommands.Count);
            Console.WriteLine("EXECUTING TEST COMMAND");
            return "EXECUTING TEST COMMAND";
        }
    }
}