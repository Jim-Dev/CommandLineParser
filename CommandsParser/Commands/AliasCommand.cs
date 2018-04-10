using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandsParser.Commands
{
    public class AliasCommand:BaseCommand
    {
        public AliasCommand()
           : base("alias",
                 "Display the list of all aliases for a given command or show if a command is an alias of another command",
                 null)
        { }

        public override string[] Help
        {
            get
            {
                return new string[] {
                    "If this command is called with no arguments, alias return a list of every   with all their respective aliases, if any.",
                    "If this command is called with a command as arguments, shows the alias for that command, or the name of the master command if this is an alias of another command"};
            }
        }

        public override string Execute(string[] arguments)
        {
            if (arguments.Length > 0) //Show help for arg[0] command
            {
                BaseCommand command = CmdParser.GetCommand(arguments[0]);
                if (command != null)
                {
                    if (command.Aliases.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("{0} => ", command.Name);
                        for (int i = 0; i < command.Aliases.Count; i++)
                        {
                            if (i== command.Aliases.Count-1)
                                sb.AppendFormat("{0}", command.Aliases[i]);
                            else
                                sb.AppendFormat("{0},", command.Aliases[i]);
                        }

                        AppendLineToResult(sb.ToString());

                    }
                    else
                    {
                        AppendLineToResult("No aliases found for command {0}", arguments[0]);
                    }
                }
                else
                {
                    AppendLineToResult("Command {0} not found", arguments[0]);
                }
            }
            else //List all aliases
            {
                AppendLineToResult("List of all " + CmdParser.AvailableCommands.Count + " commands:");
                int commandIndex = 0;
                foreach (BaseCommand command in CmdParser.AvailableCommands)
                {

                    StringBuilder sb = new StringBuilder();
                 
                    for (int i = 0; i < command.Aliases.Count; i++)
                    {
                        if (i == command.Aliases.Count - 1)
                            sb.AppendFormat("{0}", command.Aliases[i]);
                        else
                            sb.AppendFormat("{0},", command.Aliases[i]);
                    }
                    AppendLineToResult(string.Format("{0:000}: {1} => {2}", ++commandIndex, command.Name, sb.ToString()));


                }
            }
            string output = CommandOutput;
            OnOutputAvailable(new Events.OutputAvailableEventArgs(Name, output));
            return output;
        }
    }
}
