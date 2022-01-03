using System.Collections;
using System.Diagnostics;

namespace DBot.Domain;

[DebuggerDisplay("Aggregate: {Name}")]
public class AggregateRoot : BaseHierarchicalCodeElement
{
    public AggregateRoot(string name) : base(name)
    {
    }
}