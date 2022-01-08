using System.Diagnostics;

namespace Dbot.Domain;

[DebuggerDisplay("Projection: {Name}")]
public class Projection : BaseHierarchicalCodeElement
{
    public Projection(string name) : base(name)
    {
    }
}
