using System.Diagnostics;

namespace DBot.Domain;

[DebuggerDisplay("Entity: {Name}")]
public class Entity : BaseHierarchicalCodeElement
{
    public Entity(string name) : base(name)
    {
    }
}