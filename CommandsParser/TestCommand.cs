using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdParser
{
    public class TestCommand: BaseCommand
    {

        public TestCommand()
        {

        }

        public override string Execute(string[] arguments)
        {
            return base.Execute(arguments);
        }
    }
}
