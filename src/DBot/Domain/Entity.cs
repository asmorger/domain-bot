using System.Diagnostics;

namespace DBot.Domain;

[DebuggerDisplay("Entity: {Name}")]
public class Entity : CodeElement
{
    public string Name { get; }

    public Entity(string name)
    {
        Name = name;
    }
}