using DBot.Analyzers;
using DBot.Commands.Common;
using DBot.Domain;
using Spectre.Console;
using Spectre.Console.Cli;

namespace DBot.Commands;

public class Complexity : DslCommand<Complexity.ComplexitySettings>
{
    public enum ComplexityType
    {
        Events,
        Behaviors
    }

    protected override void Process(CodeElement system)
    {
        var elements = GetElementsToAnalyze(system)
            .OrderByDescending(x => x.Count);

        var chart = new BarChart();
        chart.Label(GetReportTitle());

        foreach(var e in elements)
        {
            chart.AddItem(e.Name, e.Count);
        }

        AnsiConsole.Write(chart);
    }

    private IEnumerable<(string Name, int Count)> GetElementsToAnalyze(CodeElement system) => Settings.ComplexityType switch
    {
        ComplexityType.Events => SystemAnalyzer.GetEntities(system).Select(x => (x.Name, x.GetChildEvents().Count())),
        ComplexityType.Behaviors => SystemAnalyzer.GetEntities(system).Select(x => (x.Name, x.GetBehaviors().Count())),
        _ => throw new ArgumentOutOfRangeException()
    };

    private string GetReportTitle() => Settings.ComplexityType switch
    {
        ComplexityType.Events => "System complexity by event type",
        ComplexityType.Behaviors => "System complexity by number of behaviors",
        _ => throw new ArgumentOutOfRangeException()
    };

    public class ComplexitySettings : SourceFileSettings
    {
        [CommandOption("-t|--type")] public ComplexityType ComplexityType { get; set; } = ComplexityType.Events;
    }
}
