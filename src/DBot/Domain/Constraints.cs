namespace DBot.Domain;

public interface CodeElement
{
    string Name { get; }
}

public interface HierarchicalCodeElement : CodeElement, IEnumerable<CodeElement>
{
    void AddChild(CodeElement element);
}
