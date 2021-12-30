using DBot.Dsl.Parsing;
using FluentAssertions;
using Xunit;

namespace DBot.Tests.Parsing;

public class ExpressionTokenizerTests
{
    [Fact]
    public void Basic_system_definition_parses_succesfully()
    {
        var result = ExpressionTokenizer.TryTokenize("system GizmoMaker { }");
        result.HasValue.Should().BeTrue();
        result.Value.Should().HaveCount(4);
    }
    
    [Fact]
    public void Complex_system_definition_parses_succesfully()
    {
        var result = ExpressionTokenizer.TryTokenize("system \"Gizmo Maker\" { }");
        result.HasValue.Should().BeTrue();
        result.Value.Should().HaveCount(4);
    }
}