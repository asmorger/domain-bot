using DBot.Domain;
using DBot.Dsl.Parsing;

namespace DBot.Dsl.Expressions;

public class RelationshipValue : Expression
{
    public RelationshipValue(ExpressionToken token, Expression relationshipTarget)
    {
        Type = Convert(token);
        Target = relationshipTarget;
    }
    
    public Relationship.RelationshipType Type { get; }
    public Expression Target { get; }

    private static Relationship.RelationshipType Convert(ExpressionToken token) => token switch
    {
        ExpressionToken.OneToManyRelationship => Relationship.RelationshipType.OneToMany,
        ExpressionToken.OneToOneRelationship => Relationship.RelationshipType.ManyToMany,
        _ => throw new ArgumentOutOfRangeException(nameof(token), token, null)
    };
}