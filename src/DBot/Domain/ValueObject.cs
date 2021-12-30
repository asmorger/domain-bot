namespace DBot.Domain;

public class ValueObject : CodeElement
{
    public string Name { get; }

    public ValueObject(string name)
    {
        Name = name;
    }
}