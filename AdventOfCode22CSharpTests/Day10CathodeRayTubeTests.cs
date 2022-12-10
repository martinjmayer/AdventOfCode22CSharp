using System.Diagnostics;

namespace AdventOfCode22CSharpTests;

public class Day10CathodeRayTubeTests_File
{
    private readonly Stopwatch _stopwatch;

    public Day10CathodeRayTubeTests_File()
    {
        _stopwatch = new Stopwatch();
    }

    [TestCase("Day10_Input1.txt",
        ExpectedResult = "####..##....##.###...##...##..####.#..#.\n" +
                         "#....#..#....#.#..#.#..#.#..#.#....#.#..\n" +
                         "###..#.......#.###..#....#....###..##...\n" +
                         "#....#.##....#.#..#.#.##.#....#....#.#..\n" +
                         "#....#..#.#..#.#..#.#..#.#..#.#....#.#..\n" +
                         "####..###..##..###...###..##..#....#..#.\n")]
    public string SignalStrengthCalculator_GetCathodeRayImage(string filename)
    {
        var input = FileEmbedHelper.GetInputFromFile(filename);
        
        var signalStrengthCalculator = new SignalStrengthCalculator(_stopwatch);
        return signalStrengthCalculator.GetCathodeRayImage(input, 1);
    }

    [TestCase("Day10_Input1.txt", ExpectedResult = 13480)]
    public int SignalStrengthCalculator_GetTotalSignalStrength(string filename)
    {
        var input = FileEmbedHelper.GetInputFromFile(filename);
        
        var signalStrengthCalculator = new SignalStrengthCalculator(_stopwatch);
        return signalStrengthCalculator.GetTotalSignalStrength(input, 1);
    }
}

public class Day10CathodeRayTubeTests
{
    private readonly Stopwatch _stopwatch;

    public Day10CathodeRayTubeTests()
    {
        _stopwatch = new Stopwatch();
    }
    
    [TestCase("addx 15\naddx -11\naddx 6\naddx -3\naddx 5\naddx -1\naddx -8\naddx 13\n" +
              "addx 4\nnoop\naddx -1\naddx 5\naddx -1\naddx 5\naddx -1\naddx 5\naddx -1\n" +
              "addx 5\naddx -1\naddx -35\naddx 1\naddx 24\naddx -19\naddx 1\naddx 16\n" +
              "addx -11\nnoop\nnoop\naddx 21\naddx -15\nnoop\nnoop\naddx -3\naddx 9\n" +
              "addx 1\naddx -3\naddx 8\naddx 1\naddx 5\nnoop\nnoop\nnoop\nnoop\nnoop\n" +
              "addx -36\nnoop\naddx 1\naddx 7\nnoop\nnoop\nnoop\naddx 2\naddx 6\nnoop\n" +
              "noop\nnoop\nnoop\nnoop\naddx 1\nnoop\nnoop\naddx 7\naddx 1\nnoop\naddx -13\n" +
              "addx 13\naddx 7\nnoop\naddx 1\naddx -33\nnoop\nnoop\nnoop\naddx 2\nnoop\nnoop\n" +
              "noop\naddx 8\nnoop\naddx -1\naddx 2\naddx 1\nnoop\naddx 17\naddx -9\naddx 1\n" +
              "addx 1\naddx -3\naddx 11\nnoop\nnoop\naddx 1\nnoop\naddx 1\nnoop\nnoop\n" +
              "addx -13\naddx -19\naddx 1\naddx 3\naddx 26\naddx -30\naddx 12\naddx -1\n" +
              "addx 3\naddx 1\nnoop\nnoop\nnoop\naddx -9\naddx 18\naddx 1\naddx 2\nnoop\n" +
              "noop\naddx 9\nnoop\nnoop\nnoop\naddx -1\naddx 2\naddx -37\naddx 1\naddx 3\n" +
              "noop\naddx 15\naddx -21\naddx 22\naddx -6\naddx 1\nnoop\naddx 2\naddx 1\n" +
              "noop\naddx -10\nnoop\nnoop\naddx 20\naddx 1\naddx 2\naddx 2\naddx -6\n" +
              "addx -11\nnoop\nnoop\nnoop\n",
        ExpectedResult = "##..##..##..##..##..##..##..##..##..##..\n" +
                         "###...###...###...###...###...###...###.\n" + 
                         "####....####....####....####....####....\n" +
                         "#####.....#####.....#####.....#####.....\n" +
                         "######......######......######......####\n" +
                         "#######.......#######.......#######.....\n")]
    public string SignalStrengthCalculator_GetCathodeRayImage(string input)
    {
        _stopwatch.Reset();
        var signalStrengthCalculator = new SignalStrengthCalculator(_stopwatch);
        return signalStrengthCalculator.GetCathodeRayImage(input, 1);
    }

    [TestCase("addx 15\naddx -11\naddx 6\naddx -3\naddx 5\naddx -1\naddx -8\naddx 13\n" +
              "addx 4\nnoop\naddx -1\naddx 5\naddx -1\naddx 5\naddx -1\naddx 5\naddx -1\n" +
              "addx 5\naddx -1\naddx -35\naddx 1\naddx 24\naddx -19\naddx 1\naddx 16\n" +
              "addx -11\nnoop\nnoop\naddx 21\naddx -15\nnoop\nnoop\naddx -3\naddx 9\n" +
              "addx 1\naddx -3\naddx 8\naddx 1\naddx 5\nnoop\nnoop\nnoop\nnoop\nnoop\n" +
              "addx -36\nnoop\naddx 1\naddx 7\nnoop\nnoop\nnoop\naddx 2\naddx 6\nnoop\n" +
              "noop\nnoop\nnoop\nnoop\naddx 1\nnoop\nnoop\naddx 7\naddx 1\nnoop\naddx -13\n" +
              "addx 13\naddx 7\nnoop\naddx 1\naddx -33\nnoop\nnoop\nnoop\naddx 2\nnoop\nnoop\n" +
              "noop\naddx 8\nnoop\naddx -1\naddx 2\naddx 1\nnoop\naddx 17\naddx -9\naddx 1\n" +
              "addx 1\naddx -3\naddx 11\nnoop\nnoop\naddx 1\nnoop\naddx 1\nnoop\nnoop\n" +
              "addx -13\naddx -19\naddx 1\naddx 3\naddx 26\naddx -30\naddx 12\naddx -1\n" +
              "addx 3\naddx 1\nnoop\nnoop\nnoop\naddx -9\naddx 18\naddx 1\naddx 2\nnoop\n" +
              "noop\naddx 9\nnoop\nnoop\nnoop\naddx -1\naddx 2\naddx -37\naddx 1\naddx 3\n" +
              "noop\naddx 15\naddx -21\naddx 22\naddx -6\naddx 1\nnoop\naddx 2\naddx 1\n" +
              "noop\naddx -10\nnoop\nnoop\naddx 20\naddx 1\naddx 2\naddx 2\naddx -6\n" +
              "addx -11\nnoop\nnoop\nnoop\n", ExpectedResult = 13140)]
    public int SignalStrengthCalculator_GetTotalSignalStrength(string input)
    {
        _stopwatch.Reset();
        var signalStrengthCalculator = new SignalStrengthCalculator(_stopwatch);
        return signalStrengthCalculator.GetTotalSignalStrength(input, 1);
    }

    [TestCase("noop\naddx 3\naddx -5", ExpectedResult = -1)]
    public int SignalStrengthCalculator_GetValueOfX(string input)
    {
        _stopwatch.Reset();
        var signalStrengthCalculator = new SignalStrengthCalculator(_stopwatch);
        return signalStrengthCalculator.GetValueOfX(input, 1);
    }
}

public class SignalStrengthCalculator
{
    public SignalStrengthCalculator(Stopwatch stopwatch)
    {
        Stopwatch = stopwatch;
    }

    public Stopwatch Stopwatch { get; }

    public string GetCathodeRayImage(string data, int startValueOfX)
    {
        const int screenWidth = 40;
        const int screenHeight = 6;
        
        var cycleAdjustments = GetCycleAdjustments(data).ToArray().AsSpan();

        var builder = new StringBuilder();

        var valueOfX = 1;
        for (var vertical = 0; vertical < screenHeight; vertical++)
        {
            for (var horizontal = 0; horizontal < screenWidth; horizontal++)
            {
                builder.Append(horizontal >= valueOfX - 1 && horizontal <= valueOfX + 1 ? '#' : '.');
                valueOfX+= cycleAdjustments[vertical * screenWidth + horizontal];
            }

            builder.Append('\n');
        }

        return builder.ToString();
    }
    
    public int GetTotalSignalStrength(string data, int startValueOfX)
    {
        var cycleAdjustments = GetCycleAdjustments(data).ToArray().AsSpan();
        var measurementPoints = new [] { 0, 20, 60, 100, 140, 180, 220 };

        var valueOfX = startValueOfX;
        var strengths = new List<int>();
        for (var i = 1; i< measurementPoints.Length; i++)
        {
            var endPoint = measurementPoints[i];
            var startPoint = measurementPoints[i - 1];
            var newValueOfX = GetValueOfXAfterSpecificNumberOfCycles(cycleAdjustments, valueOfX, startPoint+1, endPoint - startPoint);
            strengths.Add(newValueOfX * endPoint);
            valueOfX = newValueOfX;
        }

        return strengths.Sum();
    }
    
    public int GetValueOfX(string data, int startValueOfX)
    {
        var cycleAdjustments = GetCycleAdjustments(data).ToArray().AsSpan();
        return GetValueOfXAfterSpecificNumberOfCycles(cycleAdjustments, startValueOfX, 1, cycleAdjustments.Length);
    }

    private int GetValueOfXAfterSpecificNumberOfCycles(Span<int> cycleAdjustments, int startValueOfX, int startCycle, int length)
    {
        var startIndex = startCycle - 2;
        if (startIndex < 0)
        {
            startIndex = 0;
        }

        var section = cycleAdjustments.Slice(startIndex, length).ToArray();
        return section.Sum() + startValueOfX;
    }

    private IEnumerable<int> GetCycleAdjustments(string data)
    {
        var cycles = data.Split('\n').SelectMany(GetCommandCycleAdjustments);
        return cycles;
    }

    private IEnumerable<int> GetCommandCycleAdjustments(string command)
    {
        if (command == string.Empty)
        {
            return Enumerable.Empty<int>();
        }
        var split = command.Split(' ');
        return split[0] switch
        {
            "noop" => new[] { 0 },
            "addx" => new[] { 0, int.Parse(split[1]) },
            _ => throw new Exception("Unknown command")
        };
    }
}

public class Cycle
{
    public int CurrentValueOfXIncludingAdjustment { get; }
    public int Adjustment { get; }
}