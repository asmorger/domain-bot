namespace DBot.Dsl.Parsing;

public enum ExpressionToken
{
    String,
    Comma,
    NewLine,
    [Token(Category = "keyword")] System,
    [Token(Category = "keyword")] Aggregate,
    [Token(Category = "keyword")] Behavior,
    [Token(Category = "keyword")] Description,
    [Token(Category = "keyword")] Entity,
    [Token(Category = "keyword")] Enum,
    [Token(Category = "keyword")] Events,
    [Token(Category = "keyword")] None,
    [Token(Example = "->")] OneToManyRelationship,
    [Token(Example = "-|")] OneToOneRelationship,
    [Token(Category = "keyword")] Projection,
    [Token(Category = "keyword")] Structure,
    [Token(Category = "keyword")] Raises,
    [Token(Category = "keyword")] Relationships,
    [Token(Category = "keyword")] Returns,
    [Token(Category = "keyword")] Service,
    [Token(Category = "keyword")] ValueObject,
    [Token(Example = "{")] LBracket,
    [Token(Example = "}")] RBracket
}
