namespace DBot.Dsl.Expressions;

public class PropertyValue : Expression
{
    public Expression Type { get; }
    public Expression Name { get; }

    public PropertyValue(Expression type, Expression name)
    {
        Type = type;
        Name = name;
    }

    public override string ToString() => $"{Type}: {Name}";
}