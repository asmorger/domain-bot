using DBot.Commands.Common;
using DBot.Domain;
using Spectre.Console;

namespace DBot.Commands.Diagrams;

public class EntityRelationshipDiagram : DslCommand<DiagramSettings>
{
    protected override void Process(CodeElement system) => AnsiConsole.WriteLine("ER Diagrams will go here.");
}