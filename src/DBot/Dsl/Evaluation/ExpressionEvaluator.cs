using DBot.Domain;
using DBot.Dsl.Expressions;
using DBot.Dsl.Parsing;

namespace DBot.Dsl.Evaluation;

public static class ExpressionEvaluator
{
    public static CodeElement Evaluate(Expression expression)
    {
        if (expression is not ExpressionWithChildren)
        {
            throw new ArgumentException($"Unsupported expression {expression}.");
        }

        var system = EvaluateCodeHierarchy(expression);
        return system;
    }

    static CodeElement EvaluateCodeHierarchy(Expression expression)
    {
        var node = EvaluateExpression(expression);

        if (expression is not ExpressionWithChildren expressionWithChildren)
        {
            return node;
        }

        if (node is not HierarchicalCodeElement parent)
        {
            return node;
        }

        foreach (var child in expressionWithChildren.Children)
        {
            var childNode = EvaluateCodeHierarchy(child);
            parent.AddChild(childNode);
        }

        return node;
    }
    
    static CodeElement EvaluateExpression(Expression expression) => expression switch
    {
        CoupletValue v => v.Keyword.Value switch {
            Keyword.Behaviors => new BehaviorListing(v.Children.Cast<RaisesValue>().Select(x => new Behavior(x.BehaviorName.ToString()!, x.EventName.ToString()!))),
            Keyword.Events => new EventListing(v.Children.Select(x => new Event(x.ToString()!))),
            Keyword.Description => new Description(v.Children.First().ToString()!),
            Keyword.Properties => new PropertyListing(v.Children.Select(x => new Property(x.ToString()!))),
            _ => throw new ArgumentOutOfRangeException()
        },
        TripletValue v => v.Keyword.Value switch {
            Keyword.System => new SoftwareSystem(v.Name.Value),
            Keyword.AggregateRoot => new AggregateRoot(v.Name.Value),
            Keyword.Entity => new Entity(v.Name.Value),
            Keyword.Projection => new Projection(v.Name.Value),
            Keyword.Service => new ServiceListing(v.Name.Value),
            Keyword.ValueObject => new ValueObject(v.Name.Value),
            _ => throw new ArgumentOutOfRangeException()
        },
        // these should never be top-level things :shrug:
        NameValue => throw new NotImplementedException(),
        ChildNodes => throw new NotImplementedException(),
        KeywordValue => throw new NotImplementedException(),
        MethodValue x => new ServiceMethod(x.Name.ToString()!, x.ReturnType.ToString()!),
        _ => throw new ArgumentOutOfRangeException(nameof(expression))
    };
}