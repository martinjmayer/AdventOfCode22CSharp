using BenchmarkDotNet.Attributes;

namespace AdventOfCode22CSharpTests;

public class Day09RopeBridgeBenchmarking
{
    
    private readonly Day09RopeBridgeTests_File _tests;
    private const string _filename = "Day09_Input1.txt";

    public Day09RopeBridgeBenchmarking()
    {
        _tests = new Day09RopeBridgeTests_File();
    }

    [Benchmark]
    public void Original_GetNumberOfTailPositions_1_ReturnsExpectedInt()
        => _tests.GetNumberOfTailPositions_1_ReturnsExpectedInt(_filename);

    [Benchmark]
    public void Original_GetNumberOfTailPositions_10_ReturnsExpectedInt()
        => _tests.GetNumberOfTailPositions_10_ReturnsExpectedInt(_filename);
}