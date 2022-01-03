namespace DBot.Dsl.Expressions;

public class ListingNodeValue : Expression
{
    public ListingNodeValue(KeywordValue keyword, Expression[] children)
    {
        Keyword = keyword;
        Children = children;
    }

    public KeywordValue Keyword { get; init; }
    public Expression[] Children { get; init; }

    public bool HasChildren => Children.Any();

    public override string ToString() => $"{Keyword}with {Children.Length} items";
}