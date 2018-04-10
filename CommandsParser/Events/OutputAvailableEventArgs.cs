using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsParser.Events
{
    public class OutputAvailableEventArgs:EventArgs
    {
        public string Output { get; private set; }
        public string CommandSender { get; private set; }

        public OutputAvailableEventArgs(string output, string commandSender)
        {
            Output = output;
            CommandSender = commandSender;
        }
        public OutputAvailableEventArgs(string output)
        {
            Output = output;
            CommandSender = string.Empty;
        }
        public OutputAvailableEventArgs()
        {
            Output = string.Empty;
            CommandSender = string.Empty;
        }
    }
}
