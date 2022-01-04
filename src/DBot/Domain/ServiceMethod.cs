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
    
    public string Name { get; }
    public string ReturnType { get; }
}

[DebuggerDisplay("Service: {Name}")]
public class ServiceListing  : BaseHierarchicalCodeElement
{
    public ServiceListing(string name) : base(name)
    {
    }
}