using BenchmarkDotNet.Attributes;

namespace AdventOfCode22CSharpTests;

public class Day10CathodeRayTubeBenchmarking
{
    
    private readonly Day10CathodeRayTubeTests_File _tests;
    private const string _filename = "Day10_Input1.txt";

    public Day10CathodeRayTubeBenchmarking()
    {
        _tests = new Day10CathodeRayTubeTests_File();
    }

    [Benchmark]
    public void Original_GetTotalSignalStrength()
        => _tests.SignalStrengthCalculator_GetTotalSignalStrength(_filename);

    [Benchmark]
    public void Original_GetCathodeRayImage()
        => _tests.SignalStrengthCalculator_GetCathodeRayImage(_filename);
}