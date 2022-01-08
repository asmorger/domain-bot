using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit.Sdk;

namespace DBot.Tests.SampleData;

public class ValidDsls : DataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        foreach(var file in GetFileContent())
        {
            yield return new object[] {file};
        }
    }

    private IEnumerable<string> GetFileContent()
    {
        yield return ReadEmbeddedFile("EmptySystem.txt");
        yield return ReadEmbeddedFile("SimpleSystem.txt");
        yield return ReadEmbeddedFile("ComplexSystem.txt");
    }

    private string ReadEmbeddedFile(string fileName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var targetItem = assembly.GetManifestResourceNames().First(x => x.EndsWith(fileName));

        var stream = assembly.GetManifestResourceStream(targetItem)!;
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}
