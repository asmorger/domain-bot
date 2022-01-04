namespace DBot.Dsl.Expressions;

public class MethodValue : Expression
{
    public MethodValue(Expression returnType, Expression name)
    {
        ReturnType = returnType;
        Name = name;
    }

    public Expression ReturnType { get; }
    public Expression Name { get; }
}