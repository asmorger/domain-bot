using DBot.Domain;
using DBot.Dsl.Expressions;
using DBot.Dsl.Parsing;

namespace DBot.Dsl.Evaluation;

public class ExpressionEvaluator
{
    public static CodeElement Evaluate(Expression expression)
    {
        if (expression is not NodeValue node)
        {
            throw new ArgumentException($"Unsupported expression {expression}.");
        }

        var system = EvaluateCodeHierarchy(node);
        return system;
    }

    static CodeElement EvaluateCodeHierarchy(NodeValue expression)
    {
        var node = EvaluateExpression(expression);

        if (!expression.HasChildren)
        {
            return node;
        }

        if (node is not HierarchicalCodeElement parent)
        {
            throw new EvaluationException($"The element {expression.Name} may contain any child items.");
        }

        foreach (var child in expression.Children)
        {
            var childNode = EvaluateCodeHierarchy((NodeValue) child);
            parent.AddChild(childNode);
        }

        return node;
    }
    
    static CodeElement EvaluateExpression(Expression expression) => expression switch
    {
        ListingNodeValue v => v.Keyword.Value switch {
            // Identifier.Events => new EventListing(v.Children.Select(x => new Event(x.ToString()!))),
            _ => throw new ArgumentOutOfRangeException()
        },
        NodeValue v => v.Keyword.Value switch {
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