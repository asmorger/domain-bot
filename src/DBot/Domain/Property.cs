using System.Diagnostics;

namespace DBot.Domain;

[DebuggerDisplay("Property: {Name}")]
public class Property : CodeElement
{
    public Property(string name)
    {
        Name = name;
    }

    public string Name { get; }
}

[DebuggerDisplay("properties")]
public class PropertyListing : BaseHierarchicalCodeElement
{
    public PropertyListing() : base("properties")
    {
    }

    public PropertyListing(IEnumerable<CodeElement> elements) : this()
    {
        foreach (var element in elements)
        {
            AddChild(element);
        }
    }
}