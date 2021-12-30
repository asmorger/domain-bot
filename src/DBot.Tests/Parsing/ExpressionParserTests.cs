using DBot.Dsl.Parsing;
using FluentAssertions;
using Xunit;

namespace DBot.Tests.Parsing;

public class ExpressionParserTests
{
    [Fact]
    public void Basic_system_definition_parses_succesfully()
    {
        var tokens = ExpressionTokenizer.TryTokenize("system GizmoMaker { }");

        tokens.HasValue.Should().BeTrue();

        var result = ExpressionParser.TryParse(tokens.Value, out var expr, out var error, out var errorPosition);

        result.Should().BeTrue();
        expr.Should().NotBeNull();
        error.Should().BeNull();
    }
    
    [Fact]
    public void Basic_system_definition__with_empty_children_parses_succesfully()
    {
        const string dsl = @"system GizmoMaker { 
    aggregate Whatzit { 
        entity Whozit { },
        entity Thingamajig { }
    },
    value Dohickey { }
}";
        var tokens = ExpressionTokenizer.TryTokenize(dsl);

        tokens.HasValue.Should().BeTrue();

        var result = ExpressionParser.TryParse(tokens.Value, out var expr, out var error, out var errorPosition);

        result.Should().BeTrue();
        expr.Should().NotBeNull();
        error.Should().BeNull();
    }
    
    [Fact]
    public void Complex_system_definition_parses_succesfully()
    {
        var tokens = ExpressionTokenizer.TryTokenize("system \"Gizmo Maker\" { }");

        tokens.HasValue.Should().BeTrue();

        var result = ExpressionParser.TryParse(tokens.Value, out var expr, out var error, out var errorPosition);

        result.Should().BeTrue();
        expr.Should().NotBeNull();
        error.Should().BeNull();
    }
}