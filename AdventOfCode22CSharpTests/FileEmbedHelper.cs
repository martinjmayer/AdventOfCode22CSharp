using System.Reflection;

namespace AdventOfCode22CSharpTests;

public static class FileEmbedHelper
{
    public static string GetInputFromFile(string filename)
    {
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"AdventOfCode22CSharpTests.{filename}");
        using var streamReader = new StreamReader(stream);
            
        var contents = streamReader.ReadToEnd();

        return contents;
    }
}