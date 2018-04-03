using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CommandsParser.StandardOutput;

namespace CommandsParser
{
    public class CmdParser
    {
        private Queue<string> commandsHistory;
        private int maxCommandsInHistory = 3;

        public event OutputAvailableEventHandler OnOutputAvailable;

        private StandardOutput stdOutput;

        internal static List<BaseCommand> AvailableCommands;

        public CmdParser()
        {
            stdOutput = new StandardOutput();
            stdOutput.OnOutputAvailable += StdOutput_OnOutputAvailable;

            AvailableCommands = new List<BaseCommand>();

            commandsHistory = new Queue<string>();

            AddDefaultCommands();
        }

        private void StdOutput_OnOutputAvailable(object sender, Events.StdOutputAvailableEventArgs e)
        {
            OnOutputAvailable?.Invoke(sender, e);
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
                stdOutput.Echo(commandToExecute.Execute(args));
                return true;
            }
            else
                stdOutput.EchoLine(string.Format("ERROR, command \"{0}\", not found.", splitInput[0]));
            return false;
        }

        private void Log(string message, bool appendPrefix)
        {
            if (appendPrefix)
                Console.WriteLine(stdOutput.OutputPrefix + message);
            else
                Console.WriteLine(message);
        }
        private void Log(string message)
        {
            Log(message, true);
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