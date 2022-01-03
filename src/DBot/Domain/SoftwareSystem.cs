using System.Diagnostics;

namespace DBot.Domain;

[DebuggerDisplay("System: {Name}")]
public class SoftwareSystem : BaseHierarchicalCodeElement
{
    public SoftwareSystem(string name) : base(name)
    {
    }
}