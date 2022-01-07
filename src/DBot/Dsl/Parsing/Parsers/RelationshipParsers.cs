using DBot.Dsl.Expressions;
using Superpower;
using Superpower.Parsers;

namespace DBot.Dsl.Parsing.Parsers;

using ExpressionTokenParser = TokenListParser<ExpressionToken, Expression>;

public static class RelationshipParsers
{
    private static ExpressionTokenParser Relationship { get; } =
        from type in 
            Token.EqualTo(ExpressionToken.OneToManyRelationship)
                .Or(Token.EqualTo(ExpressionToken.OneToOneRelationship))
        from value in UniversalParsers.String
        select (Expression) new RelationshipValue(type.Kind, value);

    private static ExpressionTokenParser RelationshipArray { get; } =
        from begin in Token.EqualTo(ExpressionToken.LBracket)
        from values in Parse.Ref(() => Relationship)
            .ManyDelimitedBy(Token.EqualTo(ExpressionToken.Comma),
                Token.EqualTo(ExpressionToken.RBracket))
        select (Expression) new ChildNodes(values);

    public static ExpressionTokenParser Relationships { get; } =
        from keyword in Token.EqualTo(ExpressionToken.Relationships)
        from value in RelationshipArray
        select (Expression) new CoupletValue(new KeywordValue(Keyword.Relationships), ((ChildNodes) value).Children);
}