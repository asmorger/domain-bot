using System.Collections;

namespace DBot.Domain;

public class AggregateRoot : HierarchicalCodeElement, IEnumerable<CodeElement>
{
    public string Name { get; }
    private readonly List<CodeElement> _elements = new();

    public AggregateRoot(string name)
    {
        Name = name;
    }
    
    public void AddChild(CodeElement element) => _elements.Add(element);
    public IEnumerator<CodeElement> GetEnumerator() => _elements.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}