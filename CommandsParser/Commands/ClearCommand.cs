using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdParser.Commands
{
    public class ClearCommand:BaseCommand
    {
        public ClearCommand()
           : base("clear",
                 "Clears the console",
                 new List<string>() {"cls"})
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
            return string.Empty;
        }
    }
}
