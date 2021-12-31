namespace DBot.Domain;

public class EventListing : BaseHierarchicalCodeElement
{
    public EventListing() : base("events")
    {
    }

    public EventListing(IEnumerable<CodeElement> elements) : this()
    {
        foreach (var element in elements)
        {
            AddChild(element);
        }
    }
}

public class Event : CodeElement
{
    public string Name { get; }

    public Event(string name)
    {
        Name = name;
    }
}