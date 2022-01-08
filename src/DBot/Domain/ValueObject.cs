using System.Diagnostics;

namespace DBot.Domain;

[DebuggerDisplay("Value Object: {Name}")]
public class ValueObject : CodeElement
{
    public ValueObject(string name) => Name = name;

    public string Name { get; }
}
