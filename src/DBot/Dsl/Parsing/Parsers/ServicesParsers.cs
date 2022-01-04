using DBot.Dsl.Expressions;
using Superpower;
using Superpower.Parsers;

namespace DBot.Dsl.Parsing.Parsers;
using ExpressionTokenParser = TokenListParser<ExpressionToken, Expression>;

public class ServicesParsers
{
    private static ExpressionTokenParser MethodValues { get; } =
        Parse.Chain(Token.EqualTo(ExpressionToken.Returns), UniversalParsers.String,
            (name, methodName, returnType) =>
                new MethodValue(returnType, methodName));

    private static ExpressionTokenParser ServiceArray { get; } =
        from begin in Token.EqualTo(ExpressionToken.LBracket)
        from values in Parse.Ref(() => MethodValues)
            .ManyDelimitedBy(Token.EqualTo(ExpressionToken.Comma),
                Token.EqualTo(ExpressionToken.RBracket))
        select (Expression) new ChildNodes(values);

    public static ExpressionTokenParser Services { get; } =
        from keyword in Token.EqualTo(ExpressionToken.Service)
        from serviceName in UniversalParsers.String
        from value in ServiceArray
        select (Expression) new TripletValue(new KeywordValue(Keyword.Service), (NameValue)serviceName, ((ChildNodes) value).Children);

}