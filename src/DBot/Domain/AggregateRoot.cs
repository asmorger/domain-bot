using System.Diagnostics;

namespace DBot.Domain;

[DebuggerDisplay("Aggregate: {Name}")]
public class AggregateRoot : Entity
{
    public AggregateRoot(string name) : base(name)
    {
    }
}