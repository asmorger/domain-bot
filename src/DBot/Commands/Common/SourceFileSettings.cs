using Spectre.Console;
using Spectre.Console.Cli;

namespace DBot.Commands.Common;

public class SourceFileSettings : CommandSettings
{
    [CommandArgument(0, "<file>")] public string FilePath { get; set; } = default!;

    public override ValidationResult Validate() =>
        File.Exists(FilePath)
            ? ValidationResult.Success()
            : ValidationResult.Error("File does not exist");

    public string ReadFile() => File.ReadAllText(FilePath);
}
