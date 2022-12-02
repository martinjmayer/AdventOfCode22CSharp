using System.Diagnostics;

namespace AdventOfCode22CSharpTests;

public class Day1CalorieCountingTests
{
    [TestCase("0", 0)]
    [TestCase("1", 1)]
    [TestCase("1000", 1000)]
    [TestCase("010", 10)]
    [TestCase("1000,129", 1129)]
    [TestCase("5,6,7,8,9", 35)]
    [TestCase("1,1,1,1,1", 5)]
    [TestCase("23747,42,1,1,1", 23792)]
    public void Elf_TotalCalories_ReturnsSumOfCaloriesOfAllItems(string data, int expectedTotalCalories)
    {
        var elf = new Elf(data.Split(',').Select(int.Parse).ToList());
        
        Assert.That(elf.TotalCalories, Is.EqualTo(expectedTotalCalories));
    }
    
    [Test]
    public void ElfDataParser_Parse_WhenInputIsEmpty_ReturnsNoElves()
    {
        var parser = new ElfDataParser();
        var elves = parser.Parse(String.Empty);
        
        Assert.That(elves.Count(), Is.EqualTo(0));
    }

    [TestCase("1000\n300\n5000\n\n\n\n\n\n", 1)]
    [TestCase("1000\n300\n5000\n\n3928\n2008\n4592\n\n3711\n1239", 3)]
    [TestCase("1000\n300\n5000\n\n\n\n3928\n2008\n4592\n\n\n3711\n1239", 3)]
    public void ElfDataParser_Parse_ReturnsNumberOfElvesInData(string data, int expectedNumberOfElves)
    {
        var parser = new ElfDataParser();
        var elves = parser.Parse(data);

        Assert.That(elves.Count(), Is.EqualTo(expectedNumberOfElves));
    }
    
    [TestCase("1000\n300\n5000\n\n\n\n\n\n", 6300)]
    [TestCase("1000\n300\n5000\n\n3928\n2008\n4592\n\n3711\n1239", 10528)]
    [TestCase("1000\n300\n5000\n\n\n\n3928\n2008\n4592\n\n\n3711\n1239", 10528)]
    public void ElfCalorieRanker_GetElfWithMostCalories_ReturnsHighestNumberOfCaloriesOfAnyElf(string data, int expectedHighestNumOfCalories)
    {
        var parser = new ElfDataParser();
        var elves = parser.Parse(data);

        var ranker = new ElfCalorieRanker();
        var topElf = ranker.GetElfWithMostCalories(elves);

        Assert.That(topElf?.TotalCalories ?? 0, Is.EqualTo(expectedHighestNumOfCalories));
    }

    [TestCase("1\n1\n1\n1\n\n1\n1\n1\n\n1\n1\n\n1", 9)]
    [TestCase("7\n7\n7\n\n5\n5\n5\n\n3\n3\n3\n\n1\n1\n1", 45)]
    public void ElfCalorieRanker_GetTotalCaloriesOfTopElvesByMostCalories_With3Elves_ReturnsTop3ElvesCombinedCalories(string data, int expectedCombinedCalories)
    {
        var parser = new ElfDataParser();
        var elves = parser.Parse(data);

        var ranker = new ElfCalorieRanker();
        var combinedCalories = ranker.GetTotalCaloriesOfTopElvesByMostCalories(elves, 3);

        Assert.That(combinedCalories, Is.EqualTo(expectedCombinedCalories));
    }
    
    // File Tests

    [Test]
    public void ElfCalorieRanker_GetElfWithMostCalories_Input1File_ReturnsAResult()
    {
        var data = File.ReadAllText("Input1.txt");
        
        var parser = new ElfDataParser();
        var elves = parser.Parse(data);
        
        var ranker = new ElfCalorieRanker();
        var topElf = ranker.GetElfWithMostCalories(elves);
        
        var actualTotalCalories = topElf?.TotalCalories ?? 0;
        
        Debug.WriteLine(actualTotalCalories);
        Assert.That(actualTotalCalories, Is.GreaterThan(0));
    }
    
    [Test]
    public void ElfCalorieRanker_GetTotalCaloriesOfTopElvesByMostCalories_Input1FileWith3Elves_ReturnsAResult()
    {
        var data = File.ReadAllText("Input1.txt");
        
        var parser = new ElfDataParser();
        var elves = parser.Parse(data);

        var ranker = new ElfCalorieRanker();
        var combinedCalories = ranker.GetTotalCaloriesOfTopElvesByMostCalories(elves, 3);

        Assert.That(combinedCalories, Is.GreaterThan(0));
    }
}

public class ElfCalorieRanker
{
    public Elf? GetElfWithMostCalories(IEnumerable<Elf?> elves)
    {
        return elves.MaxBy(elf => elf?.TotalCalories ?? 0);
    }

    private IEnumerable<Elf?> GetTopElvesByMostCalories(IEnumerable<Elf?> elves, int numberOfElves)
    {
        return elves.OrderByDescending(elf => elf?.TotalCalories ?? 0).Take(numberOfElves);
    }
    
    public int GetTotalCaloriesOfTopElvesByMostCalories(IEnumerable<Elf?> elves, int numberOfElves)
    {
        return GetTopElvesByMostCalories(elves, numberOfElves).Select(elf => elf?.TotalCalories ?? 0).Sum();
    }
}

public class ElfDataParser
{
    public IEnumerable<Elf?> Parse(string data)
    {
        if (data == string.Empty)
        {
            return new List<Elf?>();
        }
        
        var normalisedData = NormaliseData(data);

        return normalisedData.Split("\n\n")
            .Select(elfData => new Elf(elfData.Split()
                .Select(foodCaloriesString =>
                    int.TryParse(foodCaloriesString, out var foodCalories)
                        ? foodCalories : 0)));
    }

    private static string NormaliseData(string data)
    {
        var dataWithoutCarriageReturns = data.Replace("\r", string.Empty);
        var trimmedData = dataWithoutCarriageReturns.Trim('\n');

        var processingData = trimmedData;
        const string tripleNewLine = "\n\n\n";
        while (processingData.Contains(tripleNewLine))
        {
            processingData = processingData.Replace(tripleNewLine, "\n\n");
        }
        
        return processingData;
    }
}

public class Elf
{
    public Elf(IEnumerable<int> calorieLines)
    {
        _calorieLines = calorieLines;
    }

    private readonly IEnumerable<int> _calorieLines;

    public int TotalCalories => _calorieLines.Sum();
}