using System.Diagnostics.CodeAnalysis;
using DBot.Dsl.Expressions;
using Superpower;
using Superpower.Model;
using Superpower.Parsers;
using Expression = DBot.Dsl.Expressions.Expression;
using ExpressionTokenParser = Superpower.TokenListParser<DBot.Dsl.Parsing.ExpressionToken, DBot.Dsl.Expressions.Expression>;


namespace DBot.Dsl.Parsing;

public static class ExpressionParser
{
    private static ExpressionTokenParser Identifier { get; } =
        Token.EqualTo(ExpressionToken.System)
            .Or(Token.EqualTo(ExpressionToken.Aggregate))
            .Or(Token.EqualTo(ExpressionToken.Entity))
            .Or(Token.EqualTo(ExpressionToken.ValueObject))
            .Apply(ExpressionTextParsers.Identifier)
            .Select(id => (Expression) new IdentifierValue(id));

    private static ExpressionTokenParser Name { get; } =
        Token.EqualTo(ExpressionToken.Name)
            .Select(x => (Expression) new NameValue(x.ToStringValue().Trim('"')));

    private static ExpressionTokenParser Node { get; } =
        from l in Identifier
        from r in Name
        from c in Array
        select (Expression) new NodeValue((IdentifierValue)l, (NameValue)r, ((ChildNodes)c).Children);

    private static ExpressionTokenParser Array { get; } =
        from begin in Token.EqualTo(ExpressionToken.LBracket)
        from values in Parse.Ref(() => Node)
            .ManyDelimitedBy(Token.EqualTo(ExpressionToken.Comma), 
                end: Token.EqualTo(ExpressionToken.RBracket))
            select (Expression) new ChildNodes(values);

    private static ExpressionTokenParser Expression = 
        Node
            .Named("DSL value");
    
    private static ExpressionTokenParser Source { get; } = Expression.AtEnd();
    
    public static bool TryParse(TokenList<ExpressionToken> tokens, [NotNullWhen(true)] out Expression? expr, 
        [NotNullWhen(false)] out string? error, out Position errorPosition)
    {
        var result = Source(tokens);
        if (!result.HasValue)
        {
            expr = null;
            error = result.ToString();
            errorPosition = result.ErrorPosition;
            return false;
        }

        expr = result.Value;
        error = null;
        errorPosition = Position.Empty;
        return true;
    }
}