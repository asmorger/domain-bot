using System.Diagnostics;

namespace Dbot.Domain;

[DebuggerDisplay("Enum Value: {Name}")]
public class EnumValue : CodeElement
{
    public EnumValue(string name) => Name = name;

    public string Name { get; }
}

[DebuggerDisplay("Enum: {Name}")]
public class EnumListing : BaseHierarchicalCodeElement
{
    public EnumListing(string name, IEnumerable<EnumValue> elements) : base(name)
    {
        foreach(var element in elements)
        {
            AddChild(element);
        }
    }
}
