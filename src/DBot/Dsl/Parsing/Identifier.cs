using Superpower;
using Superpower.Parsers;
using Superpower.Tokenizers;

namespace DBot.Dsl.Parsing;

public enum Identifier
{
    System,
    AggregateRoot,
    Entity,
    ValueObject
}
public static class ExpressionTextParsers
{
    public static TextParser<Identifier> Identifier { get; } =
        Span.EqualTo("system").Value(Parsing.Identifier.System)
            .Or(Span.EqualTo("aggregate").Value(Parsing.Identifier.AggregateRoot))
            .Or(Span.EqualTo("entity").Value(Parsing.Identifier.Entity))
            .Or(Span.EqualTo("value").Value(Parsing.Identifier.ValueObject));

    public static TokenizerBuilder<ExpressionToken> MatchIdentifiers(this TokenizerBuilder<ExpressionToken> builder) =>
        builder
            .Match(Span.EqualTo("system"), ExpressionToken.System)
            .Match(Span.EqualTo("aggregate"), ExpressionToken.Aggregate)
            .Match(Span.EqualTo("entity"), ExpressionToken.Entity)
            .Match(Span.EqualTo("value"), ExpressionToken.ValueObject);
}