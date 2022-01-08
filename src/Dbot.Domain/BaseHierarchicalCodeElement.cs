using System.Collections;

namespace Dbot.Domain;

public abstract class BaseHierarchicalCodeElement : HierarchicalCodeElement
{
    protected readonly List<CodeElement> _elements = new();

    protected BaseHierarchicalCodeElement(string name) => Name = name;

    public string Name { get; }

    public void AddChild(CodeElement element) => _elements.Add(element);
    public IEnumerator<CodeElement> GetEnumerator() => _elements.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
