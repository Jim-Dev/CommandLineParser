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
        public CloseCommand(CmdParser cmdParser)
           : base(cmdParser,
                 "close",
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

        public override void Execute(string[] arguments)
        {
            Program.isRunning = false;
        }
    }
}
