using Superpower;
using Superpower.Model;
using Superpower.Parsers;
using Superpower.Tokenizers;

namespace DBot.Dsl.Parsing;

public static class ExpressionTokenizer
{
    private static TextParser<Unit> QuotedString { get; } =
        from open in Character.EqualTo('"')
        from content in Character.EqualTo('\\').IgnoreThen(Character.AnyChar).Value(Unit.Value).Try()
            .Or(Character.Except('"').Value(Unit.Value))
            .IgnoreMany()
        from close in Character.EqualTo('"')
        select Unit.Value;

    private static Tokenizer<ExpressionToken> Tokenizer { get; } = new TokenizerBuilder<ExpressionToken>()
        .Match(Character.EqualTo('{'), ExpressionToken.LBracket)
        .Match(Character.EqualTo('}'), ExpressionToken.RBracket)
        .Match(Character.EqualTo(','), ExpressionToken.Comma)
        .MatchIdentifiers()
        .Match(QuotedString, ExpressionToken.Name)
        .Match(Span.NonWhiteSpace, ExpressionToken.Name)
        .Ignore(Span.WhiteSpace)
        .Build();
    
    public static Result<TokenList<ExpressionToken>> TryTokenize(string source) =>
        Tokenizer.TryTokenize(source);
}