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
        var node = new TreeNode(new Markup(element.Name));

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
}