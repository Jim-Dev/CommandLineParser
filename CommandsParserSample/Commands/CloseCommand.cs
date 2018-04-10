using CommandsParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandsParserSample.Commands
{
    public class CloseCommand : BaseCommand
    {
        public CloseCommand()
           : base("close",
                 "Close the application",
                 new List<string>() { "exit", "shutdown" })
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
            string output = CommandOutput;
            OnOutputAvailable(new CommandsParser.Events.OutputAvailableEventArgs(Name, output));
            return output;
        }
    }
}
