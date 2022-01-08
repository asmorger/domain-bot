using System.Diagnostics;

namespace Dbot.Domain;

[DebuggerDisplay("System: {Name}")]
public class SoftwareSystem : BaseHierarchicalCodeElement
{
    public SoftwareSystem(string name) : base(name)
    {
    }
}
