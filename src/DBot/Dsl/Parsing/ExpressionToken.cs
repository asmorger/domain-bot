using Superpower.Display;

namespace DBot.Dsl.Parsing;

public enum ExpressionToken
{
    String,
    Comma,
    NewLine,
    [Token(Category = "keyword")] System,
    [Token(Category = "keyword")] Aggregate,
    [Token(Category = "keyword")] Description,
    [Token(Category = "keyword")] Entity,
    [Token(Category = "keyword")] Events,
    [Token(Category = "keyword")] Properties,
    [Token(Category = "keyword")] ValueObject,
    [Token(Example = "{")] LBracket,
    [Token(Example = "}")] RBracket
}