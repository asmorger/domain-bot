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
    private static ExpressionTokenParser Keyword { get; } =
        Token.EqualTo(ExpressionToken.System)
            .Or(Token.EqualTo(ExpressionToken.Aggregate))
            .Or(Token.EqualTo(ExpressionToken.Description))
            .Or(Token.EqualTo(ExpressionToken.Entity))
            .Or(Token.EqualTo(ExpressionToken.Events))
            .Or(Token.EqualTo(ExpressionToken.ValueObject))
            .Apply(ExpressionTextParsers.Keyword)
            .Select(id => (Expression) new KeywordValue(id));

    private static ExpressionTokenParser String { get; } =
        Token.EqualTo(ExpressionToken.String)
            .Select(x => (Expression) new NameValue(x.ToStringValue().Trim('"')));

    private static ExpressionTokenParser Array { get; } =
        from begin in Token.EqualTo(ExpressionToken.LBracket)
        from values in Parse.Ref(() => DslValue)
            .ManyDelimitedBy(Token.EqualTo(ExpressionToken.Comma), 
                end: Token.EqualTo(ExpressionToken.RBracket))
        select (Expression) new ChildNodes(values);

    private static ExpressionTokenParser KeywordTriplet { get; } =
        Parse.Chain(String, Array.Or(Keyword),
            (name, identifier, array) =>
                new TripletValue((KeywordValue) identifier, (NameValue) name, ((ChildNodes) array).Children));

    private static ExpressionTokenParser EventsCouplet { get; } =
        from keyword in Token.EqualTo(ExpressionToken.Events)
        from array in Array
        select (Expression) new CoupletValue(new KeywordValue(Parsing.Keyword.Events), ((ChildNodes) array).Children);

    private static ExpressionTokenParser DslValue { get; } =
        EventsCouplet
            .Or(KeywordTriplet)
            .Or(String);

    private static ExpressionTokenParser Expression = 
        DslValue
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