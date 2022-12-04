namespace AdventOfCode22CSharpTests;

public class Day04CampCleanupTests
{
    // Unit Tests

    [TestCase(2, 4, 6, 8, ExpectedResult = false)]
    [TestCase(2, 3, 4, 5, ExpectedResult = false)]
    [TestCase(5, 7, 7, 9, ExpectedResult = false)]
    [TestCase(2, 8, 3, 7, ExpectedResult = true)]
    [TestCase(6, 6, 4, 6, ExpectedResult = false)]
    [TestCase(2, 6, 4, 8, ExpectedResult = false)]
    public bool IsFullyContained_ReturnsExpectedBool(int start1, int end1, int start2, int end2)
    {
        var range1 = Tuple.Create(start1, end1);
        var range2 = Tuple.Create(start2, end2);
        return IsFullyContained(range1, range2);
    }

    [TestCase("2-4,6-8\n2-3,4-5\n5-7,7-9\n2-8,3-7\n6-6,4-6\n2-6,4-8\n", ExpectedResult = 2)]
    public int TestFullOverlapCount_ReturnsExpectedInt(string data)
    {
        return GetFullOverlapCount(data);
    }
    
    [TestCase(2,8,3,7, ExpectedResult = true)]
    [TestCase(6,6,4,6, ExpectedResult = true)]
    [TestCase(2,3,4,5, ExpectedResult = false)]
    [TestCase(5,7,7,9, ExpectedResult = true)]
    [TestCase(2,6,4,8, ExpectedResult = true)]
    public bool IsPartiallyContained_ReturnsExpectedBool(int start1, int end1, int start2, int end2)
    {
        var range1 = Tuple.Create(start1, end1);
        var range2 = Tuple.Create(start2, end2);
        return IsPartiallyContained(range1, range2);
    }
    
    [TestCase("2-4,6-8\n2-3,4-5\n5-7,7-9\n2-8,3-7\n6-6,4-6\n2-6,4-8\n", ExpectedResult = 4)]
    public int TestPartialOverlapCount_ReturnsExpectedInt(string data)
    {
        return GetPartialOverlapCount(data);
    }

    // File Tests

    [Test]
    public void TestFullOverlapCount_FileTest_ReturnsExpectedOverlapCount()
    {
        var data = File.ReadAllText("Day04_Input1.txt");
        var result = GetFullOverlapCount(data);
        
        Assert.That(result, Is.GreaterThan(0));
    }
    
    [Test]
    public void TestPartialOverlapCount_FileTest_ReturnsExpectedOverlapCount()
    {
        var data = File.ReadAllText("Day04_Input1.txt");
        var result = GetPartialOverlapCount(data);
        
        Assert.That(result, Is.GreaterThan(0));
    }
    
    // Implementation

    private int GetFullOverlapCount(string data)
    {
        var normalisedData = NormaliseData(data);
        var lines = normalisedData.Split('\n');
        var ranges = lines.SelectMany(line =>
        {
            var parts = line.Split(',');
            return parts.Select(part =>
            {
                var values = part.Split('-');
                var start = int.Parse(values[0]);
                var end = int.Parse(values[1]);
                return Tuple.Create(start, end);
            });
        });
        
        return GetFullOverlapCount(ranges);
    }

    private static int GetFullOverlapCount(IEnumerable<Tuple<int, int>> input)
    {
        var ranges = input as List<Tuple<int, int>> ?? input.ToList();

        var overlapCount = 0;
        for (var i = 0; i < ranges.Count; i += 2)
        {
            var range1 = ranges[i];
            var range2 = ranges[i + 1];

            if (IsFullyContained(range1, range2) || IsFullyContained(range2, range1))
            {
                overlapCount++;
            }
        }

        return overlapCount;
    }
    
    private int GetPartialOverlapCount(string data)
    {
        var normalisedData = NormaliseData(data);
        var lines = normalisedData.Split('\n');
        var ranges = lines.SelectMany(line =>
        {
            var parts = line.Split(',');
            return parts.Select(part =>
            {
                var values = part.Split('-');
                var start = int.Parse(values[0]);
                var end = int.Parse(values[1]);
                return Tuple.Create(start, end);
            });
        });
        
        return GetPartialOverlapCount(ranges);
    }
    
    private static int GetPartialOverlapCount(IEnumerable<Tuple<int, int>> input)
    {
        var ranges = input as List<Tuple<int, int>> ?? input.ToList();

        var overlapCount = 0;
        for (var i = 0; i < ranges.Count; i += 2)
        {
            var range1 = ranges[i];
            var range2 = ranges[i + 1];

            //if (IsPartiallyContained(range1, range2) || IsPartiallyContained(range2, range1))
            if (IsPartiallyContained(range1, range2))
            {
                overlapCount++;
            }
        }

        return overlapCount;
    }
    
    private static bool IsFullyContained(Tuple<int, int> range1, Tuple<int, int> range2)
    {
        return range1.Item1 <= range2.Item1 && range1.Item2 >= range2.Item2;
    }
    
    private static bool IsPartiallyContained(Tuple<int, int> range1, Tuple<int, int> range2)
    {
        var start1 = range1.Item1;
        var end1 = range1.Item2;
        var start2 = range2.Item1;
        var end2 = range2.Item2;
        return start1 <= end2 && end1 >= start2;
    }
    
    // Utility
    
    private static string NormaliseData(string data)
    {
        var dataWithoutCarriageReturns = data.Replace("\r", string.Empty);
        var trimmedData = dataWithoutCarriageReturns.Trim('\n');

        var processingData = trimmedData;
        const string doubleNewLine = "\n\n";
        while (processingData.Contains(doubleNewLine))
        {
            processingData = processingData.Replace(doubleNewLine, "\n");
        }
        
        return processingData;
    }
}