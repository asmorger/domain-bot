namespace DBot.Dsl.Expressions;

public abstract class Expression
{
}

public interface ExpressionWithChildren
{
    Expression[] Children { get; }

    bool HasChildren();
}
