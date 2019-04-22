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

        public delegate void CommandExecutedEventHandler(object sender, CommandExecutedEventArgs e);
        public delegate void CommandExecutingEventHandler(object sender, CommandExecutingEventArgs e);
        public event CommandExecutedEventHandler CommandExecuted;
        public event CommandExecutingEventHandler CommandExecuting;

        private StringBuilder commandOutput;

        private bool isMuted = false;

        private bool cancel = false;

        public bool Cancel
        {
            get { return this.cancel; }
            set { this.cancel = value; }
        }

        public string Output
        {
            get
            {
                if (commandOutput != null)
                {
                    string output = commandOutput.ToString();
                    ClearOutput();
                    return output;
                }
                else
                {
                    commandOutput = new StringBuilder();
                    return string.Empty;
                }
            }
        }

        internal void CancelCommandExecution()
        {
            Cancel = true;
            AppendOutputLine("Command canceled!");
        }

        public bool IsMuted
        {
            get { return this.isMuted; }
            protected set { this.isMuted = value; }
        }
        public void AppendOutput(string message)
        {
            commandOutput.Append(message);
            if(!IsMuted)
            CmdParser.StdOutput.AppendOutput(message);
        }
        public void AppendOutputLine(string message)
        {
            commandOutput.AppendLine(message);
            if (!IsMuted)
                CmdParser.StdOutput.AppendOutputLine(message);
        }
        public void AppendOutputFormat(string messageFormat, params object[] args)
        {
            commandOutput.AppendFormat(messageFormat, args);
            if (!IsMuted)
                CmdParser.StdOutput.AppendOutputFormat(messageFormat, args);
        }
        public void AppendOutputLineFormat(string messageFormat, params object[] args)
        {
            commandOutput.AppendFormat(messageFormat+Environment.NewLine, args);
            if (!IsMuted)
                CmdParser.StdOutput.AppendOutputLineFormat(messageFormat, args);
        }
        private void ClearOutput()
        {
            commandOutput.Clear();
        }

       

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
        public virtual void Execute(string[] arguments)
        {}

        public void OnCommandExecuted(string[] arguments)
        {
            string output = Output;
            OnCommandExecuted(new CommandExecutedEventArgs(output,arguments));
        }
        public void OnCommandExecuted(CommandExecutedEventArgs eventArgs)
        {
            CommandExecuted?.Invoke(this, eventArgs);
        }
        public void OnCommandExecuting(string[] arguments)
        {
            OnCommandExecuting(new CommandExecutingEventArgs(this,arguments));
        }
        public void OnCommandExecuting(CommandExecutingEventArgs eventArgs)
        {
            CommandExecuting?.Invoke(this, eventArgs);
        }
    }
}