namespace DBot.Domain;

public interface CodeElement { }

public interface HierarchicalCodeElement : CodeElement
{
    void AddChild(CodeElement element);
}