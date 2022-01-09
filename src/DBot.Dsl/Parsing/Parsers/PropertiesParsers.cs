using DBot.Dsl.Expressions;

namespace DBot.Dsl.Parsing.Parsers;

using ExpressionTokenParser = TokenListParser<ExpressionToken, Expression>;

public class PropertiesParsers
{
    private static ExpressionTokenParser PropertyValue { get; } =
        from name in UniversalParsers.String
        from value in UniversalParsers.String
        select (Expression)new PropertyValue(name, value);

    private static ExpressionTokenParser PropertiesArray { get; } =
        from begin in Token.EqualTo(ExpressionToken.LBracket)
        from values in Parse.Ref(() => PropertyValue)
            .ManyDelimitedBy(Token.EqualTo(ExpressionToken.Comma),
                Token.EqualTo(ExpressionToken.RBracket))
        select (Expression)new ChildNodes(values);

    public static ExpressionTokenParser Properties { get; } =
        from keyword in Token.EqualTo(ExpressionToken.Structure)
        from array in PropertiesArray
        select (Expression)new CoupletValue(new KeywordValue(Keyword.Structure), ((ChildNodes)array).Children);
}
