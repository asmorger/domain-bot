using System.Diagnostics;

namespace DBot.Domain;

[DebuggerDisplay("Entity: {Name}")]
public class Entity : BaseHierarchicalCodeElement
{
    public Entity(string name) : base(name)
    {
    }

    public IEnumerable<Event> GetChildEvents()
    {
        foreach (var child in _elements)
        {
            if (child is Entity entity)
            {
                var events = entity.GetChildEvents();

                foreach (var childEvent in events)
                {
                    yield return childEvent;
                }
            }

            if (child is EventListing listing)
            {
                foreach (var e in listing)
                {
                    yield return (Event)e;
                }
            }
        }
    }
    
    public IEnumerable<Behavior> GetBehaviors()
    {
        foreach (var child in _elements)
        {
            if (child is Entity entity)
            {
                var events = entity.GetBehaviors();

                foreach (var childEvent in events)
                {
                    yield return childEvent;
                }
            }

            if (child is BehaviorListing listing)
            {
                foreach (var e in listing)
                {
                    yield return (Behavior)e;
                }
            }
        }
    }

    public IEnumerable<Property> GetProperties()
    {
        foreach (var child in _elements)
        {
            if (child is PropertyListing listing)
            {
                foreach (var e in listing)
                {
                    yield return (Property)e;
                }
            }
        }
    }
    
    public IEnumerable<Relationship> GetRelationships()
    {
        foreach (var child in _elements)
        {
            if (child is RelationshipListing listing)
            {
                foreach (var e in listing)
                {
                    yield return (Relationship)e;
                }
            }
        }
    }
}