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
        var node = EvaluateNode(expression);

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
    
    

    static CodeElement EvaluateNode(NodeValue expression) => expression.Identifier.Value switch
    {
        Identifier.System => new SoftwareSystem(expression.Name.Value),
        Identifier.AggregateRoot => new AggregateRoot(expression.Name.Value),
        Identifier.Entity => new Entity(expression.Name.Value),
        Identifier.ValueObject => new ValueObject(expression.Name.Value),
        _ => throw new ArgumentException($"Unsupported expression {expression}.")
    };

}