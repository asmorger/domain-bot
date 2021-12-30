namespace DBot.Domain;

public class Entity : CodeElement
{
    public string Name { get; }

    public Entity(string name)
    {
        Name = name;
    }
}