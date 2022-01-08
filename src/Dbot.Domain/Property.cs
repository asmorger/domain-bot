using System.Diagnostics;

namespace Dbot.Domain;

[DebuggerDisplay("Property: {Name}")]
public class Property : CodeElement
{
    public Property(string type, string name)
    {
        Type = type;
        Name = name;
    }

    public string Type { get; }
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
        foreach(var element in elements)
        {
            AddChild(element);
        }
    }
}
