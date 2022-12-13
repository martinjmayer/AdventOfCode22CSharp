using BenchmarkDotNet.Attributes;

namespace AdventOfCode22CSharpTests.Day12HillClimbingAlgorithm;

public class Day12Benchmarking
{
    private readonly Day12FileTests _fileTests;
    private const string _filename = "Day12_Input1.txt";

    public Day12Benchmarking()
    {
        _fileTests = new Day12FileTests();
    }
    
    [Benchmark]
    public void Original_GetFewestStepsToBestSignal() =>
        _fileTests.GetFewestStepsToBestSignal_ReturnsExpectedInt(_filename);
    
    [Benchmark]
    public void Original_GetFewestStepsToBestSignalFromAnyStartPoint() =>
        _fileTests.GetFewestStepsToBestSignalFromAnyStartPoint_ReturnsExpectedInt(_filename);
}