using CommandsParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandsParserSample.Commands
{
    public class UppercaseCommand:BaseCommand
    {
        public UppercaseCommand(CmdParser cmdParser)
           : base(cmdParser,
                 "uppercase",
                 "Makes all the text in the arguments UPPERCASE",
                 new List<string>() { "upper" },
                 null)
        {
            IsMuted = true;
        }

        public override string[] Help
        {
            get
            {
                return new string[] {
                    "Makes all the text in the arguments UPPERCASE"};
            }
        }

        public override void Execute(string[] arguments)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < arguments.Length; i++)
            {
                sb.AppendFormat("{0} ", arguments[i].ToUpper());
            }
            AppendOutputLine(sb.ToString());
            OnCommandExecuted(arguments);
        }
    }
}
