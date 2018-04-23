using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandsParser.Commands
{
    public class HelpCommand : BaseCommand
    {
        public HelpCommand(CmdParser cmdParser)
           : base(cmdParser,
                 "help",
                 "Displays the list of commands, or the help file for a specified command",
                 new List<string>() { "man" },
                 null)
        { }

        public override string[] Help
        {
            get
            {
                return new string[] {
                    "If help is called without arguments, this commands show the list of every registered command",
                    "with it's short description",
                    "if a command is passed as argument, the help file for that command is shown instead"};
            }
        }

        public override void Execute(string[] arguments)
        {
            if (arguments.Length > 0) //Show help for arg[0] command
            {
                BaseCommand command = CmdParser.GetCommand(arguments[0]);
                if (command != null)
                {
                    CmdParser.StdOutput.EchoLineFormat("Displaying help for command {0}",Name, arguments[0]);
                    for (int i = 0; i < command.Help.Length; i++)
                    {
                        CmdParser.StdOutput.EchoLine(command.Help[i]);
                    }
                }
                else
                {
                    CmdParser.StdOutput.EchoLineFormat("Command {0} not found", Name, arguments[0]);
                }
            }
            else //List all commands
            {
                CmdParser.StdOutput.EchoLineFormat("List of all {0} commands", Name, CmdParser.AvailableCommands.Count);

                int commandIndex = 0;
                foreach (BaseCommand command in CmdParser.AvailableCommands)
                {
                    if (command != null)
                    {
                        CmdParser.StdOutput.EchoLineFormat("{0:000}: {1} => {2}", Name, ++commandIndex, command.Name, command.Description);
                    }
                }
            }
        }
    }
}