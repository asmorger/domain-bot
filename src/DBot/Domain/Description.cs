using System.Diagnostics;

namespace DBot.Domain;

[DebuggerDisplay("Description: {Name}")]
public class Description : CodeElement
{
    public Description(string documentation)
    {
        Name = documentation;
    }
    
    public string Name { get; }
}