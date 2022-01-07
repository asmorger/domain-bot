using DBot.Commands.Common;
using Spectre.Console.Cli;

namespace DBot.Commands.Diagrams;

public class DiagramSettings : SourceFileSettings
{
    [CommandOption("-f|--format")] public DiagramFormat DiagramFormat { get; set; } = DiagramFormat.Mermaid;
}

public enum DiagramFormat
{
    Mermaid
}