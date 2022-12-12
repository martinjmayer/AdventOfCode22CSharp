using System.Reflection;

namespace AdventOfCode22CSharpTests;

public static class FileEmbedHelper
{
    public static string GetInputFromFile(string filename, string resourceNamespace = "")
    {
        var resNamespace = resourceNamespace == string.Empty
            ? "AdventOfCode22CSharpTests."
            : resourceNamespace.Trim('.') + ".";
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{resNamespace}{filename}");
        using var streamReader = new StreamReader(stream);

        var contents = streamReader.ReadToEnd();

        return contents;
    }
}