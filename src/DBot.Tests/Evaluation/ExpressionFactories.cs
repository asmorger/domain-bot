using System.Collections.Generic;
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

    private static NameValue Name() => new (Lorem.Verb());

    public static readonly NodeValue EmptySystem = Node(SystemId);
    public static readonly NodeValue SingleAggregateSystem = Node(SystemId,  Aggregate());
    public static readonly NodeValue DualAggregateSystem = Node(SystemId,  Aggregate(),  Aggregate());
    public static readonly NodeValue ComplexAggregateSystem = Node(SystemId, 
        Aggregate(Entity(), Entity(), ValueObject()));

    private static NodeValue Node(KeywordValue id, params Expression[] children) => new(id, Name(), children);
    private static NodeValue Aggregate(params Expression[] children) => new (AggregateId, Name(), children);
    private static NodeValue Entity(params Expression[] children) => new (EntityId, Name(), children);
    private static NodeValue ValueObject(params Expression[] children) => new (ValueObjectId, Name(), children);
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