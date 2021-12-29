using Superpower.Display;

namespace DBot.Dsl.Parsing;

public enum ExpressionToken
{
    String,
    [Token(Example = "{")] LBrace,
    [Token(Example = "}")] RBrace
}