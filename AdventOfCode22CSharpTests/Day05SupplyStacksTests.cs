using System.Collections.Concurrent;

namespace AdventOfCode22CSharpTests;

public class Day05SupplyStacksTests
{
    // Unit Tests
    [TestCase("    [D]    \n[N] [C]    \n[Z] [M] [P]\n 1   2   3 \n\n" +
              "move 1 from 2 to 1\nmove 3 from 1 to 3\nmove 2 from 2 to 1\nmove 1 from 1 to 2\n",
        ExpectedResult = "CMZ")]
    public string GetTopCrates_SingleLift_ReturnsExpectedString(string data)
    {
        return GetTopCrates(data, false);
    }
    
    [TestCase("    [D]    \n[N] [C]    \n[Z] [M] [P]\n 1   2   3 \n\n" +
               "move 1 from 2 to 1\nmove 3 from 1 to 3\nmove 2 from 2 to 1\nmove 1 from 1 to 2\n",
        ExpectedResult = "MCD")]
    public string GetTopCrates_MultiLift_ReturnsExpectedString(string data)
    {
        return GetTopCrates(data, true);
    }

    // File Tests
    [TestCase("Day05_Input1.txt", ExpectedResult = "BSDMQFLSP")]
    public string GetTopCrates_SingleLift_FileTest_ReturnsExpectedString(string filename)
    {
        var data = File.ReadAllText(filename);
        return GetTopCrates(data, false);
    }
    
    [TestCase("Day05_Input1.txt", ExpectedResult = "PGSQBFLDP")]
    public string GetTopCrates_MultiLift_FileTest_ReturnsExpectedString(string filename)
    {
        var data = File.ReadAllText(filename);
        return GetTopCrates(data, true);
    }

    // Implementation
    private static string GetTopCrates(string data, bool isMultiLift)
    {
        // Parse the input to extract the stacks and the procedure
        var sections = data.Split("\n\n");
        var stackLines = sections[0].Split('\n').SkipLast(1).ToArray();
        var procedure = sections[1].Split('\n');

        const int chunkSize = 4;
        var rowsReversed = stackLines.Select(line => Enumerable.Range(0, (line.Length + 1) / chunkSize)
            .Select(i => (line + " ").ElementAt((i * chunkSize) + 1)).ToArray()).Reverse();

        var numberOfStacks = rowsReversed.ToArray()[0].Count();
        var stacks = Enumerable.Range(0,numberOfStacks).Select(_ => new CrateStack()).ToArray();
        foreach (var row in rowsReversed)
        {
            for (var stackIndex = 0; stackIndex < row.Length; stackIndex++)
            {
                if (row[stackIndex] != ' ')
                {
                    stacks[stackIndex].StackCrate(row[stackIndex]);
                }
            }
        }

        return GetTopCrates(stacks, procedure, isMultiLift);
    }

    private static string GetTopCrates(IEnumerable<CrateStack> stacks, IEnumerable<string> procedure, bool isMultiLift)
    {
        var allStacks = stacks.ToList();

        foreach (var step in procedure)
        {
            if (step == string.Empty)
            {
                continue;
            }
            var tokens = step.Split(' ');
            var numberOfCratesToMoveDuringStep = int.Parse(tokens[1]);
            var sourceStack = int.Parse(tokens[3]);
            var destinationStack = int.Parse(tokens[5]);

            if (isMultiLift)
            {
                var crates = allStacks[sourceStack - 1].LiftCrates(numberOfCratesToMoveDuringStep);
                allStacks[destinationStack - 1].StackMultipleCrates(crates);
            }
            else
            {
                for (var i = 0; i < numberOfCratesToMoveDuringStep; i++)
                {
                    var crate = allStacks[sourceStack - 1].LiftCrate();
                    allStacks[destinationStack - 1].StackCrate(crate);
                }
            }

        }

        var topCrates = new StringBuilder();
        foreach (var stack in allStacks)
        {
            topCrates.Append(stack.LiftCrate());
        }
        return topCrates.ToString();
    }
}

public class CrateStack
{
    private readonly Stack<char> _contents;
    
    public CrateStack()
    {
        _contents = new Stack<char>();
    }
    
    public void StackCrate(char crate)
    {
        _contents.Push(crate);
    }

    public char LiftCrate()
    {
        return _contents.Pop();
    }

    public IEnumerable<char> LiftCrates(int numberToLift)
    {
        return _contents.PopRange(numberToLift);
    }

    public void StackMultipleCrates(IEnumerable<char> crates)
    {
        foreach (var crate in crates)
        {
            _contents.Push(crate);
        }
    }
}

public static class Extensions
{
    public static IEnumerable<T> PopRange<T>(this Stack<T> stack, int amount)
    {
        var result = new Stack<T>(amount);
        while (amount-- > 0 && stack.Count > 0)
        {
            result.Push(stack.Pop());
        }
        return result;
    }
}