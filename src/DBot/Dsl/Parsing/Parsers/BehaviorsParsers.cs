using DBot.Dsl.Expressions;
using Superpower;
using Superpower.Parsers;

namespace DBot.Dsl.Parsing.Parsers;

using ExpressionTokenParser = TokenListParser<ExpressionToken, Expression>;

public static class BehaviorsParsers
{
    private static ExpressionTokenParser RaisesTriplet { get; } =
        Parse.Chain(
            Token.EqualTo(ExpressionToken.Raises).Or(Token.EqualTo(ExpressionToken.Returns)), 
            UniversalParsers.String,
            (_, behaviorName, eventToBeRaised) => new RaisesValue(behaviorName, eventToBeRaised));

    private static ExpressionTokenParser RaisesArray { get; } =
        from begin in Token.EqualTo(ExpressionToken.LBracket)
        from values in Parse.Ref(() => RaisesTriplet)
            .ManyDelimitedBy(Token.EqualTo(ExpressionToken.Comma),
                Token.EqualTo(ExpressionToken.RBracket))
        select (Expression) new ChildNodes(values);

    public static ExpressionTokenParser Behaviors { get; } =
        from keyword in Token.EqualTo(ExpressionToken.Behavior)
        from value in RaisesArray
        select (Expression) new CoupletValue(new KeywordValue(Keyword.Behaviors), ((ChildNodes) value).Children);
}