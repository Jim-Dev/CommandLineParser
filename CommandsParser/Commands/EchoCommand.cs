using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsParser.Commands
{
    public class EchoCommand:BaseCommand
    {
        public EchoCommand(CmdParser cmdParser)
           : base(cmdParser,
                 "echo",
                 "Echoes a message to the active Output",
                 null,
                 null)
        { }

        public override string[] Help
        {
            get
            {
                return new string[] {
                    "Echoes a message to the active Output"};
            }
        }

        public override void Execute(string[] arguments)
        {
            string output= string.Join(" ", arguments);
            base.CmdParser.StdOutput.AppendOutputLine(output);
        }
    }
}
