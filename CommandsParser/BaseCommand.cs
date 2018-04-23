using CommandsParser.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandsParser
{
    public class BaseCommand
    {
        private const string DEFAULT_COMMAND_NAME = "UnnamedCommand";
        private const string DEFAULT_COMMAND_DESCRIPTION = "NO_DESCRIPTION";
        private readonly string[] DEFAULT_COMMAND_HELP = { "UNDEFINED_HELP"};

        protected CmdParser CmdParser;

        public BaseCommand(CmdParser cmdParser, string name, string description, List<string> aliases, string[] commandHelp)
        {
            this.CmdParser = cmdParser;

            if (name.Trim() != string.Empty)
                Name = name;
            else
                name = DEFAULT_COMMAND_NAME;

            if (description.Trim() != string.Empty)
                Description = description;
            else
                description = DEFAULT_COMMAND_DESCRIPTION;

            if (aliases != null && aliases.Count > 0)
                Aliases = aliases;
            else
                Aliases = new List<string>();

            if (commandHelp != null && commandHelp.Length > 0)
                Help = commandHelp;
            else
                Help = DEFAULT_COMMAND_HELP;
        }


        /// <summary>
        /// The name of the command; the command will be invoked through this name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///  Gets the manual for the command that is displayed with the 'help' command
        /// </summary>
        public virtual string[] Help { get; private set; }

        public virtual string Description { get; private set; }
        /// <summary>
        ///  Gets the optionals aliases, so the same command can be invoked through different names
        /// </summary>
        public virtual List<string> Aliases { get; private set; }

        /// <summary>
        /// The action of the command.  The return string value is used as output in the console 
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public virtual void Execute(string[] arguments)
        {}
       
    }
}