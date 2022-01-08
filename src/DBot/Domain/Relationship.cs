using System.Diagnostics;

namespace DBot.Domain;

[DebuggerDisplay("Relationship to {Name}")]
public class Relationship : CodeElement
{
    public enum RelationshipType
    {
        OneToMany,
        OneToOne
    }

    public Relationship(RelationshipType type, string name)
    {
        Type = type;
        Name = name;
    }

    public RelationshipType Type { get; }
    public string Name { get; }
}

[DebuggerDisplay("relationships")]
public class RelationshipListing : BaseHierarchicalCodeElement
{
    public RelationshipListing() : base("relationships")
    {
    }

    public RelationshipListing(IEnumerable<CodeElement> elements) : this()
    {
        foreach(var element in elements)
        {
            AddChild(element);
        }
    }
}
