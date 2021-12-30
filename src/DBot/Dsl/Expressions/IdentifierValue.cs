using DBot.Dsl.Parsing;

namespace DBot.Dsl.Expressions;

public class IdentifierValue : Expression
{
    public IdentifierValue(Identifier value)
    {
        Value = value;
    }

    public Identifier Value { get; init; }

    public override string ToString() => Value.ToString();
}