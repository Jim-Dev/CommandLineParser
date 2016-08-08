﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommandLineParser;

namespace CommandsParserSample
{
    class Program
    {
        static bool isRunning;
        static void Main(string[] args)
        {
            CommandsParser cmdParser = new CommandsParser();
            cmdParser.AddCommand("close", delegate (object sender, CommandEventArgs e) {

                isRunning = false;
            }, "Close the application");


            isRunning = true;

            while (isRunning)
            {
                cmdParser.Execute(Console.ReadLine());
            }
        }
    }
}
