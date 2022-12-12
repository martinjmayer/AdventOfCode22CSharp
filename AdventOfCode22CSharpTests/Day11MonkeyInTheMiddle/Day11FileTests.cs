namespace AdventOfCode22CSharpTests.Day11MonkeyInTheMiddle;

public class Day11FileTests
{
    [TestCase("Day11_Input1.txt", ExpectedResult = 113220)]
    public long MonkeyBusinessAnalyser_GetLevelOfMonkeyBusiness________20_Rounds_ReturnsExpectedInt(string filename)
    {
        var input = FileEmbedHelper.GetInputFromFile(filename, "AdventOfCode22CSharpTests.Day11MonkeyInTheMiddle");
        return MonkeyBusinessAnalyser.GetLevelOfMonkeyBusiness(input, 20, true);
    }
    
    [TestCase("Day11_Input1.txt", ExpectedResult = "30599555965")]
    public string MonkeyBusinessAnalyser_GetLevelOfMonkeyBusiness____10_000_Rounds_MoreWorry_ReturnsExpectedInt(string filename)
    {
        var input = FileEmbedHelper.GetInputFromFile(filename, "AdventOfCode22CSharpTests.Day11MonkeyInTheMiddle");
        return MonkeyBusinessAnalyser.GetLevelOfMonkeyBusiness(input, 10000, false).ToString();
    }
    
    [TestCase("Day11_Input1.txt", ExpectedResult = "1226680869161780")]
    public string MonkeyBusinessAnalyser_GetLevelOfMonkeyBusiness_2_000_000_Rounds_MoreWorry_ReturnsExpectedInt(string filename)
    {
        var input = FileEmbedHelper.GetInputFromFile(filename, "AdventOfCode22CSharpTests.Day11MonkeyInTheMiddle");
        return MonkeyBusinessAnalyser.GetLevelOfMonkeyBusiness(input, 2_000_000, false).ToString();
    }
}