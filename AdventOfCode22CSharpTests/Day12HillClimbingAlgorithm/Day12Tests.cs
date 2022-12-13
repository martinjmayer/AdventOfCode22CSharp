using System.Diagnostics;

namespace AdventOfCode22CSharpTests.Day12HillClimbingAlgorithm;

public class Day12Tests
{
    [TestCase("Sabqponm\nabcryxxl\naccszExk\nacctuvwj\nabdefghi\n", ExpectedResult = 31)]
    public int GetFewestStepsToBestSignal_ReturnsExpectedInt(string input)
    {
        return SignalPathDetector.GetFewestStepsToBestSignal(input);
    }
    
    [TestCase("Sabqponm\nabcryxxl\naccszExk\nacctuvwj\nabdefghi\n", ExpectedResult = 29)]
    public int GetFewestStepsToBestSignalFromAnyStartPoint_ReturnsExpectedInt(string input)
    {
        return SignalPathDetector.GetFewestStepsToBestSignalFromAnyStartPoint(input);
    }
}