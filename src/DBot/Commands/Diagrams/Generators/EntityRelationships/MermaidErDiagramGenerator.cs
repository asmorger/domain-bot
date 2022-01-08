using Dbot.Domain;
using Dbot.Domain.Analyzers;
using Scriban;

namespace DBot.Commands.Diagrams.Generators.EntityRelationships;

public class MermaidErDiagramGenerator : DiagramGenerator
{
    private static readonly Lazy<Template> Template = new(() => Scriban.Template.Parse(GetTemplate()));

    public string Generate(CodeElement system)
    {
        var input =
            from entity in SystemAnalyzer.GetEntities(system)
            let properties = entity.GetProperties().Select(x => new Property(x.Type, x.Name))
            let relationships = entity.GetRelationships().Select(x => new Relationship(GetRelationshipSymbol(x.Type), x.Name))
            select new Entity(entity.Name, properties, relationships);

        var opts = new {entities = input};

        return Template.Value.Render(opts);
    }

    private static string GetTemplate() => @"erDiagram
{{~ for entity in entities ~}}
    {{ entity.name }} {
{{~ for property in entity.properties ~}}
        {{ property.type }} {{ property.name }}
{{~ end ~}}
    }
{{~ end ~}}

{{~ for entity in entities ~}}
{{~ for relationship in entity.relationships ~}}
    {{ entity.name }} {{ relationship.symbol }} {{ relationship.target }} : """"
{{~ end ~}}
{{~ end ~}}
";

    private static string GetRelationshipSymbol(Dbot.Domain.Relationship.RelationshipType type) => type switch
    {
        Dbot.Domain.Relationship.RelationshipType.OneToMany => "||--o{",
        Dbot.Domain.Relationship.RelationshipType.OneToOne => "||--||",
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
    };

    private record Entity(string Name, IEnumerable<Property> Properties, IEnumerable<Relationship> Relationships);

    private record Property(string Type, string Name);

    private record Relationship(string Symbol, string Target);
}
