using System.Collections;
using System.Diagnostics;

namespace DBot.Domain;

[DebuggerDisplay("System: {Name}")]
public class SoftwareSystem : HierarchicalCodeElement, IEnumerable<CodeElement>
{
    public string Name { get; }
    private readonly List<CodeElement> _elements = new();

    public SoftwareSystem(string name)
    {
        Name = name;
    }
    
    public void AddChild(CodeElement element) => _elements.Add(element);
    public IEnumerator<CodeElement> GetEnumerator() => _elements.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
    
}