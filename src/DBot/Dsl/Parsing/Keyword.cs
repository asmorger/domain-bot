using Superpower;
using Superpower.Parsers;
using Superpower.Tokenizers;

namespace DBot.Dsl.Parsing;

public enum Keyword
{
    System,
    AggregateRoot,
    Entity,
    ValueObject
}
public static class ExpressionTextParsers
{
    public static TextParser<Keyword> Keyword { get; } =
        Span.EqualTo("system").Value(Parsing.Keyword.System)
            .Or(Span.EqualTo("aggregate").Value(Parsing.Keyword.AggregateRoot))
            .Or(Span.EqualTo("entity").Value(Parsing.Keyword.Entity))
            .Or(Span.EqualTo("value").Value(Parsing.Keyword.ValueObject));

    public static TokenizerBuilder<ExpressionToken> MatchKeywords(this TokenizerBuilder<ExpressionToken> builder) =>
        builder
            .Match(Span.EqualTo("system"), ExpressionToken.System)
            .Match(Span.EqualTo("aggregate"), ExpressionToken.Aggregate)
            .Match(Span.EqualTo("entity"), ExpressionToken.Entity)
            .Match(Span.EqualTo("value"), ExpressionToken.ValueObject);
}