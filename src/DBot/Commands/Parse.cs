using DBot.Domain;
using Spectre.Console;

namespace DBot.Commands;

public class Parse : DslCommand
{
    protected override void Process(CodeElement system)
    {
        var tree = new Tree(system.Name);

        if (system is HierarchicalCodeElement parent)
        {
            foreach (var element in parent)
            {
                tree.AddNode(BuildNode(element));
            }
        }
        else
        {
            tree.AddNode(BuildNode(system));
        }
        
        AnsiConsole.Write(tree);
    }
    
    private TreeNode BuildNode(CodeElement element)
    {
        var color = GetColor(element);
        var node = new TreeNode(new Markup($"[{color}]{element.Name.EscapeMarkup()}[/]"));

        if (element is HierarchicalCodeElement current)
        {
            foreach (var item in current)
            {
                var node2 = BuildNode(item);
                node.AddNode(node2);
            }
        }

        return node;
    }

    private Color GetColor(CodeElement element) => element switch
    {
        Behavior => Color.Maroon,
        AggregateRoot => Color.Green,
        BehaviorListing => Color.Maroon,
        Description => Color.Grey,
        Entity => Color.Green,
        Event => Color.Aqua,
        EventListing => Color.Aqua,
        Property => Color.Purple,
        PropertyListing => Color.Purple,
        SoftwareSystem => Color.Aqua,
        ValueObject => Color.Yellow,
        ServiceListing => Color.Olive,
        ServiceMethod => Color.Olive,
        _ => Color.White
    };
}