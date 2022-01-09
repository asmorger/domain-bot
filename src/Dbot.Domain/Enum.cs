using System.Diagnostics;

namespace Dbot.Domain;

[DebuggerDisplay("Enum: {Name}")]
public class Enum : CodeElement
{
    public Enum(string name) => Name = name;

    public string Name { get; }
}

[DebuggerDisplay("enums")]
public class EnumListing : BaseHierarchicalCodeElement
{
    public EnumListing() : base("enums")
    {
    }

    public EnumListing(IEnumerable<CodeElement> elements) : this()
    {
        foreach(var element in elements)
        {
            AddChild(element);
        }
    }
}
