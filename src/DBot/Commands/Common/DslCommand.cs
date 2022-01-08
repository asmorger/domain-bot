using DBot.Dsl.Evaluation;
using DBot.Dsl.Parsing;
using Superpower.Model;

#pragma warning disable CS8765

namespace DBot.Commands.Common;

public abstract class DslCommand<TSettings> : Command<TSettings>
    where TSettings : SourceFileSettings
{
    protected TSettings Settings { get; private set; } = default!;

    public override int Execute(CommandContext context, TSettings settings)
    {
        try
        {
            Settings = settings;

            var dsl = settings.ReadFile();

            if(string.IsNullOrEmpty(dsl))
            {
                AnsiConsole.MarkupLine("[red]File cannot be empty.[/]");
                return (int)ExitCodes.Error;
            }

            var tokens = ExpressionTokenizer.TryTokenize(dsl);

            if(!tokens.HasValue)
            {
                WriteSyntaxError(dsl, tokens.ToString(), tokens.ErrorPosition);
            }
            else if(!ExpressionParser.TryParse(tokens.Value, out var expr, out var error, out var errorPosition))
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
        catch(Exception e)
        {
            AnsiConsole.WriteException(e);
        }

        return (int)ExitCodes.Error;
    }

    protected abstract void Process(CodeElement system);

    private static void WriteSyntaxError(string dsl, string message, Position errorPosition)
    {
        if(!errorPosition.HasValue)
        {
            return;
        }

        var dslByLine = dsl.Split(Environment.NewLine);
        var zeroBasedErrorLine = errorPosition.Line - 1;

        if(zeroBasedErrorLine == 0)
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
