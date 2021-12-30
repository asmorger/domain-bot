using System;
using DBot.Domain;
using DBot.Dsl.Evaluation;
using DBot.Dsl.Expressions;
using DBot.Dsl.Parsing;
using FluentAssertions;
using Xunit;

namespace DBot.Tests;

public class ExpressionEvaluatorTests
{
    [Fact]
    public void Software_systems_can_be_evaluated()
    {
        var node = new NodeValue(new IdentifierValue(Identifier.System), new NameValue("Test"), Array.Empty<Expression>());

        var result = ExpressionEvaluator.Evaluate(node);

        result.Should().NotBeNull();
        result.Should().BeOfType<SoftwareSystem>();
    }
}