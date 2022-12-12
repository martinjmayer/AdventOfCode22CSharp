using BenchmarkDotNet.Attributes;

namespace AdventOfCode22CSharpTests.Day11MonkeyInTheMiddle;

public class Day11Benchmarking
{
    private readonly Day11FileTests _fileTests;
    private const string _filename = "Day11_Input1.txt";

    public Day11Benchmarking()
    {
        _fileTests = new Day11FileTests();
    }

    // Consider rewriting using Roslyn to assemble classes based on input data so that Test Divisor and
    // Worry Operations are hardcoded as C# rather than running through conditionals every round.
    
    [Benchmark]
    public void Original_GetLevelOfMonkeyBusiness________20_Rounds() =>
        _fileTests.MonkeyBusinessAnalyser_GetLevelOfMonkeyBusiness________20_Rounds_ReturnsExpectedInt(_filename);
    
    [Benchmark]
    public void Original_GetLevelOfMonkeyBusiness____10_000_Rounds() =>
        _fileTests.MonkeyBusinessAnalyser_GetLevelOfMonkeyBusiness____10_000_Rounds_MoreWorry_ReturnsExpectedInt(_filename);
    
    [Benchmark]
    public void Original_GetLevelOfMonkeyBusiness_2_000_000_Rounds() =>
        _fileTests.MonkeyBusinessAnalyser_GetLevelOfMonkeyBusiness_2_000_000_Rounds_MoreWorry_ReturnsExpectedInt(_filename);
}