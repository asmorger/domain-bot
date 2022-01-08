using Dbot.Domain;

namespace DBot.Commands.Diagrams.Generators;

public interface DiagramGenerator
{
    string Generate(CodeElement system);
}
