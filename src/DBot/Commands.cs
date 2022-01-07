using DBot.Commands;
using DBot.Commands.Diagrams;
using Spectre.Console.Cli;

namespace DBot;

public static class AppCommands
{
    public static void Configure(IConfigurator config)
    {
        config.Settings.CaseSensitivity = CaseSensitivity.None;

        config.AddCommand<Parse>("parse");
        
        config.AddBranch("analyze", analyze =>
        {
            analyze.AddCommand<Complexity>("complexity");
        });
        
        config.AddBranch("diagram", diagram =>
        {
            diagram.AddCommand<EntityRelationshipDiagram>("er");
        });
        
    }
}