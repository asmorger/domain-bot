using System.Diagnostics;

namespace DBot.Domain;

[DebuggerDisplay("Projection: {Name}")]
public class Projection : BaseHierarchicalCodeElement
{
    public Projection(string name) : base(name)
    {
    }
}
