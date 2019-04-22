﻿using CommandsParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandsParserSample.Commands
{

    public class ClearConsoleCommand : BaseCommand
    {
        public ClearConsoleCommand(CmdParser cmdParser)
           : base(cmdParser,
                 "clear",
                 "Clears the console",
                 new List<string>() { "cls" },
                 null)
        { }

        public override string[] Help
        {
            get
            {
                return new string[] {
                    "Clears the console"};
            }
        }

        public override void Execute(string[] arguments)
        {
            OnCommandExecuting(arguments);
            if (Cancel)
                return;
            else
            {
                Console.Clear();
                OnCommandExecuted(arguments);
            }
        }
    }

}