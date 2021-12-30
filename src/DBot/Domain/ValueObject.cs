using System.Diagnostics;

namespace DBot.Domain;

[DebuggerDisplay("Value Object: {Name}")]
public class ValueObject : CodeElement
{
    public string Name { get; }

    public ValueObject(string name)
    {
        Name = name;
    }
}