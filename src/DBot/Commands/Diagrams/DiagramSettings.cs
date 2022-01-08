using DBot.Commands.Common;

namespace DBot.Commands.Diagrams;

public class DiagramSettings : SourceFileSettings
{
    [CommandArgument(1, "[format]")] public DiagramFormat DiagramFormat { get; set; } = DiagramFormat.Mermaid;
}

public enum DiagramFormat
{
    Mermaid
}
