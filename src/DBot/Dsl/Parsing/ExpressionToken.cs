using Superpower.Display;

namespace DBot.Dsl.Parsing;

public enum ExpressionToken
{
    Name,
    Comma,
    [Token(Category = "keyword")] System,
    [Token(Category = "keyword")] Aggregate,
    [Token(Category = "keyword")] Entity,
    [Token(Category = "keyword")] ValueObject,
    [Token(Example = "{")] LBracket,
    [Token(Example = "}")] RBracket
}