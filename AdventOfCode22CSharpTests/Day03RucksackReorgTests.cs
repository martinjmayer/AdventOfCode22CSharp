namespace AdventOfCode22CSharpTests;

public class Day03RucksackReorgTests
{
    [TestCase("vJrwpWtwJgWrhcsFMMfFFhFp", ExpectedResult = 'p')]
    [TestCase("jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", ExpectedResult = 'L')]
    [TestCase("PmmdzqPrVvPwwTWBwg", ExpectedResult = 'P')]
    [TestCase("wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn", ExpectedResult = 'v')]
    [TestCase("ttgJtRGJQctTZtZT", ExpectedResult = 't')]
    [TestCase("CrZsJsPPZsGzwwsLwLmpwMDw", ExpectedResult = 's')]
    public char GetSharedItem_ReturnsExpectedCharFromCaseData(string rucksackData)
    {
       return GetSharedItemAcrossBothCompartments(rucksackData);
    }

    [TestCase('p', ExpectedResult = 16)]
    [TestCase('L', ExpectedResult = 38)]
    [TestCase('P', ExpectedResult = 42)]
    [TestCase('v', ExpectedResult = 22)]
    [TestCase('t', ExpectedResult = 20)]
    [TestCase('s', ExpectedResult = 19)]
    public int GetPriorityOfItem_ReturnsExpectedPriority(char itemId)
    {
        return GetPriorityOfItem(itemId);
    }
    
    [TestCase("vJrwpWtwJgWrhcsFMMfFFhFp\njqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL\n" +
              "PmmdzqPrVvPwwTWBwg\nwMqvLMZHhHMvwLHjbvcjnnSBnvTQFn\nttgJtRGJQctTZtZT\n" +
              "CrZsJsPPZsGzwwsLwLmpwMDw", ExpectedResult = 157)]
    public int GetTotalPriority_ReturnsExpectedTotal(string data)
    {
        return GetTotalPriority(data);
    }

    [TestCase("vJrwpWtwJgWrhcsFMMfFFhFp\njqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL\n"+
        "PmmdzqPrVvPwwTWBwg\nwMqvLMZHhHMvwLHjbvcjnnSBnvTQFn\nttgJtRGJQctTZtZT\n"+
        "CrZsJsPPZsGzwwsLwLmpwMDw", ExpectedResult=70)]
    [TestCase("abcP\ndefP\nghiP\nABCO\nDEFO\nGHIO\nzyxQ\nwvuQ\ntsrQ", ExpectedResult = 126)]
    public int GetTotalPriorityOfGroupBadges_ReturnsExpectedTotal(string data)
    {
        return GetTotalPriorityOfGroupBadges(data);
    }
    
    // FileTests
    
    [TestCase("Day03_Input1.txt", ExpectedResult = 7821)]
    public int GetTotalPriority_FileTest_ReturnsExpectedTotal(string filename)
    {
        var data = File.ReadAllText(filename);
        var totalPriority = GetTotalPriority(data);
        
        Assert.That(totalPriority, Is.GreaterThan(0));
        return totalPriority;
    }
    
    [TestCase("Day03_Input1.txt", ExpectedResult = 2752)]
    public int GetTotalPriorityOfGroupBadges_FileTest_ReturnsExpectedTotal(string filename)
    {
        var data = File.ReadAllText(filename);
        var totalPriority = GetTotalPriorityOfGroupBadges(data);
        
        Assert.That(totalPriority, Is.GreaterThan(0));
        return totalPriority;
    }
    
    // Implementation

    private int GetTotalPriorityOfGroupBadges(string data)
    {
        var normalisedData = NormaliseData(data);
        var rucksacks = normalisedData.Split('\n');
        return rucksacks.Select((sack, index) => (sack, index))
            .GroupBy(indexedSack => indexedSack.index / 3)
            .Select(GetSharedBadge)
            .Select(GetPriorityOfItem)
            .Sum();
    }

    private char GetSharedBadge(IGrouping<int, (string sack, int index)> group)
    {
        var listOfLists = group.Select(list => list.sack);
        return listOfLists.Skip(1)
            .Aggregate(
                new HashSet<char>(listOfLists.First()),
                (h, e) =>
                {
                    h.IntersectWith(e);
                    return h;
                }
            ).Single();
    }
    
    private int GetTotalPriority(string data)
    {
        var normalisedData = NormaliseData(data);
        var rucksacks = normalisedData.Split('\n');
        return rucksacks.Select(GetSharedItemAcrossBothCompartments).Select(GetPriorityOfItem).Sum();
    }
    
    private static string NormaliseData(string data)
    {
        var dataWithoutCarriageReturns = data.Replace("\r", string.Empty);
        var trimmedData = dataWithoutCarriageReturns.Trim('\n');

        var processingData = trimmedData;
        const string doubleNewLine = "\n\n";
        while (processingData.Contains(doubleNewLine))
        {
            processingData = processingData.Replace(doubleNewLine, "\n");
        }
        
        return processingData;
    }
    
    private char GetSharedItemAcrossBothCompartments(string rucksackData)
    {
        var contentsOfFirstCompartment = rucksackData.Take(rucksackData.Length / 2);
        var contentsOfSecondCompartment = rucksackData.Skip(rucksackData.Length / 2);

        var sharedItems = GetSharedItemsAcross2Groups(contentsOfFirstCompartment, contentsOfSecondCompartment);

        return sharedItems.Single();
    }

    private static IEnumerable<char> GetSharedItemsAcross2Groups(
        IEnumerable<char> sackA,
        IEnumerable<char> sackB)
    {
        var sharedItems = sackA.Intersect(sackB);
        return sharedItems;
    }
    
    private static int GetPriorityOfItem(char itemId)
    {
        var numericItemId = (int)itemId;
        return numericItemId switch
        {
            >= 65 and <= 90 => numericItemId - 38,
            >= 97 and <= 122 => numericItemId - 96
        };
    }
}