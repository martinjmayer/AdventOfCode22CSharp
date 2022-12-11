using BenchmarkDotNet.Attributes;

namespace AdventOfCode22CSharpTests;

public class Day02RockPaperScissorsBenchmarking
{
    private readonly Day02RockPaperScissorsTests _tests;

    public Day02RockPaperScissorsBenchmarking()
    {
        _tests = new Day02RockPaperScissorsTests();
    }

    [Benchmark]
    public void Original_FirstStrategyGuideParser()
        => _tests.FirstStrategyGuideParser_Parse_InputFile1_ReturnsAStrategyGuide();

    [Benchmark]
    public void Original_SecondStrategyGuideParser()
        => _tests.SecondStrategyGuideParser_Parse_InputFile1_ReturnsAStrategyGuide();
}