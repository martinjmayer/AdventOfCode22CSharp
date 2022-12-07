using BenchmarkDotNet.Attributes;

namespace AdventOfCode22CSharpTests;

public class Day06TuningTroubleBenchmarking
{
    private readonly Day06TuningTroubleTests_File _tests;

    public Day06TuningTroubleBenchmarking()
    {
        _tests = new Day06TuningTroubleTests_File();
    }

    [Benchmark]
    public void Original_DetectPosition_4CharMarker()
        => _tests.StartOfPacketMarkerDetector_DetectPosition_4CharMarker_ReturnsExpectedInt("Day06_Input1.txt");
    
    [Benchmark]
    public void Optimised_DetectPosition_4CharMarker()
        => _tests.StartOfPacketMarkerDetectorOptimised_DetectPosition_4CharMarker_ReturnsExpectedInt("Day06_Input1.txt");
    
    [Benchmark]
    public void SuperOptimised_DetectPosition_4CharMarker()
        => _tests.StartOfPacketMarkerDetectorSuperOptimised_DetectPosition_4CharMarker_ReturnsExpectedInt("Day06_Input1.txt");
    
    [Benchmark]
    public void Original_DetectPosition_14CharMarker()
        => _tests.StartOfPacketMarkerDetector_DetectPosition_14CharMarker_ReturnsExpectedInt("Day06_Input1.txt");
    
    [Benchmark]
    public void Optimised_DetectPosition_14CharMarker()
        => _tests.StartOfPacketMarkerDetectorOptimised_DetectPosition_14CharMarker_ReturnsExpectedInt("Day06_Input1.txt");
    
    [Benchmark]
    public void SuperOptimised_DetectPosition_14CharMarker()
        => _tests.StartOfPacketMarkerDetectorSuperOptimised_DetectPosition_14CharMarker_ReturnsExpectedInt("Day06_Input1.txt");
}