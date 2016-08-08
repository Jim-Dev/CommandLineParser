﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineParser
{
    /// <summary>
    /// Represents the method that will handle an command event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">CommandEventArgs that contains the command,
    /// all aguments passed on the command and a timestamp.</param>
    public delegate void CommandHandler(object sender, CommandEventArgs e);

    /// <summary>
    /// Defines the EventArgs for the command event.
    /// </summary>
    public class CommandEventArgs : EventArgs
    {
        /// <summary>
        /// Timestamp when the event is raised and the command is executed.
        /// </summary>
        public DateTime Time { get; private set; }

        /// <summary>
        /// The command.
        /// </summary>
        public Command Command { get; private set; }

        /// <summary>
        /// The arguments passed on the command.
        /// </summary>
        public string[] Args { get; private set; }

        /// <summary>
        /// Creates a new instance of the CommandEventArgs.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="args">The arguments passed on the command.</param>
        public CommandEventArgs(Command command, string[] args)
        {
            Time = DateTime.Now;
            Command = command;
            Args = args;
        }
    }
}
