namespace AdventOfCode22CSharpTests.Day12HillClimbingAlgorithm;

public class Day12FileTests
{
    [TestCase("Day12_Input1.txt", ExpectedResult = 472)]
    public int GetFewestStepsToBestSignal_ReturnsExpectedInt(string filename)
    {
        var input = FileEmbedHelper.GetInputFromFile(filename, "AdventOfCode22CSharpTests.Day12HillClimbingAlgorithm");
        return SignalPathDetector.GetFewestStepsToBestSignal(input);
    }
    
    [TestCase("Day12_Input1.txt", ExpectedResult = 465)]
    public int GetFewestStepsToBestSignalFromAnyStartPoint_ReturnsExpectedInt(string filename)
    {
        var input = FileEmbedHelper.GetInputFromFile(filename, "AdventOfCode22CSharpTests.Day12HillClimbingAlgorithm");
        return SignalPathDetector.GetFewestStepsToBestSignalFromAnyStartPoint(input);
    }
}