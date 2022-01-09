namespace DBot.Dsl.Expressions;

public class CoupletValue : Expression
{
    public CoupletValue(KeywordValue keyword, Expression[] children)
    {
        Keyword = keyword;
        Children = children;
    }

    public KeywordValue Keyword { get; }
    public Expression[] Children { get; }
    public bool HasChildren() => Children.Any();
}
