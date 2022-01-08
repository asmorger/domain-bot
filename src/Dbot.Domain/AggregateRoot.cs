using System.Diagnostics;

namespace Dbot.Domain;

[DebuggerDisplay("Aggregate: {Name}")]
public class AggregateRoot : Entity
{
    public AggregateRoot(string name) : base(name)
    {
    }
}
