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
CommandLineParser is a library that provides a way to easily parse commands from a string, basically this library maps a command (single word string) and optionally a set of aliases and arguments, to a delegate to execute an action.
Also provides an easy way to implement help and usage information for any command.

Now compiled to target NetStandard 1.0!, to make it compatible with older version of .NetFramework (4.5).(*this could potentially change in the future*)

# Instalation
You can install this CommandLineParser using nuget
```batch
Install-Package CommandParser
```
# Usage
First of all you need to create an instance of CmdParser, then optionally but highly encouraged, you could subscribe to the event OnOutputAvailable so you can process Output messages from the commands.

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

If you want to implement your own custom command, you need to create a class and make it inherit from BaseCommand, and override it's methods

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
