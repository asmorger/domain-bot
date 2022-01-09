using System.Diagnostics;

namespace Dbot.Domain;

[DebuggerDisplay("Value Object: {Name}")]
public class ValueObject : CodeElement
{
    public ValueObject(string name) => Name = name;

    public string Name { get; }
}
