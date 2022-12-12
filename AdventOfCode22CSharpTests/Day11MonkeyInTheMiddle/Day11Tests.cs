namespace AdventOfCode22CSharpTests.Day11MonkeyInTheMiddle;

public class Day11Tests
{
    [TestCase("Monkey 0:\n  Starting items: 79, 98\n  Operation: new = old * 19\n" +
              "  Test: divisible by 23\n    If true: throw to monkey 2\n" +
              "    If false: throw to monkey 3\n\n" +
              "Monkey 1:\n  Starting items: 54, 65, 75, 74\n" +
              "  Operation: new = old + 6\n  Test: divisible by 19\n    If true: throw to monkey 2\n" +
              "    If false: throw to monkey 0\n\n" +
              "Monkey 2:\n  Starting items: 79, 60, 97\n" +
              "  Operation: new = old * old\n  Test: divisible by 13\n    If true: throw to monkey 1\n" +
              "    If false: throw to monkey 3\n\n" +
              "Monkey 3:\n  Starting items: 74\n  Operation: new = old + 3\n  Test: divisible by 17\n" +
              "    If true: throw to monkey 0\n    If false: throw to monkey 1\n", ExpectedResult = 10605)]
    public long MonkeyBusinessAnalyser_GetLevelOfMonkeyBusiness_20Rounds_ReturnsExpectedInt(string input)
    {
        return MonkeyBusinessAnalyser.GetLevelOfMonkeyBusiness(input, 20, true);
    }
    
    [TestCase("Monkey 0:\n  Starting items: 79, 98\n  Operation: new = old * 19\n" +
              "  Test: divisible by 23\n    If true: throw to monkey 2\n" +
              "    If false: throw to monkey 3\n\n" +
              "Monkey 1:\n  Starting items: 54, 65, 75, 74\n" +
              "  Operation: new = old + 6\n  Test: divisible by 19\n    If true: throw to monkey 2\n" +
              "    If false: throw to monkey 0\n\n" +
              "Monkey 2:\n  Starting items: 79, 60, 97\n" +
              "  Operation: new = old * old\n  Test: divisible by 13\n    If true: throw to monkey 1\n" +
              "    If false: throw to monkey 3\n\n" +
              "Monkey 3:\n  Starting items: 74\n  Operation: new = old + 3\n  Test: divisible by 17\n" +
              "    If true: throw to monkey 0\n    If false: throw to monkey 1\n", ExpectedResult = "2713310158")]
    public string MonkeyBusinessAnalyser_GetLevelOfMonkeyBusiness_10000Rounds_MoreWorry_ReturnsExpectedInt(string input)
    {
        return MonkeyBusinessAnalyser.GetLevelOfMonkeyBusiness(input, 10000, false).ToString();
    }
}