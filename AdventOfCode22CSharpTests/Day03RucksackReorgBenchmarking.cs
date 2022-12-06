using BenchmarkDotNet.Attributes;

namespace AdventOfCode22CSharpTests;

public class Day03RucksackReorgBenchmarking
{
    private readonly Day03RucksackReorgTests _tests;

    public Day03RucksackReorgBenchmarking()
    {
        _tests = new Day03RucksackReorgTests();
    }

    [Benchmark]
    public void InitialSolution_GetTotalPriority_FileTest_ReturnsExpectedTotal()
        => _tests.GetTotalPriority_FileTest_ReturnsExpectedTotal("Day03_Input1.txt");
    
    [Benchmark]
    public void InitialSolution_GetTotalPriorityOfGroupBadges_FileTest_ReturnsExpectedTotal()
        => _tests.GetTotalPriorityOfGroupBadges_FileTest_ReturnsExpectedTotal("Day03_Input1.txt");
}