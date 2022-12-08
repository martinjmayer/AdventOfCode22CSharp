using BenchmarkDotNet.Attributes;

namespace AdventOfCode22CSharpTests;

public class Day08TreeTopTreehouseBenchmarking
{
    private readonly Day08TreeTopTreehouseTests_File _tests;
    private const string _filename = "Day07_Input1.txt";

    public Day08TreeTopTreehouseBenchmarking()
    {
        _tests = new Day08TreeTopTreehouseTests_File();
    }

    [Benchmark]
    public void Original_TreeHeightAnalyser_GetNumVisibleTrees()
        => _tests.TreeHeightAnalyser_GetNumVisibleTrees_ReturnsExpectedInt(_filename);

    [Benchmark]
    public void Original_TreeHeightAnalyser_GetHighestScenicScore()
        => _tests.TreeHeightAnalyser_GetHighestScenicScore_ReturnsExpectedInt(_filename);
}