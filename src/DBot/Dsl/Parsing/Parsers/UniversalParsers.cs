﻿using DBot.Dsl.Expressions;
using Superpower;
using Superpower.Parsers;

namespace DBot.Dsl.Parsing.Parsers;
using ExpressionTokenParser = TokenListParser<ExpressionToken, Expression>;

public static class UniversalParsers
{
    public static ExpressionTokenParser Keyword { get; } =
        Token.EqualTo(ExpressionToken.System)
            .Or(Token.EqualTo(ExpressionToken.Aggregate))
            .Or(Token.EqualTo(ExpressionToken.Behavior))
            .Or(Token.EqualTo(ExpressionToken.Description))
            .Or(Token.EqualTo(ExpressionToken.Entity))
            .Or(Token.EqualTo(ExpressionToken.Events))
            .Or(Token.EqualTo(ExpressionToken.Properties))
            .Or(Token.EqualTo(ExpressionToken.Raises))
            .Or(Token.EqualTo(ExpressionToken.ValueObject))
            .Apply(ExpressionTextParsers.Keyword)
            .Select(id => (Expression) new KeywordValue(id));

    public static ExpressionTokenParser String { get; } =
        Token.EqualTo(ExpressionToken.String)
            .Select(x => (Expression) new NameValue(x.ToStringValue().Trim('"')));
}