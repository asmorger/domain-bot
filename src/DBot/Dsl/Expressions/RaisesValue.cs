namespace DBot.Dsl.Expressions;

public class RaisesValue : Expression
{
    public RaisesValue(Expression behaviorName, Expression eventName)
    {
        BehaviorName = behaviorName;
        EventName = eventName;
    }

    public Expression BehaviorName { get; init; }
    public Expression EventName { get; init; }
}