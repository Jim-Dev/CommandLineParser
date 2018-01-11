using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdParser
{
    public class BaseCommand
    {
        private const string DEFAULT_COMMAND_NAME = "UnnamedCommand";
        private const string DEFAULT_COMMAND_DESCRIPTION = "NO_DESCRIPTION";
        private readonly string[] DEFAULT_COMMAND_HELP = { "NO_HELP_DEFINED"};

        protected StringBuilder commandOutput;

        protected void AppendToResult(string message)
        {
            commandOutput.Append(message);
        }
        protected void AppendToResult(string messageFormat, params object[] args)
        {
            commandOutput.AppendFormat(messageFormat, args);
        }
        protected void AppendLineToResult()
        {
            commandOutput.AppendLine();
        }
        protected void AppendLineToResult(string message)
        {
            commandOutput.AppendLine(message);
        }
        protected void AppendLineToResult(string message, params object[] args)
        {
            commandOutput.AppendLine(string.Format(message, args));
        }
        

        protected void ClearOutputBuffer()
        {
            commandOutput.Clear();
        }

        public BaseCommand()
        {
            Name = DEFAULT_COMMAND_NAME;
            Description = DEFAULT_COMMAND_DESCRIPTION;
            Aliases = new List<string>();
            Help = DEFAULT_COMMAND_HELP;
            if (commandOutput == null)
                commandOutput = new StringBuilder();
        }
        public BaseCommand(string name)
        {
            Name = name;
            Description = DEFAULT_COMMAND_DESCRIPTION;
            Aliases = new List<string>();
            Help = DEFAULT_COMMAND_HELP;
            if (commandOutput == null)
                commandOutput = new StringBuilder();
        }
        public BaseCommand(string name, string description)
        {
            Name = name;
            Description = description;
            Aliases = new List<string>();
            Help = DEFAULT_COMMAND_HELP;
            if (commandOutput == null)
                commandOutput = new StringBuilder();
        }
        public BaseCommand(string name, string description, List<string> aliases)
        {
            Name = name;
            Description = description;
            if (aliases != null && aliases.Count > 0)
                Aliases = aliases;
            else
                Aliases = new List<string>();
            Help = DEFAULT_COMMAND_HELP;
            if (commandOutput == null)
                commandOutput = new StringBuilder();
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
        public virtual string Execute(string[] arguments)
        {
            ClearOutputBuffer();
            return commandOutput.ToString();
        }
    }
}