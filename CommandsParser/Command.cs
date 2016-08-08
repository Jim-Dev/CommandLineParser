using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineParser
{
    /// <summary>
    /// Defines a command.
    /// </summary>
    public class Command
    {
        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the methods that will be executed with the command.
        /// </summary>
        public CommandHandler Handler { get; set; }

        /// <summary>
        /// Gets the manual for the command, one line per index.
        /// </summary>
        public string[] Help { get; private set; }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="command">The name of the command.</param>
        /// <param name="handler">The method for the command event.</param>
        /// <param name="addToLog">True to add the command to the log, otherwise false.</param>
        /// <param name="addToHistory">True to add the command to the input history, otherwise false.</param>
        /// <param name="manual">The manual of the command, one line per index.</param>
        public Command(string command, CommandHandler handler, bool addToLog, bool addToHistory,
            string[] help)
        {
            Name = command;
            Handler = handler;
            Help = help;
        }
    }
}
