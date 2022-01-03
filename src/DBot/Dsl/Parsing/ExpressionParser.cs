using System.Diagnostics.CodeAnalysis;
using DBot.Dsl.Expressions;
using DBot.Dsl.Parsing.Parsers;
using Superpower;
using Superpower.Model;
using Superpower.Parsers;
using ExpressionTokenParser =
    Superpower.TokenListParser<DBot.Dsl.Parsing.ExpressionToken, DBot.Dsl.Expressions.Expression>;


namespace DBot.Dsl.Parsing;

public static class ExpressionParser
{
    private static readonly ExpressionTokenParser Expression =
        DslValue
            .Named("DSL value");

    private static ExpressionTokenParser Array { get; } =
        from begin in Token.EqualTo(ExpressionToken.LBracket)
        from values in Parse.Ref(() => DslValue)
            .ManyDelimitedBy(Token.EqualTo(ExpressionToken.Comma),
                Token.EqualTo(ExpressionToken.RBracket))
        select (Expression) new ChildNodes(values);

    private static ExpressionTokenParser KeywordTriplet { get; } =
        Parse.Chain(UniversalParsers.String, Array.Or(UniversalParsers.Keyword),
            (name, identifier, array) =>
                new TripletValue((KeywordValue) identifier, (NameValue) name, ((ChildNodes) array).Children));

    private static ExpressionTokenParser EventsCouplet { get; } =
        from keyword in Token.EqualTo(ExpressionToken.Events)
        from array in Array
        select (Expression) new CoupletValue(new KeywordValue(Keyword.Events), ((ChildNodes) array).Children);

    private static ExpressionTokenParser DescriptionCouplet { get; } =
        from keyword in Token.EqualTo(ExpressionToken.Description)
        from value in UniversalParsers.String
        select (Expression) new CoupletValue(new KeywordValue(Keyword.Description), new[] {value});

    private static ExpressionTokenParser DslValue { get; } =
        EventsCouplet
            .Or(DescriptionCouplet)
            .Or(PropertiesParsers.Properties)
            .Or(BehaviorsParsers.Behaviors)
            .Or(KeywordTriplet)
            .Or(UniversalParsers.String);

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