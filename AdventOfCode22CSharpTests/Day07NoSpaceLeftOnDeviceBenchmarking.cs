using BenchmarkDotNet.Attributes;

namespace AdventOfCode22CSharpTests;

public class Day07NoSpaceLeftOnDeviceBenchmarking
{
    private readonly Day07NoSpaceLeftOnDeviceTests_File _tests;
    private const string _filename = "Day07_Input1.txt";

    public Day07NoSpaceLeftOnDeviceBenchmarking()
    {
        _tests = new Day07NoSpaceLeftOnDeviceTests_File();
    }

    [Benchmark]
    public void Original_DetectPosition_SumOfDirectorySizes()
        => _tests.DirectorySizer_GetSumOfDirectorySizes_ReturnsExpectedInt(_filename);

    [Benchmark]
    public void Original_DetectPosition_GetSizeOfDirectoryToDelete()
        => _tests.DirectorySizer_GetSizeOfDirectoryToDelete_ReturnsExpectedInt(_filename);
}