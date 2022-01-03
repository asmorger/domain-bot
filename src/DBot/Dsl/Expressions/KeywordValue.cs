using DBot.Dsl.Parsing;

namespace DBot.Dsl.Expressions;

public class KeywordValue : Expression
{
    public KeywordValue(Keyword value)
    {
        Value = value;
    }

    public Keyword Value { get; init; }

    public override string ToString() => Value.ToString();
}