namespace DBot.Dsl.Expressions;

public class ListingNodeValue : Expression
{
    public ListingNodeValue(IdentifierValue identifier, Expression[] children)
    {
        Identifier = identifier;
        Children = children;
    }

    public IdentifierValue Identifier { get; init; }
    public Expression[] Children { get; init; }

    public bool HasChildren => Children.Any();

    public override string ToString() => $"{Identifier}with {Children.Length} items";
}