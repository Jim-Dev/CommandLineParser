﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CommandsParser.StandardOutput;

namespace CommandsParser
{
    public class CmdParser
    {
        public const string COMMAND_START_OUTPUT = "<COMMAND_START>";
        public const string COMMAND_FINISH_OUTPUT = "<COMMAND_FINISH>";

        private Queue<string> executedCommandsHistory;
        private int maxCommandsInHistory = 32;

        public event OutputAvailableEventHandler OnOutputAvailable;

        private StandardOutput stdOutput;

        internal static List<BaseCommand> AvailableCommands;

        public bool UseAutomaticOutput = false;

        public string[] CommandsHistory
        {
            get { return this.executedCommandsHistory.ToArray(); }
        }

        public StandardOutput StdOutput
        {
            get { return stdOutput; }
        }

        public CmdParser()
        {
            stdOutput = new StandardOutput();
            stdOutput.OnOutputAvailable += Output_OnOutputAvailable;

            AvailableCommands = new List<BaseCommand>();

            executedCommandsHistory = new Queue<string>();

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
            AddCommand(new Commands.HelpCommand(this));
            AddCommand(new Commands.AliasCommand(this));
            AddCommand(new Commands.EchoCommand(this));
            AddCommand(new Commands.HistoryCommand(this));
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

            if (splitInput.Length > 0)
            {
                string commandName = splitInput[0];
                string joinedArguments = string.Join(" ", splitInput.Skip(1));

                BaseCommand commandToExecute = GetCommand(splitInput[0]);
                if (commandToExecute != null)
                {
                    string[] args = new string[splitInput.Length - 1];
                    Array.Copy(splitInput, 1, args, 0, args.Length);

                    executedCommandsHistory.Enqueue(string.Format("{0} {1}", commandToExecute.Name, joinedArguments));
                    if (executedCommandsHistory.Count > maxCommandsInHistory)
                        executedCommandsHistory.Dequeue();

                    commandToExecute.RunCommand(args);
                    stdOutput.FlushOutput(commandToExecute.Name);

                    return true;
                }
                else
                    stdOutput.EchoLine(string.Format("ERROR, command \"{0}\", not found.", commandName), commandName);
            }

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