using DBot.Commands;
using Spectre.Console.Cli;

namespace DBot;

public static class AppCommands
{
    public static void Configure(IConfigurator config)
    {
        config.Settings.CaseSensitivity = CaseSensitivity.None;

        config.AddCommand<Parse>("parse");
    }
}