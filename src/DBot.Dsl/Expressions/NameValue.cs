namespace DBot.Dsl.Expressions;

public class NameValue : Expression
{
    public NameValue(string value) => Value = value;

    public string Value { get; init; }

    public override string ToString() => Value;
}
