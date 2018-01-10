﻿using System;
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
                return this.commandOutput.ToString();
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
            /*
            AddCommand("help", delegate (object sender, CommandEventArgs e)
            {
                IEnumerable<Command> commandsSelected;
                if (e.Args.Length > 0)
                {
                    commandsSelected = from c in commands.Values
                                       where c.Name.Contains(e.Args[0])
                                       orderby c.Name
                                       select c;
                    if (commandsSelected.Any())
                    {
                        e.CommandOutput.AppendLine("List of " + commandsSelected.Count() + " appropriate commands for '" +
                            e.Args[0] + "':");
                    }
                    else {
                        e.CommandOutput.AppendLine("No appropriate commands found for '" + e.Args[0] + "'.");
                        //e.Command.AddToHistory = false;
                    }
                }
                else {
                    commandsSelected = from c in commands.Values orderby c.Name select c;
                    e.CommandOutput.AppendLine("List of all " + commands.Count + " commands:");
                }
                int i = 0;
                foreach (Command command in commandsSelected)
                {
                    e.CommandOutput.AppendLine(string.Format("{0:000}: {1} => {2}", ++i, command.Name, command.Help[0]));
                }

                //Log(e.CommandOutput.ToString());
            }, "Lists all currently registered commands with their first description line.",
                "commands <part of the command> - lists only the appropriate commands.");

            AddCommand("clear", delegate (object sender, CommandEventArgs e)
            {
                if (e.Args.Length > 0)
                {
                    if (e.Args[0] == "commands")
                    {
                        ClearCommands();
                    }
                    else {
                        if (commands.ContainsKey(e.Args[0]))
                            commands.Remove(e.Args[0]);
                        else
                            e.CommandOutput.AppendLine("Command 'clear " + e.Args[0] + "' not found.");
                    }
                }
                else {
                    Console.Clear();

                }
            }, "Clears the console log or the command list.",
                "clear - clears the log.",
                "clear commands - clears all registered commands.");


            */

            AddCommand(new Commands.ClearCommand());
            AddCommand(new Commands.HelpCommand());
            AddCommand(new Commands.AliasCommand());

        }

        /*
        /// <summary>
        /// <para>Adds a custom command to the command line parser.</para>
        /// <para>It is possible to add more then one command with the same name.</para>
        /// </summary>
        /// <param name="command">The name of the command.</param>
        /// <param name="handler">The method that will be called if the command is executed.</param>
        /// <param name="addToLog">True to add the command to the log, otherwise false.</param>
        /// <param name="addToHistory">True to add the command to the input history, otherwise false.</param>
        /// <param name="manual">The manual of the command, one line per index.</param>
        /// <example>
        /// Add a new command using a delegate (extended example):
        /// <code>
        /// GameConsole console = new GameConsole(this, null);
        /// console.AddCommand("hello", delegate(object sender, CommandEventArgs e)
        /// {
        ///     console.Log("Hello World!");
        /// }, true, false, "hello", "world", "manual");
        /// </code>
        /// </example>
        public void AddCommand(string command, CommandHandler handler, bool addToLog,
            bool addToHistory, params string[] manual)
        {
            if (handler != null)
            {
                if (commands.ContainsKey(command))
                {
                    commands[command].Handler += handler;
                }
                else {
                    if (manual.Length == 0)
                    {
                        manual = new[] { "NO_DESCRIPTION" };
                    }
                    commands.Add(command,
                        new Command(command, handler, addToLog, addToHistory, manual));
                }
            }
        }

        /// <summary>
        /// <para>Adds a custom command to the game console.</para>
        /// </summary>
        /// <param name="command">The name of the command.</param>
        /// <param name="handler">The method that will be called if the command is executed.</param>
        /// <param name="manual">The manual of the command, one line per index.</param>
        /// <example>
        /// Add a new command using a lamba expression:
        /// <code>
        /// GameConsole console = new GameConsole(this, null);
        /// console.AddCommand("hello", (sender, e) => console.Log("Hello World!"));
        /// </code>
        /// </example>
        /// <example>
        /// Add a new command using a delegate:
        /// <code>
        /// CommandLineParser cmdParser = new CommandLineParser(this, null);
        /// console.AddCommand("hello", delegate(object sender, CommandEventArgs e)
        /// {
        ///     console.Log("Hello World!");
        /// });
        /// </code>
        /// </example>
        /// <example>
        /// Add a new command using a standalone method:
        /// <code>
        /// CommandLineParser cmdParser = new CommandLineParser(this, null);
        /// console.AddCommand("hello", testMethod);
        /// void testMethod(object sender, CommandEventArgs e)
        /// {
        ///     console.Log("Hello World!");
        /// }
        /// </code>
        /// </example>
        public void AddCommand(string command, CommandHandler handler, params string[] manual)
        {
            AddCommand(command, handler, true, true, manual);
        }
        */
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
        /*
        /// <summary>
        /// Removes the command(s) from the game console.
        /// </summary>
        /// <param name="command">The name of the command.</param>
        public void RemoveCommand(string command)
        {
            if (commands.ContainsKey(command))
            {
                commands.Remove(command);
            }
        }
        */

        /*
    /// <summary>
    /// <para>Executes a game console command.</para>
    /// <para>This method will also be called if the user enters a command in the input field and hits enter.</para>
    /// </summary>
    /// <param name="input">The command to be executed.</param>
    /// <param name="addToLog">True, if the input should be added to the log.</param>
    /// <returns>Returns true if the execution was successful, otherwise false.</returns>
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

        if (commands.ContainsKey(splitInput[0]))
        {
            Command command = commands[splitInput[0]];
            string[] args = new string[splitInput.Length - 1];
            Array.Copy(splitInput, 1, args, 0, args.Length);
            command.Handler(this, new CommandEventArgs(command, args, commandOutput));

            if (commandsHistory.Count >= maxCommandsInHistory)
                commandsHistory.Dequeue();
            commandsHistory.Enqueue(input);

            return true;
        }
        else
        {
            Log(string.Format("ERROR command {0} not found", input));
            return false;
        }


    }

    */

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
                commandToExecute.Execute(args);
                return true;
            }
            else
                return false;
            /*
            if (commands.ContainsKey(splitInput[0]))
            {
                Command command = commands[splitInput[0]];
                string[] args = new string[splitInput.Length - 1];
                Array.Copy(splitInput, 1, args, 0, args.Length);
                command.Handler(this, new CommandEventArgs(command, args, commandOutput));

                if (commandsHistory.Count >= maxCommandsInHistory)
                    commandsHistory.Dequeue();
                commandsHistory.Enqueue(input);

                return true;
            }
            else
            {
                Log(string.Format("ERROR command {0} not found", input));
                return false;
            }
            */

        }

        private void Log(string message)
        {
            Console.WriteLine(OutputPrefix + message);
        }

        /*
        /// <summary>
        /// Removes all commands.
        /// </summary>
        public void ClearCommands()
        {
            int count = commands.Count;
            commands.Clear();
            Log("Commands cleared, " + count + " commands deleted.");
        }
        */

    }
}