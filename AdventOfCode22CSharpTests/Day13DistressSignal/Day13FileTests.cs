namespace AdventOfCode22CSharpTests.Day13DistressSignal;

public class Day13FileTests
{
    [TestCase("Day13_Input1.txt", ExpectedResult = -1)]
    public int PacketAnalyser_GetIndexSumOfCorrectlyOrderedPackets_ReturnsExpectedInt(string filename)
    {
        var input = FileEmbedHelper.GetInputFromFile(filename, "AdventOfCode22CSharpTests.Day13DistressSignal");
        return PacketAnalyser.GetIndexSumOfCorrectlyOrderedPackets(input);
    }
}