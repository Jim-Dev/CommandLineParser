using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdParser
{
    public class CommandsParser
    {
        private Queue<string> commandsHistory;
        private int maxCommandsInHistory = 3;

        public string LastExecutedOutput
        {
            get
            {
                string commandResult = this.commandOutput.ToString();
                this.commandOutput.Clear();
                return commandResult;
            }
        }
        private StringBuilder commandOutput;

        private string outputPrefix;
        /// <summary>
        /// The dictionary to store all the commands and command event handlers.
        /// </summary>
        //private readonly Dictionary<string, Command> commands;

        internal static List<BaseCommand> AvailableCommands;
        //internal static List<BaseCommand> comms;
        public string OutputPrefix
        {
            get { return this.outputPrefix; }
            set { this.outputPrefix = value; }
        }

        public CommandsParser()
        {
            OutputPrefix = ">>> ";
            //commands = new Dictionary<string, Command>();
            AvailableCommands = new List<BaseCommand>();
            commandOutput = new StringBuilder();

            commandsHistory = new Queue<string>();

            AddDefaultCommands();
        }

        public static BaseCommand GetCommand(string commandNameOrAlias)
        {
            BaseCommand output = GetCommandByName(commandNameOrAlias);
            if (output != null)
                return output;
            else
                return GetCommandByAlias(commandNameOrAlias);

        }
        public static BaseCommand GetCommandByName(string commandName)
        {
            foreach (BaseCommand command in AvailableCommands)
            {
                if (command.Name == commandName)
                    return command;
            }
            return null;
        }
        public static BaseCommand GetCommandByAlias(string commandAlias)
        {
            foreach (BaseCommand command in AvailableCommands)
            {
                foreach (string cmdAlias in command.Aliases)
                {
                    if (cmdAlias == commandAlias)
                        return command;
                }
            }
            return null;
        }


        private void AddDefaultCommands()
        {
            AddCommand(new Commands.ClearCommand());
            AddCommand(new Commands.HelpCommand());
            AddCommand(new Commands.AliasCommand());
        }

        public void AddCommand(BaseCommand command)
        {
            if (!AvailableCommands.Contains(command))
                AvailableCommands.Add(command);
        }

        public void RemoveCommand(string commandName)
        {
            RemoveCommand(GetCommandByName(commandName));
        }
        public void RemoveCommand(BaseCommand command)
        {
            if (AvailableCommands.Contains(command))
                AvailableCommands.Remove(command);
        }



        public bool Execute(string input)
        {
            commandOutput.Clear();
            input = input.Trim();

            if (input == string.Empty)
            {
                return false;
            }

            string[] splitInput = input.Split(new[] { ' ', '\t' },
                StringSplitOptions.RemoveEmptyEntries);

            BaseCommand commandToExecute = GetCommand(splitInput[0]);
            if (commandToExecute != null)
            {
                string[] args = new string[splitInput.Length - 1];
                Array.Copy(splitInput, 1, args, 0, args.Length);
                commandOutput.Append(commandToExecute.Execute(args));
                return true;
            }
            else
                commandOutput.AppendLine(string.Format("ERROR, command \"{0}\", not found.", splitInput[0]));
            return false;
        }

        private void Log(string message, bool appendPrefix)
        {
            if (appendPrefix)
                Console.WriteLine(OutputPrefix + message);
            else
                Console.WriteLine(message);
        }
        private void Log(string message)
        {
            Log(message, true);
        }



        private void AppendToResult(string message)
        {
            AppendToResult(message, false);
        }
        private void AppendLineToResult(string message)
        {
            AppendLineToResult(message, false);
        }
        private void AppendFormatToResult(string messageFormat, params object[] args)
        {
            AppendFormatToResult(messageFormat, false, args);
        }

        private void AppendToResult(string message, bool appendPrefix)
        {
            if (appendPrefix)
                commandOutput.Append(OutputPrefix + message);
            else
                commandOutput.Append(message);
        }
        private void AppendLineToResult(string message, bool appendPrefix)
        {
            if (appendPrefix)
                commandOutput.AppendLine(OutputPrefix + message);
            else
                commandOutput.AppendLine(message);
        }
        private void AppendFormatToResult(string messageFormat, bool appendPrefix, params object[] args)
        {
            if (appendPrefix)
                commandOutput.AppendFormat(OutputPrefix + messageFormat, args);
            else
                commandOutput.AppendFormat(messageFormat, args);
        }

        /// <summary>
        /// Removes all commands.
        /// </summary>
        public void ClearCommands()
        {
            int count = AvailableCommands.Count;
            AvailableCommands.Clear();
            Log("Commands cleared, " + count + " commands deleted.");
        }


    }
}