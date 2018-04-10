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

        private void AppendOutput(string message)
        {
            AppendOutput(message, false);
        }
        private void AppendOutputLine(string message)
        {
            AppendOutputLine(message, false);
        }
        private void AppendOutputFormat(string messageFormat, params object[] args)
        {
            AppendOutputFormat(messageFormat, false, args);
        }

        private void AppendOutput(string message, bool appendPrefix)
        {
            if (appendPrefix)
                commandOutput.Append(OutputPrefix + message);
            else
                commandOutput.Append(message);
        }
        private void AppendOutputLine(string message, bool appendPrefix)
        {
            if (appendPrefix)
                commandOutput.AppendLine(OutputPrefix + message);
            else
                commandOutput.AppendLine(message);
        }
        private void AppendOutputFormat(string messageFormat, bool appendPrefix, params object[] args)
        {
            if (appendPrefix)
                commandOutput.AppendFormat(OutputPrefix + messageFormat, args);
            else
                commandOutput.AppendFormat(messageFormat, args);
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