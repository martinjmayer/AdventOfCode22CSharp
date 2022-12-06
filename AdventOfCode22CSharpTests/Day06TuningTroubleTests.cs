namespace AdventOfCode22CSharpTests;

public class Day06TuningTroubleTests_File
{
    [TestCase("Day06_Input1.txt", ExpectedResult = 1544)]
    public int StartOfPacketMarkerDetector_DetectPosition_4CharMarker_ReturnsExpectedInt(string filename)
    {
        var input = File.ReadAllText(filename);
        return StartOfPacketMarkerDetector.DetectPosition(input, 4);
    }
    [TestCase("Day06_Input1.txt", ExpectedResult = 1544)]
    public int StartOfPacketMarkerDetectorOptimised_DetectPosition_4CharMarker_ReturnsExpectedInt(string filename)
    {
        var input = File.ReadAllText(filename);
        return StartOfPacketMarkerDetectorOptimised.DetectPosition(input, 4);
    }
    
    [TestCase("Day06_Input1.txt", ExpectedResult = 2145)]
    public int StartOfPacketMarkerDetector_DetectPosition_14CharMarker_ReturnsExpectedInt(string filename)
    {
        var input = File.ReadAllText(filename);
        return StartOfPacketMarkerDetector.DetectPosition(input, 14);
    }
    
    [TestCase("Day06_Input1.txt", ExpectedResult = 2145)]
    public int StartOfPacketMarkerDetectorOptimised_DetectPosition_14CharMarker_ReturnsExpectedInt(string filename)
    {
        var input = File.ReadAllText(filename);
        return StartOfPacketMarkerDetectorOptimised.DetectPosition(input, 14);
    }
}

public class Day06TuningTroubleTests
{
    [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", ExpectedResult = 7)]
    [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", ExpectedResult = 5)]
    [TestCase("nppdvjthqldpwncqszvftbrmjlhg", ExpectedResult = 6)]
    [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", ExpectedResult = 10)]
    [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", ExpectedResult = 11)]
    public int StartOfPacketMarkerDetector_DetectPosition_4CharMarker_ReturnsExpectedInt(string input)
    {
        return StartOfPacketMarkerDetector.DetectPosition(input, 4);
    }
    
    [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", ExpectedResult = 19)]
    [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", ExpectedResult = 23)]
    [TestCase("nppdvjthqldpwncqszvftbrmjlhg", ExpectedResult = 23)]
    [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", ExpectedResult = 29)]
    [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", ExpectedResult = 26)]
    public int StartOfPacketMarkerDetector_DetectPosition_14CharMarker_ReturnsExpectedInt(string input)
    {
        return StartOfPacketMarkerDetector.DetectPosition(input, 14);
    }
}

public static class StartOfPacketMarkerDetector
{
    public static int DetectPosition(string streamData, int markerLength)
    {
        for (var streamIndex = 0; streamIndex < streamData.Length - markerLength; streamIndex++)
        {
            var substringValue = streamData.Substring(streamIndex, markerLength);
            if (substringValue.Distinct().Count() == markerLength)
            {
                return streamIndex + markerLength;
            }
        }

        throw new ArgumentException($"The stream data did not contain a {markerLength}-character Start of Packet Marker. '{streamData}'");
    }
}


public static class StartOfPacketMarkerDetectorOptimised
{
    public static int DetectPosition(string streamData, int markerLength)
    {
        var streamSpan = streamData.AsSpan();

        for (var streamIndex = 0; streamIndex < streamData.Length - markerLength; streamIndex++)
        {
            var substringOfSpan = streamSpan.Slice(streamIndex, markerLength);

            var foundChars = 0;
            var allCharsUnique = true;
            foreach (var character in substringOfSpan)
            {
                var bitMask = 1 << character;
                if ((foundChars & bitMask) != 0)
                {
                    allCharsUnique = false;
                    break;
                }
                foundChars |= bitMask;
            }

            if (allCharsUnique)
            {
                return streamIndex + markerLength;
            }
        }

        throw new ArgumentException($"The stream data did not contain a {markerLength}-character Start of Packet Marker. '{streamData}'");
    }
}