using DBot.Dsl.Evaluation;
using DBot.Dsl.Expressions;
using FluentAssertions;
using Xunit;

namespace DBot.Tests.Evaluation;

public class ExpressionEvaluatorTests
{
    [Theory]
    [ValidExpressionTrees]
    public void Software_systems_can_be_evaluated(TripletValue system)
    {
        var result = ExpressionEvaluator.Evaluate(system);
        result.Should().NotBeNull();
    }
}
