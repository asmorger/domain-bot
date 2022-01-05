using DBot.Domain;

namespace DBot.Analyzers;

public static class SystemAnalyzer
{
    public static IEnumerable<Entity> GetEntities(CodeElement system)
    {
        if (system is not HierarchicalCodeElement hierarchy)
        {
            yield break;
        }

        foreach (var item in hierarchy)
        {
            if (item is Entity entity)
            {
                yield return entity;
            }

            var childEntities = GetEntities(item);

            foreach (var child in childEntities)
            {
                yield return child;
            }
        }
        
    }
}