using CommandsParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandsParserSample.Commands
{

    public class ClearConsoleCommand : BaseCommand
    {
        public ClearConsoleCommand()
           : base("clear",
                 "Clears the console",
                 new List<string>() { "cls" })
        { }

        public override string[] Help
        {
            get
            {
                return new string[] {
                    "Clears the console"};
            }
        }

        public override string Execute(string[] arguments)
        {
            Console.Clear();
            string output = CommandOutput;
            OnOutputAvailable(new CommandsParser.Events.OutputAvailableEventArgs(Name, output));
            return output;
        }
    }

}