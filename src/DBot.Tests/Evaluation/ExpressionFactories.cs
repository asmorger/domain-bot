using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bogus.DataSets;
using DBot.Dsl.Expressions;
using DBot.Dsl.Parsing;
using Xunit.Sdk;

namespace DBot.Tests.Evaluation;

public static class ExpressionFactories
{
    private static Hacker Lorem => new();
    
    private static KeywordValue SystemId => new(Keyword.System);
    private static KeywordValue AggregateId => new(Keyword.AggregateRoot);
    private static KeywordValue EntityId => new(Keyword.Entity);
    private static KeywordValue ValueObjectId => new(Keyword.ValueObject);
    private static KeywordValue EventsId => new(Keyword.Events);

    private static NameValue Name() => new (Lorem.Verb());
    private static CoupletValue SampleEvents() => EventsObject("Test1", "Test2");

    public static readonly TripletValue EmptySystem = Node(SystemId);
    public static readonly TripletValue SingleAggregateSystem = Node(SystemId,  Aggregate());
    public static readonly TripletValue DualAggregateSystem = Node(SystemId,  Aggregate(),  Aggregate());
    public static readonly TripletValue ComplexAggregateSystem = Node(SystemId, 
        Aggregate(SampleEvents(), Entity(SampleEvents()), Entity(), ValueObject()));

    private static TripletValue Node(KeywordValue id, params Expression[] children) => new(id, Name(), children);
    private static TripletValue Aggregate(params Expression[] children) => new (AggregateId, Name(), children);
    private static TripletValue Entity(params Expression[] children) => new (EntityId, Name(), children);
    private static TripletValue ValueObject(params Expression[] children) => new (ValueObjectId, Name(), children);

    private static CoupletValue EventsObject(params string[] children) =>
        new(EventsId, children.Select(x => new NameValue(x)).Cast<Expression>().ToArray());
}

public class ValidExpressionTrees : DataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        foreach (var x in SourceData())
        {
            yield return new[] {x};
        }
    }

    private IEnumerable<object> SourceData()
    {
        yield return ExpressionFactories.EmptySystem;
        yield return ExpressionFactories.SingleAggregateSystem;
        yield return ExpressionFactories.DualAggregateSystem;
        yield return ExpressionFactories.ComplexAggregateSystem;
    }
}