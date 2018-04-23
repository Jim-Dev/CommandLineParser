using System;
using System.Collections.Generic;
using System.Text;

using CommandsParser.Events;
namespace CommandsParser
{
    public class StandardOutput
    {
        public delegate void OutputAvailableEventHandler(object sender, OutputAvailableEventArgs e);
        public event OutputAvailableEventHandler OnOutputAvailable;

        public const string DEFAULT_UNSPECIFIED_COMMAND = "UnspecifiedCommand";

        public string Output
        {
            get
            {
                if (commandOutput != null)
                    return commandOutput.ToString();
                else
                {
                    commandOutput = new StringBuilder();
                    return string.Empty;
                }
            }
        }

        private StringBuilder commandOutput;
        private string outputPrefix;

        private const string DEFAULT_OUTPUT_PREFIX = ">>>";

        public string OutputPrefix
        {
            get { return this.outputPrefix; }
            set { this.outputPrefix = value; }
        }

        public void AppendOutput(string message)
        {
            AppendOutput(message, false);
        }
        public void AppendOutputLine(string message)
        {
            AppendOutputLine(message, false);
        }
        public void AppendOutputFormat(string messageFormat, params object[] args)
        {
            AppendOutputFormat(messageFormat, false, args);
        }
        public void AppendOutputLineFormat(string messageFormat, params object[] args)
        {
            AppendOutputLineFormat(messageFormat, false, args);
        }

        public void AppendOutput(string message, bool appendPrefix)
        {
            if (appendPrefix)
                commandOutput.Append(OutputPrefix + message);
            else
                commandOutput.Append(message);
        }
        public void AppendOutputLine(string message, bool appendPrefix)
        {
            if (appendPrefix)
                commandOutput.AppendLine(OutputPrefix + message);
            else
                commandOutput.AppendLine(message);
        }
        public void AppendOutputFormat(string messageFormat, bool appendPrefix, params object[] args)
        {
            if (appendPrefix)
                commandOutput.AppendFormat(OutputPrefix + messageFormat, args);
            else
                commandOutput.AppendFormat(messageFormat, args);
        }
        public void AppendOutputLineFormat(string messageFormat, bool appendPrefix, params object[] args)
        {
            if (appendPrefix)
                commandOutput.AppendFormat(OutputPrefix + messageFormat + Environment.NewLine, args);
            else
                commandOutput.AppendFormat(messageFormat + Environment.NewLine, args);
        }

        public void Echo(string message)//Append (output) and Flush
        {
            Echo(message, DEFAULT_UNSPECIFIED_COMMAND);
        }
        public void EchoLine(string message)//Append (output) and Flush
        {
            EchoLine(message, DEFAULT_UNSPECIFIED_COMMAND);
        }
        public void EchoFormat(string format, params object[] args)//Append (output) and Flush
        {
            EchoFormat(format, DEFAULT_UNSPECIFIED_COMMAND, args);
        }
        public void EchoLineFormat(string format, params object[] args)//Append (output) and Flush
        {
            EchoLineFormat(format, DEFAULT_UNSPECIFIED_COMMAND, args);
        }

        public void Echo(string message, string commandSender)//Append (output) and Flush
        {
            AppendOutput(message);
            FlushOutput(commandSender);
        }
        public void EchoLine(string message, string commandSender)//Append (output) and Flush
        {
            AppendOutputLine(message);
            FlushOutput(commandSender);
        }
        public void EchoFormat(string format, string commandSender, params object[] args)//Append (output) and Flush
        {
            AppendOutputFormat(format, args);
            FlushOutput(commandSender);
        }
        public void EchoLineFormat(string format, string commandSender, params object[] args)//Append (output) and Flush
        {
            AppendOutputFormat(format+Environment.NewLine, args);
            FlushOutput(commandSender);
        }
        public void FlushOutput(string commandSender)
        {
            string cmdResult = commandOutput.ToString();
            commandOutput.Clear();
            OnOutputAvailable?.Invoke(this, new OutputAvailableEventArgs(cmdResult, commandSender));
            
        }

        public StandardOutput()
        {
            OutputPrefix = DEFAULT_OUTPUT_PREFIX;
            commandOutput = new StringBuilder();
        }
    }

}