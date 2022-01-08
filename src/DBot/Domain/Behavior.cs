using System.Diagnostics;

namespace DBot.Domain;

[DebuggerDisplay("Behavior: {Name} raises {EventToBeRaised}")]
public class Behavior : CodeElement
{
    public Behavior(string name, string eventToBeRaised)
    {
        Name = name;
        EventToBeRaised = eventToBeRaised;
    }

    public string EventToBeRaised { get; }

    public string Name { get; }
}

[DebuggerDisplay("behaviors")]
public class BehaviorListing : BaseHierarchicalCodeElement
{
    public BehaviorListing() : base("behaviors")
    {
    }

    public BehaviorListing(IEnumerable<CodeElement> elements) : this()
    {
        foreach(var element in elements)
        {
            AddChild(element);
        }
    }
}
