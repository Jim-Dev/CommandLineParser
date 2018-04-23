﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsParser.Events
{
    public class CommandExecutedEventArgs:EventArgs
    {
        public string Output { get; private set; }

        public CommandExecutedEventArgs(string output)
        {
            Output = output;
        }
    }
}
