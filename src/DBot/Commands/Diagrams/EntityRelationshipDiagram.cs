using DBot.Commands.Common;
using DBot.Commands.Diagrams.Generators.EntityRelationships;
using DBot.Domain;

namespace DBot.Commands.Diagrams;

public class EntityRelationshipDiagram : DslCommand<DiagramSettings>
{
    protected override void Process(CodeElement system)
    {
        var diagram = GenerateDiagram(system);
        Console.Write(diagram);
    }

    private string GenerateDiagram(CodeElement system) => Settings.DiagramFormat switch
    {
        DiagramFormat.Mermaid => new MermaidErDiagramGenerator().Generate(system),
        _ => throw new ArgumentOutOfRangeException()
    };
}