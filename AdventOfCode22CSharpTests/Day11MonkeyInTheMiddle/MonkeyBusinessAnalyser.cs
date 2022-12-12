using System.Text.RegularExpressions;

namespace AdventOfCode22CSharpTests.Day11MonkeyInTheMiddle;

public static class MonkeyBusinessAnalyser
{
    private const int WorryAdjustmentIsOldWorryValue = -1;

    public static long GetLevelOfMonkeyBusiness(string data, int numberOfRounds, bool worryReducedWhenMonkeyIsBored)
    {
        var monkeys = ParseAllMonkeysData(data).ToArray();
        
        var leastCommonMultiple = LeastCommonMultiple(monkeys.Select(x => x.TestDivisor));

        for (var count = 0; count < numberOfRounds; count++)
        {
            for (var monkeyIndex = 0; monkeyIndex < monkeys.Length; monkeyIndex++)
            {
                var monkey = monkeys[monkeyIndex];

                if (monkey.ItemsWorries.Count == 0)
                {
                    continue;
                }

                for (var index = 0; index < monkey.ItemsWorries.Count; index++)
                {
                    var itemWorry = monkey.ItemsWorries[index];

                    // Monkey inspection - increases worry
                    monkey.InspectionCount++;
                
                    var adjustment = monkey.WorryAdjustment;
                    if (adjustment == -1)
                    {
                        adjustment = itemWorry;
                    }

                    itemWorry = monkey.WorryOperator switch
                    {
                        MathematicalOperator.Addition => itemWorry + adjustment,
                        MathematicalOperator.Multiplication => itemWorry * adjustment,
                        _ => throw new ArgumentException("Unsupported Mathematical Operator.")
                    };

                    // Monkey gets bored
                    if (worryReducedWhenMonkeyIsBored)
                    {
                        itemWorry /= 3;
                    }

                    // Monkey tests worry to decide where to throw item
                    var monkeyTestDivisorModulus = itemWorry % monkey.TestDivisor;
                
                    if (itemWorry >= leastCommonMultiple)
                    {
                        itemWorry %= leastCommonMultiple;
                    }
                
                    if (monkeyTestDivisorModulus == 0)
                    {
                        // Throw to 'true' monkey
                        monkeys[monkey.MonkeyToThrowToIfTestTrue].ItemsWorries.Add(itemWorry);
                    }
                    else
                    {
                        // Throw to false monkey
                        monkeys[monkey.MonkeyToThrowToIfTestFalse].ItemsWorries.Add(itemWorry);
                    }
                }

                // Clear all items worries as all items thrown
                monkey.ItemsWorries.Clear();

                monkeys[monkeyIndex] = monkey;
            }
        }

        var top2InspectionCounts =
            monkeys.Select(monkey => monkey.InspectionCount).OrderDescending().Take(2).ToArray();

        var levelOfMonkeyBusiness = top2InspectionCounts[0] * top2InspectionCounts[1];
        return levelOfMonkeyBusiness;
    }

    private static int LeastCommonMultiple(IEnumerable<int> numbers)
    {
        return numbers.Aggregate(LeastCommonMultiple);
    }

    private static int LeastCommonMultiple(int a, int b)
    {
        return Math.Abs(a * b) / GreatestCommonFactor(a, b);
    }

    private static int GreatestCommonFactor(int a, int b)
    {
        while (true)
        {
            if (b == 0) return a;
            var a1 = a;
            a = b;
            b = a1 % b;
        }
    }

    private static IEnumerable<Monkey> ParseAllMonkeysData(string data)
    {
        return data.Split("\n\n").Select(ParseMonkeyData);
    }

    // 1 = monkeyNumber / 2 = Starting Items / 3 = Worry Operator / 4 = Worry Adjustment
    // 5 = TestDivisor / 6 = MonkeyToThrowToIfTestTrue / 7 = MonkeyToThrowToIfTestFalse
    private static readonly Regex _monkeyDataRegex
        = new(@"Monkey ([0-9]+)[:].+Starting items[:] ([0-9\s,]+).+Operation[:] new [=] old ([*+]) ([0-9]+|old).+Test[:] divisible by ([0-9]+).+If true[:] throw to monkey ([0-9+]).+If false[:] throw to monkey ([0-9+])",
            RegexOptions.Singleline);
    
    private static Monkey ParseMonkeyData(string monkeyData)
    {
        var match = _monkeyDataRegex.Match(monkeyData);

        if (!match.Success)
        {
            throw new ArgumentException($"Invalid monkey data: '{monkeyData}'");
        }

        var items = match.Groups[2].Value.Split(", ").Select(long.Parse);
        var worryOperator = match.Groups[3].Value switch
        {
            "+" => MathematicalOperator.Addition,
            "*" => MathematicalOperator.Multiplication,
            _ => throw new Exception("Unknown operator")
        };
        var worryAdjustString = match.Groups[4].Value;
        var worryAdjustment = worryAdjustString == "old" ? WorryAdjustmentIsOldWorryValue : int.Parse(worryAdjustString);
        var testDivisor = int.Parse(match.Groups[5].Value);
        var monkeyThrowToIfTestTrue= int.Parse(match.Groups[6].Value);
        var monkeyThrowToIfTestFalse= int.Parse(match.Groups[7].Value);
            
        return new Monkey(items, worryOperator, worryAdjustment,
            testDivisor, monkeyThrowToIfTestTrue, monkeyThrowToIfTestFalse);
    }

    private class Monkey
    {
        public Monkey(IEnumerable<long> items, MathematicalOperator worryOperator, int worryAdjustment, int testDivisor, int monkeyToThrowToIfTestTrue, int monkeyToThrowToIfTestFalse)
        {
            ItemsWorries = items.ToList();
            WorryOperator = worryOperator;
            WorryAdjustment = worryAdjustment;
            TestDivisor = testDivisor;
            MonkeyToThrowToIfTestTrue = monkeyToThrowToIfTestTrue;
            MonkeyToThrowToIfTestFalse = monkeyToThrowToIfTestFalse;
            InspectionCount = 0; 
        }

        public List<long> ItemsWorries { get; }
        public MathematicalOperator WorryOperator { get; }
        public long WorryAdjustment { get; }
        public int TestDivisor { get; }
        public int MonkeyToThrowToIfTestTrue { get; }
        public int MonkeyToThrowToIfTestFalse { get; }
        public long InspectionCount { get; set; }
    }
}