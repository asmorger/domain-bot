using DBot.Dsl.Expressions;

namespace DBot.Dsl.Parsing.Parsers;

using ExpressionTokenParser = TokenListParser<ExpressionToken, Expression>;

public class EnumParsers
{
    private static ExpressionTokenParser EnumArray { get; } =
        from begin in Token.EqualTo(ExpressionToken.LBracket)
        from values in Parse.Ref(() => UniversalParsers.String)
            .ManyDelimitedBy(Token.EqualTo(ExpressionToken.Comma),
                Token.EqualTo(ExpressionToken.RBracket))
        select (Expression)new ChildNodes(values);

    public static ExpressionTokenParser Enums { get; } =
        from keyword in Token.EqualTo(ExpressionToken.Enum)
        from enumName in UniversalParsers.String
        from value in EnumArray
        select (Expression)new TripletValue(new KeywordValue(Keyword.Enum), (NameValue)enumName, ((ChildNodes)value).Children);
}
