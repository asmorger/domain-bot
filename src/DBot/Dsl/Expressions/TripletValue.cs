namespace DBot.Dsl.Expressions;

public class TripletValue : Expression
{
    public TripletValue(KeywordValue keyword, NameValue name, Expression[] children)
    {
        Keyword = keyword;
        Name = name;
        Children = children;
    }

    public KeywordValue Keyword { get; init; }
    public NameValue Name { get; init; }
    public Expression[] Children { get; init; }

    public bool HasChildren => Children.Any();

    public override string ToString() => $"{Keyword}:{Name} with {Children.Length} items";
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