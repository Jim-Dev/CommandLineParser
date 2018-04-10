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
            stdOutput.OnOutputAvailable += Output_OnOutputAvailable;

            AvailableCommands = new List<BaseCommand>();

            commandsHistory = new Queue<string>();

            AddDefaultCommands();
        }

        private void Output_OnOutputAvailable(object sender, Events.OutputAvailableEventArgs e)
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
                stdOutput.Echo(commandToExecute.Execute(args), commandToExecute.Name);
                return true;
            }
            else
                stdOutput.EchoLine(string.Format("ERROR, command \"{0}\", not found.", commandToExecute.Name), commandToExecute.Name);
            return false;
        }

        /// <summary>
        /// Removes all commands.
        /// </summary>
        public void ClearCommands()
        {
            int count = AvailableCommands.Count;
            AvailableCommands.Clear();
            stdOutput.EchoLine("Commands cleared, " + count + " commands deleted.");
        }


    }
}