using System.Diagnostics.CodeAnalysis;
using DBot.Dsl.Expressions;
using DBot.Dsl.Parsing.Parsers;
using ExpressionTokenParser = Superpower.TokenListParser<DBot.Dsl.Parsing.ExpressionToken, DBot.Dsl.Expressions.Expression>;

namespace DBot.Dsl.Parsing;

public static class ExpressionParser
{
    private static ExpressionTokenParser Array { get; } =
        from begin in Token.EqualTo(ExpressionToken.LBracket)
        from values in Parse.Ref(() => Dsl!)
            .ManyDelimitedBy(Token.EqualTo(ExpressionToken.Comma),
                Token.EqualTo(ExpressionToken.RBracket))
        select (Expression)new ChildNodes(values);

    private static ExpressionTokenParser CodeElementKeyword { get; } =
        Parse.Chain(UniversalParsers.String, Array.Or(UniversalParsers.Keyword),
            (name, identifier, array) =>
                new TripletValue((KeywordValue)identifier, (NameValue)name, ((ChildNodes)array).Children));
    private static ExpressionTokenParser Events { get; } =
        from keyword in Token.EqualTo(ExpressionToken.Events)
        from array in Array
        select (Expression)new CoupletValue(new KeywordValue(Keyword.Events), ((ChildNodes)array).Children);

    private static ExpressionTokenParser Description { get; } =
        from keyword in Token.EqualTo(ExpressionToken.Description)
        from value in UniversalParsers.String
        select (Expression)new CoupletValue(new KeywordValue(Keyword.Description), new[] {value});

    private static ExpressionTokenParser Dsl { get; } =
        Events
            .Or(Description)
            .Or(EnumParsers.Enums)
            .Or(PropertiesParsers.Properties)
            .Or(BehaviorsParsers.Behaviors)
            .Or(ServicesParsers.Services)
            .Or(RelationshipParsers.Relationships)
            .Or(CodeElementKeyword)
            .Or(UniversalParsers.String);

    private static ExpressionTokenParser Source { get; } = Dsl.Named("DSL value").AtEnd();

    public static bool TryParse(TokenList<ExpressionToken> tokens, [NotNullWhen(true)] out Expression? expr,
        [NotNullWhen(false)] out string? error, out Position errorPosition)
    {
        var result = Source(tokens);
        if(!result.HasValue)
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
