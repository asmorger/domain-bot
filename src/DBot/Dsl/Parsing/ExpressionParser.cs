using System.Diagnostics.CodeAnalysis;
using DBot.Dsl.Expressions;
using DBot.Dsl.Parsing.Parsers;
using Superpower;
using Superpower.Model;
using Superpower.Parsers;
using Expression = DBot.Dsl.Expressions.Expression;
using ExpressionTokenParser = Superpower.TokenListParser<DBot.Dsl.Parsing.ExpressionToken, DBot.Dsl.Expressions.Expression>;


namespace DBot.Dsl.Parsing;

public static class ExpressionParser
{
    private static ExpressionTokenParser Array { get; } =
        from begin in Token.EqualTo(ExpressionToken.LBracket)
        from values in Parse.Ref(() => DslValue)
            .ManyDelimitedBy(Token.EqualTo(ExpressionToken.Comma), 
                end: Token.EqualTo(ExpressionToken.RBracket))
        select (Expression) new ChildNodes(values);
    
    private static ExpressionTokenParser PropertiesArray { get; } =
        from begin in Token.EqualTo(ExpressionToken.LBracket)
        from values in Parse.Ref(() => PropertyValue)
            .ManyDelimitedBy(Token.EqualTo(ExpressionToken.Comma), 
                end: Token.EqualTo(ExpressionToken.RBracket))
        select (Expression) new ChildNodes(values);
    
    private static ExpressionTokenParser RaisesArray { get; } =
        from begin in Token.EqualTo(ExpressionToken.LBracket)
        from values in Parse.Ref(() => RaisesTriplet)
            .ManyDelimitedBy(Token.EqualTo(ExpressionToken.Comma), 
                end: Token.EqualTo(ExpressionToken.RBracket))
        select (Expression) new ChildNodes(values);

    private static ExpressionTokenParser KeywordTriplet { get; } =
        Parse.Chain(UniversalParsers.String, Array.Or(UniversalParsers.Keyword),
            (name, identifier, array) =>
                new TripletValue((KeywordValue) identifier, (NameValue) name, ((ChildNodes) array).Children));

    private static ExpressionTokenParser EventsCouplet { get; } =
        from keyword in Token.EqualTo(ExpressionToken.Events)
        from array in Array
        select (Expression) new CoupletValue(new KeywordValue(Parsing.Keyword.Events), ((ChildNodes) array).Children);
    
    private static ExpressionTokenParser DescriptionCouplet { get; } =
        from keyword in Token.EqualTo(ExpressionToken.Description)
        from value in UniversalParsers.String
        select (Expression) new CoupletValue(new KeywordValue(Parsing.Keyword.Description), new []{ value });
    
    private static ExpressionTokenParser RaisesTriplet { get; } =
        Parse.Chain(Token.EqualTo(ExpressionToken.Raises), UniversalParsers.String,
            (name, behaviorName, eventToBeRaised) =>
                new RaisesValue(behaviorName, eventToBeRaised));
    
    private static ExpressionTokenParser BehaviorCouplet { get; } =
        from keyword in Token.EqualTo(ExpressionToken.Behavior)
        from value in RaisesArray
        select (Expression) new CoupletValue(new KeywordValue(Parsing.Keyword.Behaviors), ((ChildNodes) value).Children);

    private static ExpressionTokenParser PropertyValue { get; } =
        from name in UniversalParsers.String
        from value in UniversalParsers.String
        select (Expression) new PropertyValue(name, value);
    
    private static ExpressionTokenParser PropertiesCouplet { get; } =
        from keyword in Token.EqualTo(ExpressionToken.Properties)
        from array in PropertiesArray
        select (Expression) new CoupletValue(new KeywordValue(Parsing.Keyword.Properties), ((ChildNodes) array).Children);

    private static ExpressionTokenParser DslValue { get; } =
        EventsCouplet
            .Or(DescriptionCouplet)
            .Or(PropertiesCouplet)
            .Or(BehaviorCouplet)
            .Or(KeywordTriplet)
            .Or(UniversalParsers.String);

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