using System.Diagnostics;

namespace DBot.Domain;

[DebuggerDisplay("Relationship to {Name}")]
public class Relationship : CodeElement
{
    public Relationship(RelationshipType type, string name)
    {
        Type = type;
        Name = name;
    }

    public RelationshipType Type { get; }
    public string Name { get; }
    
    public enum RelationshipType
    {
        OneToMany,
        OneToOne,
    }
}

[DebuggerDisplay("relationships")]
public class RelationshipListing : BaseHierarchicalCodeElement
{
    public RelationshipListing() : base("relationships")
    {
    }

    public RelationshipListing(IEnumerable<CodeElement> elements) : this()
    {
        foreach (var element in elements)
        {
            AddChild(element);
        }
    }
}