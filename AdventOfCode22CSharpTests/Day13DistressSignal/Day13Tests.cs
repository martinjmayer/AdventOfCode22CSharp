using Newtonsoft.Json.Linq;

namespace AdventOfCode22CSharpTests.Day13DistressSignal;

public class Day13Tests
{
    [TestCase(
        "[1,1,3,1,1]\n[1,1,5,1,1]\n\n[[1],[2,3,4]]\n[[1],4]\n\n[9]\n[[8,7,6]]\n\n" +
        "[[4,4],4,4]\n[[4,4],4,4,4]\n\n[7,7,7,7]\n[7,7,7]\n\n[]\n[3]\n\n[[[]]]\n[[]]\n\n" +
        "[1,[2,[3,[4,[5,6,7]]]],8,9]\n[1,[2,[3,[4,[5,6,0]]]],8,9]\n",
        ExpectedResult = 13)]
    public int PacketAnalyser_GetIndexSumOfCorrectlyOrderedPackets_ReturnsExpectedInt(string input)
    {
        return PacketAnalyser.GetIndexSumOfCorrectlyOrderedPackets(input);
    }
}

public static class PacketAnalyser
{
    public static int GetIndexSumOfCorrectlyOrderedPackets(string data)
    {
        var input = data.Replace("\n\n", "\n").TrimEnd('\n').Split('\n').ToArray();
        
        var parsedInput = new int[input.Length][];
        for (var i = 0; i < input.Length; i++)
        {
            var list = new List<int>();
            ParseList(list, input[i]);
            parsedInput[i] = list.ToArray();
        }
        
        var rightOrder = new List<int>();
        for (var i = 0; i < parsedInput.Length; i += 2)
        {
            if (IsInRightOrder(
                    parsedInput[i],
                    parsedInput[i + 1],
                    new Tuple<string, string>(input[i], input[i + 1])))
            {
                rightOrder.Add((i / 2) + 1);
            }
        }

        return rightOrder.Sum();
    }
    
    private static bool IsInRightOrder(IReadOnlyList<int> left, IReadOnlyList<int> right, Tuple<string,string> pairStrings)
    {
        var i = 0;
        while (i < left.Count && i < right.Count)
        {
            if (left[i] < right[i])
            {
                return true;
            }
            else if (left[i] > right[i])
            {
                return false;
            }
            else if (left[i] != right[i] && left[i] == 0 && right[i] != 0)
            {
                return false;
            }
            else if (left[i] != right[i] && left[i] != 0 && right[i] == 0)
            {
                return true;
            }

            i++;
        }

        if (ValidatePairStrings(pairStrings) == Result.Pass)
        {
            return true;
        }

        if (ValidatePairStrings(pairStrings) == Result.Failure)
        {
            return false;
        }

        return left.Count <= right.Count;
    }

    private static Result ValidatePairStrings(Tuple<string, string> pairStrings)
    {
        const StringSplitOptions stringSplitOptions = StringSplitOptions.RemoveEmptyEntries|StringSplitOptions.TrimEntries;
        var leftScore1 = pairStrings.Item1.Replace('[', ',').Replace(']', ',').Split(',', stringSplitOptions).Length;
        var rightScore1 = pairStrings.Item2.Replace('[', ',').Replace(']', ',').Split(',', stringSplitOptions).Length;
        
        if (rightScore1 > leftScore1)
        {
            return Result.Pass;
        }
        if (leftScore1 > rightScore1)
        {
            return Result.Failure;
        }
        
        var scoringElements = new[] { '[', ',' };
        var leftScore2 = pairStrings.Item1.Count(c => scoringElements.Contains(c));
        var rightScore2 = pairStrings.Item2.Count(c => scoringElements.Contains(c));
        if (rightScore2 > leftScore2)
        {
            return Result.Pass;
        }
        if (leftScore2 > rightScore2)
        {
            return Result.Failure;
        }
        
        return Result.Inconclusive;
    }

    private static void ParseList(ICollection<int> list, string input)
    {
        if (input == string.Empty || input.Trim() == "[]") return;

        // Remove the surrounding brackets from the input string
        input = input.TrimStart('[').TrimEnd(']');

        // Parse the input string into a list of integers
        var parts = input.Split(',');
        foreach (var part in parts)
        {
            if (int.TryParse(part, out var value))
            {
                // If the part is an integer, add it to the list as a single-element list
                list.Add(value);
            }
            else
            {
                // If the part is another list, recursively parse it
                ParseList(list, part);
            }
        }
    }
}

public enum Result
{
    Pass,
    Failure,
    Inconclusive
}