using System.Diagnostics;

namespace DBot.Domain;

[DebuggerDisplay("events")]
public class EventListing : BaseHierarchicalCodeElement
{
    public EventListing() : base("events")
    {
    }

    public EventListing(IEnumerable<CodeElement> elements) : this()
    {
        foreach(var element in elements)
        {
            AddChild(element);
        }
    }
}

[DebuggerDisplay("Event: {Name}")]
public class Event : CodeElement
{
    public Event(string name) => Name = name;

    public string Name { get; }
}
