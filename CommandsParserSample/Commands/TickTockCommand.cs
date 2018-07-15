using CommandsParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandsParserSample.Commands
{
    public class TickTockCommand : BaseCommand
    {
        private int totalCycleCount = 0;
        private int totalCycleLenght = 0;
        private int cyclesCompleted = 0;

        private const int DEFAULT_CYCLES_COUNT = 3;
        private const int DEFAULT_CYCLE_LENGHT = 500;

        private object lockObject;
        Timer timer;
        public TickTockCommand(CmdParser cmdParser)
           : base(cmdParser,
                 "tick",
                 "Clears the console",
                 new List<string>(),
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
            if (arguments.Length>0)
            {
                if (arguments.Length>=2)
                {
                    totalCycleCount = int.Parse(arguments[0]);
                    totalCycleLenght = int.Parse(arguments[1]);
                }
            }
            else
            {
                totalCycleCount = DEFAULT_CYCLES_COUNT;
                totalCycleLenght = DEFAULT_CYCLE_LENGHT;
            }
            timer = new Timer(TimerCallback, null, totalCycleLenght, Timeout.Infinite);
            CmdParser.StdOutput.EchoLineFormat("cycle duration : {0}, cycles: {1}", totalCycleLenght, totalCycleCount);


        }

        private void TimerCallback(Object stateInfo)
        {
            cyclesCompleted++;
            if (cyclesCompleted <= totalCycleCount)
            {
                CmdParser.StdOutput.EchoLine("tick");
                timer.Change(totalCycleLenght, Timeout.Infinite);

            }
            else
            {
                AppendOutputLine("tock");
                OnCommandExecuted();

            }
        }
    }
}