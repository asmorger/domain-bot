using DBot.Dsl.Parsing;
using DBot.Tests.SampleData;
using FluentAssertions;
using Xunit;

namespace DBot.Tests.Parsing;

public class ExpressionParserTests
{
    [Theory]
    [ValidDsls]
    public void Tokenizer_understands_known_dsls(string dsl)
    {
        var tokens = ExpressionTokenizer.TryTokenize(dsl);

        tokens.HasValue.Should().BeTrue();

        var result = ExpressionParser.TryParse(tokens.Value, out var expr, out var error, out var errorPosition);

        error.Should().BeNull();
        result.Should().BeTrue();
        expr.Should().NotBeNull();
        
    }
}