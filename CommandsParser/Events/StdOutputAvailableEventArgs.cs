using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsParser.Events
{
    public class StdOutputAvailableEventArgs:EventArgs
    {
        public string Output { get; private set; }

        public StdOutputAvailableEventArgs(string status)
        {
            Output = status;
        }
    }
}
