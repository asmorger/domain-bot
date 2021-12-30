using Superpower;
using Superpower.Model;
using Superpower.Parsers;

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
            .Or(Span.EqualTo("value").Value(Parsing.Identifier.ValueObject))
        ;
    public static TextParser<Unit> Keyword { get; } =
        Span.EqualTo("system")
            .Or(Span.EqualTo("aggregate"))
            .Or(Span.EqualTo("entity"))
            .Or(Span.EqualTo("value"))
            .Value(Unit.Value);
}