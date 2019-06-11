using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsParser.Commands
{
    public class HistoryCommand:BaseCommand
    {
        public HistoryCommand(CmdParser cmdParser)
          : base(cmdParser,
                "history",
                "Echoes the command history for this parser",
                null)
        { }

        public override string[] Help
        {
            get
            {
                return new string[] {
                    "Echoes the command history for this parser"};
            }
        }

        public override void Execute(string[] arguments)
        {
            int commandCount = 1;
            foreach (string command in CmdParser.CommandsHistory)
            {
                CmdParser.StdOutput.AppendOutputLineFormat("{0}: {1}",commandCount,command);
                commandCount++;
            }
        }
    }
}
