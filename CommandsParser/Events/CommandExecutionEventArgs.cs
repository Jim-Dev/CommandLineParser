using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsParser.Events
{
    public class CommandExecutedEventArgs : EventArgs
    {
        public string Output { get; private set; }
        public string[] Arguments { get; private set; }

        public CommandExecutedEventArgs(string output,string[] arguments)
        {
            Output = output;
            Arguments = arguments;
        }
    }
    public class CommandExecutingEventArgs : EventArgs
    {
        private bool cancel = false;
        public string[] Arguments { get; private set; }

        public string MergedArgs
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (string arg in Arguments)
                {
                    sb.AppendFormat("{0} ", arg);
                }
                return sb.ToString().Trim();
            }
        }
        private BaseCommand commandOwner;
        public bool Cancel
        {
            get { return this.cancel; }
            set { this.cancel = value;
                if (value)
                    commandOwner.CancelCommandExecution();
            }
        }

        public CommandExecutingEventArgs(BaseCommand commandOwner, string[] arguments)
        {
            this.commandOwner = commandOwner;
            Arguments = arguments;
        }
    }
}
