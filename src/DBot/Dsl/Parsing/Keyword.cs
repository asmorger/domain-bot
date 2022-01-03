using Superpower;
using Superpower.Parsers;
using Superpower.Tokenizers;

namespace DBot.Dsl.Parsing;

public enum Keyword
{
    System,
    AggregateRoot,
    Behaviors,
    Description,
    Entity,
    Events,
    Properties,
    Raises,
    ValueObject
}
public static class ExpressionTextParsers
{
    public static TextParser<Keyword> Keyword { get; } =
        Span.EqualTo("system").Value(Parsing.Keyword.System)
            .Or(Span.EqualTo("aggregate").Value(Parsing.Keyword.AggregateRoot))
            .Or(Span.EqualTo("behaviors").Value(Parsing.Keyword.Behaviors))
            .Or(Span.EqualTo("description").Value(Parsing.Keyword.Description))
            .Or(Span.EqualTo("entity").Value(Parsing.Keyword.Entity))
                .Try()
                .Or(Span.EqualTo("events").Value(Parsing.Keyword.Events))
            .Or(Span.EqualTo("properties").Value(Parsing.Keyword.Properties))
            .Or(Span.EqualTo("raises").Value(Parsing.Keyword.Raises))
            .Or(Span.EqualTo("value").Value(Parsing.Keyword.ValueObject));

    public static TokenizerBuilder<ExpressionToken> MatchKeywords(this TokenizerBuilder<ExpressionToken> builder) =>
        builder
            .Match(Span.EqualTo("system"), ExpressionToken.System)
            .Match(Span.EqualTo("aggregate"), ExpressionToken.Aggregate)
            .Match(Span.EqualTo("behaviors"), ExpressionToken.Behavior)
            .Match(Span.EqualTo("description"), ExpressionToken.Description)
            .Match(Span.EqualTo("entity"), ExpressionToken.Entity)
            .Match(Span.EqualTo("events"), ExpressionToken.Events)
            .Match(Span.EqualTo("properties"), ExpressionToken.Properties)
            .Match(Span.EqualTo("raises"), ExpressionToken.Raises)
            .Match(Span.EqualTo("value"), ExpressionToken.ValueObject);
}