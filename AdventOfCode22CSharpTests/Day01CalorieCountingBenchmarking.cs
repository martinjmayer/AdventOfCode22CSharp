using BenchmarkDotNet.Attributes;

namespace AdventOfCode22CSharpTests;

public class Day01CalorieCountingBenchmarking
{
    private readonly Day01CalorieCountingTests _tests;

    public Day01CalorieCountingBenchmarking()
    {
        _tests = new Day01CalorieCountingTests();
    }

    [Benchmark]
    public void Original_GetElfWithMostCalories()
        => _tests.ElfCalorieRanker_GetElfWithMostCalories_Input1File_ReturnsAResult();

    [Benchmark]
    public void Original_GetTotalCaloriesOfTopElvesByMostCalories()
        => _tests.ElfCalorieRanker_GetTotalCaloriesOfTopElvesByMostCalories_Input1FileWith3Elves_ReturnsAResult();
}