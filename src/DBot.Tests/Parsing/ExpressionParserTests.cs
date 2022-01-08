using DBot.Dsl.Parsing;
using DBot.Tests.SampleData;
using FluentAssertions;
using Xunit;

namespace DBot.Tests.Parsing;

public class ExpressionParserTests
{
    [Theory]
    [ValidDsls]
    public void Tokenizer_understands_known_dsls(string fileName)
    {
        var dsl = ValidDsls.ReadEmbeddedFile(fileName);
        var tokens = ExpressionTokenizer.TryTokenize(dsl);

        tokens.HasValue.Should().BeTrue();

        var result = ExpressionParser.TryParse(tokens.Value, out var expr, out var error, out var _);

        error.Should().BeNull();
        result.Should().BeTrue();
        expr.Should().NotBeNull();
    }
}
