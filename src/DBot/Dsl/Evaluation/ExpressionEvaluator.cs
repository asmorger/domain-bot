using DBot.Domain;
using DBot.Dsl.Expressions;
using DBot.Dsl.Parsing;

namespace DBot.Dsl.Evaluation;

public class ExpressionEvaluator
{
    public static CodeElement Evaluate(Expression expression)
    {
        if (expression is not ExpressionWithChildren node)
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
            Keyword.Events => new EventListing(v.Children.Select(x => new Event(x.ToString()!))),
            _ => throw new ArgumentOutOfRangeException()
        },
        TripletValue v => v.Keyword.Value switch {
            Keyword.System => new SoftwareSystem(v.Name.Value),
            Keyword.AggregateRoot => new AggregateRoot(v.Name.Value),
            Keyword.Entity => new Entity(v.Name.Value),
            Keyword.ValueObject => new ValueObject(v.Name.Value),
            _ => throw new ArgumentOutOfRangeException()
        },
        // these should never be top-level things :shrug:
        NameValue => throw new NotImplementedException(),
        ChildNodes => throw new NotImplementedException(),
        KeywordValue => throw new NotImplementedException(),
        _ => throw new ArgumentOutOfRangeException(nameof(expression))
    };
}