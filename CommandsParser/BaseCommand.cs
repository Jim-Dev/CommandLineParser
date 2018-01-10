using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdParser
{
    public class BaseCommand
    {
        /// <summary>
        /// The name of the command; the command will be invoked through this name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///  Gets the manual for the command that is displayed with the 'help' command
        /// </summary>
        public string Help { get; private set; }

        /// <summary>
        ///  Gets the optionals aliases, so the same command can be invoked through different names
        /// </summary>
        public List<string> Aliases { get; private set; }

        /// <summary>
        /// The action of the command.  The return string value is used as output in the console 
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public virtual string Execute(string[] arguments)
        {
            return string.Empty;
        }
    }
}
