﻿using DBot.Domain;
using DBot.Dsl.Evaluation;
using DBot.Dsl.Parsing;
using Spectre.Console;
using Spectre.Console.Cli;
using Superpower.Model;

#pragma warning disable CS8765

namespace DBot.Commands;

public abstract class DslCommand : Command<SourceFileSettings>
{
    public override int Execute(CommandContext context, SourceFileSettings settings)
    {
        try
        {
            var dsl = settings.ReadFile();

            if (string.IsNullOrEmpty(dsl))
            {
                AnsiConsole.MarkupLine("[red]File cannot be empty.[/]");
                return (int)ExitCodes.Error;
            }

            var tokens = ExpressionTokenizer.TryTokenize(dsl);

            if (!tokens.HasValue)
            {
                WriteSyntaxError(dsl, tokens.ToString(), tokens.ErrorPosition);
            }
            else if (!ExpressionParser.TryParse(tokens.Value, out var expr, out var error, out var errorPosition))
            {
                WriteSyntaxError(dsl, error, errorPosition);
            }
            else
            {
                var result = ExpressionEvaluator.Evaluate(expr);
                Process(result);
                return (int)ExitCodes.Success;
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
        
        return (int)ExitCodes.Error;
    }

    protected abstract void Process(CodeElement system);

    static void WriteSyntaxError(string dsl, string message, Position errorPosition)
    {
        if (!errorPosition.HasValue)
        {
            return;
        }

        var dslByLine = dsl.Split(Environment.NewLine);
        var zeroBasedErrorLine = errorPosition.Line - 1;

        if (zeroBasedErrorLine == 0)
        {
            AnsiConsole.WriteLine(dslByLine[0]);
        }
        else
        {
            // give a little more context to the error message
            AnsiConsole.WriteLine(dslByLine[zeroBasedErrorLine - 1]);
            AnsiConsole.WriteLine(dslByLine[zeroBasedErrorLine]);
        }
        
        AnsiConsole.WriteLine(new string(' ', errorPosition.Column - 1) + '^');
        AnsiConsole.MarkupLine($"[yellow]{message}[/]");
    }
}