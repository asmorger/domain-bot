using Superpower.Tokenizers;

namespace DBot.Dsl.Parsing;

public enum Keyword
{
    AggregateRoot,
    Behaviors,
    Description,
    Entity,
    Events,
    None,
    Projection,
    Structure,
    Raises,
    Relationships,
    Returns,
    Service,
    System,
    ValueObject
}

public static class ExpressionTextParsers
{
    public static TextParser<Keyword> Keyword { get; } =
        Span.EqualTo("aggregate").Value(Parsing.Keyword.AggregateRoot)
            .Or(Span.EqualTo("behaviors").Value(Parsing.Keyword.Behaviors))
            .Or(Span.EqualTo("description").Value(Parsing.Keyword.Description))
            .Try().Or(Span.EqualTo("dto").Value(Parsing.Keyword.Projection))
            .Or(Span.EqualTo("entity").Value(Parsing.Keyword.Entity))
            .Try().Or(Span.EqualTo("events").Value(Parsing.Keyword.Events))
            .Or(Span.EqualTo("none").Value(Parsing.Keyword.None))
            .Or(Span.EqualTo("projection").Value(Parsing.Keyword.Projection))
            .Or(Span.EqualTo("raises").Value(Parsing.Keyword.Raises))
            .Try().Or(Span.EqualTo("relationships").Value(Parsing.Keyword.Relationships))
            .Try().Or(Span.EqualTo("returns").Value(Parsing.Keyword.Returns))
            .Or(Span.EqualTo("service").Value(Parsing.Keyword.Service))
            .Try().Or(Span.EqualTo("structure").Value(Parsing.Keyword.Structure))
            .Try().Or(Span.EqualTo("system").Value(Parsing.Keyword.System))
            .Or(Span.EqualTo("value").Value(Parsing.Keyword.ValueObject));

    public static TokenizerBuilder<ExpressionToken> MatchKeywords(this TokenizerBuilder<ExpressionToken> builder) =>
        builder
            .Match(Span.EqualTo("system"), ExpressionToken.System)
            .Match(Span.EqualTo("aggregate"), ExpressionToken.Aggregate)
            .Match(Span.EqualTo("behaviors"), ExpressionToken.Behavior)
            .Match(Span.EqualTo("description"), ExpressionToken.Description)
            .Match(Span.EqualTo("dto"), ExpressionToken.Projection)
            .Match(Span.EqualTo("entity"), ExpressionToken.Entity)
            .Match(Span.EqualTo("events"), ExpressionToken.Events)
            .Match(Span.EqualTo("none"), ExpressionToken.None)
            .Match(Span.EqualTo("projection"), ExpressionToken.Projection)
            .Match(Span.EqualTo("raises"), ExpressionToken.Raises)
            .Match(Span.EqualTo("relationships"), ExpressionToken.Relationships)
            .Match(Span.EqualTo("returns"), ExpressionToken.Returns)
            .Match(Span.EqualTo("service"), ExpressionToken.Service)
            .Match(Span.EqualTo("structure"), ExpressionToken.Structure)
            .Match(Span.EqualTo("value"), ExpressionToken.ValueObject);
}
