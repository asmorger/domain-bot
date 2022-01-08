using System.Diagnostics;

namespace DBot.Domain;

[DebuggerDisplay("Service Method: {Name}")]
public class ServiceMethod : CodeElement
{
    public ServiceMethod(string name, string returnType)
    {
        Name = name;
        ReturnType = returnType;
    }

    public string ReturnType { get; }

    public string Name { get; }
}

[DebuggerDisplay("Service: {Name}")]
public class ServiceListing : BaseHierarchicalCodeElement
{
    public ServiceListing(string name) : base(name)
    {
    }
}
