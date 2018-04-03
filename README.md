# CommandLineParser

<p align="center">
  <img src="https://img.shields.io/github/issues/Jim-Dev/CommandLineParser.svg">
  <img src="https://img.shields.io/github/license/Jim-Dev/CommandLineParser.svg">
</p>
<p align="center">
  <img src="https://img.shields.io/badge/.Net%20Standard-1.0-brightgreen.svg">
  <img src="https://img.shields.io/badge/.Net%20Core-1.0-brightgreen.svg">
  <img src="https://img.shields.io/badge/.Net%20Framework-4.5-brightgreen.svg">
</p>

---
CommandLineParser is a library that provides a way to easily parse commands from a string, Basically this library maps a command (single-word string) and optionally a set of aliases and arguments, allowing a delegate to execute an action.
Additionally, it provides an easy way to implement help and usage information for any command.

Now compiled to target NetStandard 1.0!, to make it compatible with older version of .NetFramework (4.5).(*liable to change in the future*)

# Instalation
You can install this CommandLineParser using nuget
```batch
Install-Package CommandParser
```
# Usage
First of all you must create an instance of CmdParser, then optionally. The subscription to the event OnOutputAvailable is optional but highly encouraged as a second step, as it allows processing of Output messages from the commands and command parser.

Here is an example, from the sample project in this repository.

```C#
using System;
using CommandsParser;

namespace CommandsParserSample
{
    class Program
    {
        public static bool isRunning;
        static void Main(string[] args)
        {
            CmdParser cmdParser = new CmdParser();

            cmdParser.OnOutputAvailable += StdOutput_OnOutputAvailable;

            isRunning = true;

            while (isRunning)
            {
                cmdParser.Execute(Console.ReadLine());
            }
        }
        private static void StdOutput_OnOutputAvailable(object sender, CommandsParser.Events.StdOutputAvailableEventArgs e)
        {
            Console.WriteLine(e.Output);
        }
    }
}
```

# Custom commands

If you want to implement your own custom command, you must create a class and make it inherit from BaseCommand, and override it's methods

Here is an example from the sample provided in this repository

```C#
public class ClearConsoleCommand : BaseCommand
    {
        public ClearConsoleCommand()
           : base("clear",
                 "Clears the console",
                 new List<string>() { "cls" }){ }
        public override string[] Help
        {
            get
            {
                return new string[] {
                    "Clears the console"};
            }
        }
        public override string Execute(string[] arguments)
        {
            Console.Clear();
            return string.Empty;
        }
}
```
