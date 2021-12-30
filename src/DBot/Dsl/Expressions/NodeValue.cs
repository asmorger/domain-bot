namespace DBot.Dsl.Expressions;

public class NodeValue : Expression
{
    public NodeValue(IdentifierValue identifier, NameValue name, Expression[] children)
    {
        Identifier = identifier;
        Name = name;
        Children = children;
    }

    public IdentifierValue Identifier { get; init; }
    public NameValue Name { get; init; }
    public Expression[] Children { get; init; }

    public override string ToString() => $"{Identifier}:{Name} with {Children.Length} items";
}

public class ChildNodes : Expression
{
    public ChildNodes(Expression[] children)
    {
        Children = children;
    }

    public Expression[] Children { get; init; }
    public int Count => Children.Length;
    
    public override string ToString() => $"{Count} items";
}