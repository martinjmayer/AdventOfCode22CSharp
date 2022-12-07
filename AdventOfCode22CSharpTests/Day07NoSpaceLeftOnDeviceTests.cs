using System.Text.RegularExpressions;

namespace AdventOfCode22CSharpTests;

public class Day07NoSpaceLeftOnDeviceTests_File
{
    [TestCase("Day07_Input1.txt", ExpectedResult = 1_315_285)]
    public int DirectorySizer_GetSumOfDirectorySizes_ReturnsExpectedInt(string filename)
    {
        var input = GetAllTextFromFile(filename);
        return DirectorySizer.GetSumOfDirectorySizesBelowOrEqualToCap(DirectorySizer.ParseFileStructureFromTerminalData(input));
    }

    [TestCase("Day07_Input1.txt", ExpectedResult = 9_847_279)]
    public int DirectorySizer_GetSizeOfDirectoryToDelete_ReturnsExpectedInt(string filename)
    {
        var input = GetAllTextFromFile(filename);
        return DirectorySizer.GetSizeOfDirectoryToDelete(DirectorySizer.ParseFileStructureFromTerminalData(input));
    }

    private static string GetAllTextFromFile(string filename)
    {
        return FileEmbedHelper.GetInputFromFile(filename);
    }
}

public class Day07NoSpaceLeftOnDeviceTests
{
    [TestCase("$ cd /\n$ ls\ndir a\n14848514 b.txt\n8504156 c.dat\ndir d\n$ cd a\n$ ls\n" +
              "dir e\n29116 f\n2557 g\n62596 h.lst\n$ cd e\n$ ls\n584 i\n$ cd ..\n$ cd ..\n" +
              "$ cd d\n$ ls\n4060174 j\n8033020 d.log\n5626152 d.ext\n7214296 k",
        ExpectedResult = 95_437)]
    public int DirectorySizer_GetSumOfDirectorySizes_ReturnsExpectedInt(string input)
    {
        return DirectorySizer.GetSumOfDirectorySizesBelowOrEqualToCap(DirectorySizer.ParseFileStructureFromTerminalData(input));
    }
    
    [TestCase("$ cd /\n$ ls\ndir a\n14848514 b.txt\n8504156 c.dat\ndir d\n$ cd a\n$ ls\n" +
              "dir e\n29116 f\n2557 g\n62596 h.lst\n$ cd e\n$ ls\n584 i\n$ cd ..\n$ cd ..\n" +
              "$ cd d\n$ ls\n4060174 j\n8033020 d.log\n5626152 d.ext\n7214296 k",
        ExpectedResult = 24_933_642)]
    public int DirectorySizer_GetSizeOfDirectoryToDelete_ReturnsExpectedInt(string input)
    {
        return DirectorySizer.GetSizeOfDirectoryToDelete(DirectorySizer.ParseFileStructureFromTerminalData(input));
    }
}

public static class DirectorySizer
{
    private const string ChangeDirectoryToRoot = "$ cd /";
    private const string ChangeDirectoryToParent = "$ cd ..";
    private const string ListCommand = "$ ls";
    private const string ChangeDirectoryCommand = "cd";

    public static DeviceFileSystem ParseFileStructureFromTerminalData(string terminalData)
    {
        var fileSystem = new DeviceFileSystem();
        var terminalLines = terminalData.Split('\n');

        if (terminalLines[0] != ChangeDirectoryToRoot)
        {
            throw new ArgumentException($"Expecting line 1 to be '{ChangeDirectoryToRoot}'");
        }
        
        if (terminalLines[1] != ListCommand)
        {
            throw new ArgumentException($"Expecting line 2 to be '{ListCommand}'");
        }
        
        var path = new List<string> { "/" };

        foreach (var line in terminalLines.Skip(2))
        {
            if (line == string.Empty)
            {
                continue;
            }
            
            if (line.StartsWith('$'))
            {
                if (line == ListCommand)
                {
                    continue;
                }

                if (line == ChangeDirectoryToParent)
                {
                    path.RemoveAt(path.Count - 1);
                    continue;
                }

                var tokens = line.Split(' ');

                if (tokens[1] != ChangeDirectoryCommand)
                {
                    throw new Exception($"Expected 2nd token to be {ChangeDirectoryCommand}: '{line}'");
                }

                path.Add(tokens[2]);
            }
            else if (line.StartsWith("dir"))
            {
                continue;
            }
            else
            {
                var tokens = line.Split(' ');

                if (int.TryParse(tokens[0], out var size))
                {
                    for (var i = 0; i < path.Count; i++)
                    {
                        fileSystem.AddFileForRecursiveRegister(new DeviceFile(tokens[1], path.Take(i+1), size));
                    }
                    
                    fileSystem.AddFileForNonRecursiveRegister(new DeviceFile(tokens[1], path, size));
                }
                else
                {
                    throw new Exception($"Expected 1st token to be a numeric value: '{tokens[0]}'");
                }
            }
        }

        return fileSystem;
    }

    private static int GetSumOfDirectorySizes(IEnumerable<KeyValuePair<string,int>>? directorySizes)
    {
        return directorySizes
            .Select(kvp => kvp.Value)
            .Sum();
    }
    
    public static int GetSumOfDirectorySizesBelowOrEqualToCap(DeviceFileSystem fileSystem)
    {
        return GetDirectorySizes(fileSystem, true)
            .Where(directorySize => directorySize.Value <= 100_000)
            .Select(kvp => kvp.Value)
            .Sum();
    }

    private static IEnumerable<KeyValuePair<string, int>> GetDirectorySizes(DeviceFileSystem fileSystem, bool useRecursiveRegister)
    {
        if (useRecursiveRegister)
        {
            return fileSystem.RecursiveFileRegister.GroupBy(file => file.PathKey).Select(group =>
                new KeyValuePair<string, int>(
                    group.Key,
                    group.Select(file => file.Size).Sum()));
        }
        
        return fileSystem.NonRecursiveFileRegister.GroupBy(file => file.PathKey).Select(group =>
            new KeyValuePair<string, int>(
                group.Key,
                group.Select(file => file.Size).Sum()));
    }

    public static int GetSizeOfDirectoryToDelete(DeviceFileSystem fileSystem)
    {
        const int TotalDiskSize = 70_000_000;
        const int UnusedSpaceRequired = 30_000_000;

        var nonRecursiveDirectorySizes = GetDirectorySizes(fileSystem, false);
        var diskSpaceUsed = GetSumOfDirectorySizes(nonRecursiveDirectorySizes);

        var targetToSpaceToFreeUp = UnusedSpaceRequired - (TotalDiskSize - diskSpaceUsed);

        var recursiveDirectorySizes = GetDirectorySizes(fileSystem, true);
        return recursiveDirectorySizes.OrderBy(size => size.Value)
            .FirstOrDefault(size => size.Value > targetToSpaceToFreeUp).Value;
    }
}

public class DeviceFile
{
    public DeviceFile(string name, IEnumerable<string> path, int size)
    {
        Name = name;
        PathKey = string.Join('/', path);
        Size = size;
    }

    private string Name { get; }

    public string PathKey { get; }
    public int Size { get; }

    public override string ToString()
    {
        return $"{PathKey} -- {Name} -- {Size}";
    }
}

public class DeviceFileSystem
{
    private readonly List<DeviceFile> _recursiveFileRegister;
    private readonly List<DeviceFile> _nonRecursiveFileRegister;

    public DeviceFileSystem()
    {
        _recursiveFileRegister = new List<DeviceFile>();
        _nonRecursiveFileRegister = new List<DeviceFile>();
    }

    public IEnumerable<DeviceFile> RecursiveFileRegister => _recursiveFileRegister;
    public IEnumerable<DeviceFile> NonRecursiveFileRegister => _nonRecursiveFileRegister;

    public void AddFileForRecursiveRegister(DeviceFile file)
    {
        _recursiveFileRegister.Add(file);
    }
    
    public void AddFileForNonRecursiveRegister(DeviceFile file)
    {
        _nonRecursiveFileRegister.Add(file);
    }
}