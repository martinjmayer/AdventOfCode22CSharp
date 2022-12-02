using System.Data;

namespace AdventOfCode22CSharpTests;

public class Day02RockPaperScissorsTests
{
    [TestCase(Shape.Rock, ExpectedResult = 1)]
    [TestCase(Shape.Paper, ExpectedResult = 2)]
    [TestCase(Shape.Scissors, ExpectedResult = 3)]
    public int ShapeScorer_GetScore_ReturnsExpectedScoreForEachShape(Shape shape)
    {
        return ShapeScorer.GetScore(shape);
    }

    [TestCase(RoundResult.Loss, ExpectedResult = 0)]
    [TestCase(RoundResult.Draw, ExpectedResult = 3)]
    [TestCase(RoundResult.Win, ExpectedResult = 6)]
    public int ResultScorer_GetScore_ReturnsExpectedScoreForEachResult(RoundResult result)
    {
        return ResultScorer.GetScore(result);
    }
    
    [TestCase(Shape.Rock, Shape.Paper, ExpectedResult = RoundResult.Loss)]
    [TestCase(Shape.Paper, Shape.Rock, ExpectedResult = RoundResult.Win)]
    [TestCase(Shape.Scissors, Shape.Scissors, ExpectedResult = RoundResult.Draw)]
    [TestCase(Shape.Rock, Shape.Scissors, ExpectedResult = RoundResult.Win)]
    [TestCase(Shape.Paper, Shape.Paper, ExpectedResult = RoundResult.Draw)]
    [TestCase(Shape.Scissors, Shape.Rock, ExpectedResult = RoundResult.Loss)]
    [TestCase(Shape.Rock, Shape.Rock, ExpectedResult = RoundResult.Draw)]
    [TestCase(Shape.Paper, Shape.Scissors, ExpectedResult = RoundResult.Loss)]
    [TestCase(Shape.Scissors, Shape.Paper, ExpectedResult = RoundResult.Win)]
    public RoundResult RoundCalculator_GetProtagonistResult_ReturnsExpectedResultForEachRound(Shape shapeA, Shape shapeB)
    {
        return RoundCalculator.GetProtagonistResult(shapeA, shapeB);
    }

    [TestCase(Shape.Paper, Shape.Rock, RoundResult.Win, ExpectedResult = "[Paper/Rock/Win]")]
    [TestCase(Shape.Rock, Shape.Paper, RoundResult.Loss, ExpectedResult = "[Rock/Paper/Loss]")]
    [TestCase(Shape.Scissors, Shape.Scissors, RoundResult.Draw, ExpectedResult = "[Scissors/Scissors/Draw]")]
    public string RoundGuidance_ToString_ReturnsInExpectedFormat(
        Shape protagonistShape,
        Shape antagonistShape,
        RoundResult protagonistResult)
    {
        return new RoundGuidance(new RoundPlays(protagonistShape, antagonistShape), protagonistResult).ToString();
    }
    
    [Test]
    public void StrategyGuide_ToString_ReturnsInExpectedFormat()
    {
        var expectedString = "[Paper/Rock/Win]--[Scissors/Scissors/Draw]";
        var strategyGuide = new StrategyGuide(new List<RoundGuidance>()
        {
            new(new RoundPlays(Shape.Paper, Shape.Rock), RoundResult.Win),
            new(new RoundPlays(Shape.Scissors, Shape.Scissors), RoundResult.Draw)
        });
        Assert.That(strategyGuide.ToString(), Is.EqualTo(expectedString));
    }

    [TestCase("A Y\nB X\nC Z", ExpectedResult = "[Paper/Rock/Win]--[Rock/Paper/Loss]--[Scissors/Scissors/Draw]")]
    public string FirstStrategyGuideParser_Parse_ReturnsExpectedStrategyGuideForEachString(string guideString)
    {
        var parser = (IStrategyGuideParser)new FirstStrategyGuideParser();
        var strategyGuide = parser.Parse(guideString);
        return strategyGuide.ToString();
    }

    [TestCase("A Y\nB X\nC Z", ExpectedResult = "[Rock/Rock/Draw]--[Rock/Paper/Loss]--[Rock/Scissors/Win]")]
    public string SecondStrategyGuideParser_Parse_ReturnsExpectedStrategyGuideForEachString(string guideString)
    {
        var parser = (IStrategyGuideParser)new SecondStrategyGuideParser();
        var strategyGuide = parser.Parse(guideString);
        return strategyGuide.ToString();
    }

    [TestCase("A Y\nB X\nC Z", ExpectedResult = 15)]
    public int StrategyGuideScorer_WithFirstStrategyGuideParser_GetScore_ReturnsExpectedStrategyGuideScore(string guideString)
    {
        var parser = (IStrategyGuideParser)new FirstStrategyGuideParser();
        var strategyGuide = parser.Parse(guideString);
        return StrategyGuideScorer.GetScore(strategyGuide);
    }

    [TestCase("A Y\nB X\nC Z", ExpectedResult = 12)]
    public int StrategyGuideScorer_WithSecondStrategyGuideParser_GetScore_ReturnsExpectedStrategyGuideScore(string guideString)
    {
        var parser = (IStrategyGuideParser)new SecondStrategyGuideParser();
        var strategyGuide = parser.Parse(guideString);
        return StrategyGuideScorer.GetScore(strategyGuide);
    }
    
    // File Tests

    [Test]
    public void FirstStrategyGuideParser_Parse_InputFile1_ReturnsAStrategyGuide()
    {
        var data = File.ReadAllText("Day02_Input1.txt");
        var parser = (IStrategyGuideParser)new FirstStrategyGuideParser();
        var strategyGuide = parser.Parse(data);
        
        Assert.That(strategyGuide.Rounds.Count(), Is.GreaterThan(0));
    }

    [Test]
    public void StrategyGuideScorer_WithFirstStrategyGuideParser_GetScore_InputFile1_ReturnsAStrategyGuideScore()
    {
        var data = File.ReadAllText("Day02_Input1.txt");
        var parser = (IStrategyGuideParser)new FirstStrategyGuideParser();
        var strategyGuide = parser.Parse(data);
        var score = StrategyGuideScorer.GetScore(strategyGuide);
        
        Assert.That(score, Is.GreaterThan(0));
    }

    [Test]
    public void SecondStrategyGuideParser_Parse_InputFile1_ReturnsAStrategyGuide()
    {
        var data = File.ReadAllText("Day02_Input1.txt");
        var parser = (IStrategyGuideParser)new SecondStrategyGuideParser();
        var strategyGuide = parser.Parse(data);
        
        Assert.That(strategyGuide.Rounds.Count(), Is.GreaterThan(0));
    }

    [Test]
    public void StrategyGuideScorer_WithSecondStrategyGuideParser_GetScore_InputFile1_ReturnsAStrategyGuideScore()
    {
        var data = File.ReadAllText("Day02_Input1.txt");
        var parser = (IStrategyGuideParser)new SecondStrategyGuideParser();
        var strategyGuide = parser.Parse(data);
        var score = StrategyGuideScorer.GetScore(strategyGuide);
        
        Assert.That(score, Is.GreaterThan(0));
    }
}

public static class StrategyGuideScorer
{
    public static int GetScore(StrategyGuide guide)
    {
        return guide.Rounds.Select(round =>
            ResultScorer.GetScore(round.ProtagonistResult)
            + ShapeScorer.GetScore(round.Plays.ProtagonistShape))
                .Sum();
    } 
}

public class FirstStrategyGuideParser : IStrategyGuideParser
{
    public StrategyGuide Parse(string data)
    {
        var normalisedData = NormaliseData(data);
        
        ValidateData(normalisedData);

        return new StrategyGuide(
            normalisedData.Split('\n')
                .Select(roundString =>
                    new RoundPlays(
                        GetProtagonistShape(roundString),
                        GetAntagonistShape(roundString)))
                            .Select(roundPlay =>
                                new RoundGuidance(
                                    roundPlay,
                                    RoundCalculator.GetProtagonistResult(
                                        roundPlay.ProtagonistShape,
                                        roundPlay.AntagonistShape))));
    }

    private void ValidateData(string normalisedData)
    {
        var lines = normalisedData.Split('\n');

        for (var i = 0; i < lines.Length; i++)
        {
            var checkLine = lines[i];
            if (
                checkLine.Length == 3
                    && new[]{ 'A', 'B', 'C' }.Contains(checkLine[0])
                        && checkLine[1] == ' '
                            && new[]{ 'X', 'Y', 'Z' }.Contains(checkLine[2]))
            {
                continue;
            }
            
            throw new DataException($"Check line {i+1}. '{checkLine}' All lines must be 3 characters long. The 1st character must be X,Y or Z. The 2nd character must be space. The 3rd character must be A,B or C.");
        }
    }

    private Shape GetAntagonistShape(string roundString)
    {
        var antagonistCharacter = roundString[0];

        return antagonistCharacter switch
        {
            'A' => Shape.Rock,
            'B' => Shape.Paper,
            'C' => Shape.Scissors
        };
    }

    private Shape GetProtagonistShape(string roundString)
    {
        var protagonistCharacter = roundString[2];

        return protagonistCharacter switch
        {
            'X' => Shape.Rock,
            'Y' => Shape.Paper,
            'Z' => Shape.Scissors
        };
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
}

public class SecondStrategyGuideParser : IStrategyGuideParser
{
    public StrategyGuide Parse(string data)
    {
        var normalisedData = NormaliseData(data);
        
        ValidateData(normalisedData);

        return new StrategyGuide(
            normalisedData.Split('\n')
                .Select(roundString =>
                    new RoundPlays(
                        RoundCalculator.GetProtagonistShapeToAchieveResult(
                            GetProtagonistResult(roundString),
                            GetAntagonistShape(roundString)),
                        GetAntagonistShape(roundString)))
                            .Select(roundPlay =>
                                new RoundGuidance(
                                    roundPlay,
                                    RoundCalculator.GetProtagonistResult(
                                        roundPlay.ProtagonistShape,
                                        roundPlay.AntagonistShape))));
    }

    private void ValidateData(string normalisedData)
    {
        var lines = normalisedData.Split('\n');

        for (var i = 0; i < lines.Length; i++)
        {
            var checkLine = lines[i];
            if (
                checkLine.Length == 3
                    && new[]{ 'A', 'B', 'C' }.Contains(checkLine[0])
                        && checkLine[1] == ' '
                            && new[]{ 'X', 'Y', 'Z' }.Contains(checkLine[2]))
            {
                continue;
            }
            
            throw new DataException($"Check line {i+1}. '{checkLine}' All lines must be 3 characters long. The 1st character must be X,Y or Z. The 2nd character must be space. The 3rd character must be A,B or C.");
        }
    }

    private Shape GetAntagonistShape(string roundString)
    {
        var antagonistCharacter = roundString[0];

        return antagonistCharacter switch
        {
            'A' => Shape.Rock,
            'B' => Shape.Paper,
            'C' => Shape.Scissors
        };
    }

    private RoundResult GetProtagonistResult(string roundString)
    {
        var protagonistCharacter = roundString[2];

        return protagonistCharacter switch
        {
            'X' => RoundResult.Loss,
            'Y' => RoundResult.Draw,
            'Z' => RoundResult.Win
        };
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
    
}

public interface IStrategyGuideParser
{
    StrategyGuide Parse(string data);
}

public class StrategyGuide
{
    public StrategyGuide(IEnumerable<RoundGuidance> rounds)
    {
        Rounds = rounds;
    }
    
    public IEnumerable<RoundGuidance> Rounds { get; }

    public override string ToString()
    {
        return !Rounds.Any()
            ? string.Empty
            : Rounds.Select(round => round.ToString()).Aggregate((s1, s2) => s1 + "--" + s2);
    }
}

public class RoundGuidance
{
    public RoundGuidance(RoundPlays plays, RoundResult protagonistResult)
    {
        ProtagonistResult = protagonistResult;
        Plays = plays;
    }
    
    public RoundResult ProtagonistResult { get; }
    public RoundPlays Plays { get; }

    public override string ToString()
    {
        return $"[{Plays.ProtagonistShape}/{Plays.AntagonistShape}/{ProtagonistResult}]";
    }
}

public class RoundPlays
{
    public RoundPlays(Shape protagonistShape, Shape antagonistShape)
    {
        ProtagonistShape = protagonistShape;
        AntagonistShape = antagonistShape;
    }
    
    public Shape ProtagonistShape { get; }
    public Shape AntagonistShape { get; }
}

public static class ShapeScorer
{
    public static int GetScore(Shape shape)
    {
        return shape switch
        {
            Shape.Rock => 1,
            Shape.Paper => 2,
            Shape.Scissors => 3,
            _ => throw new ArgumentOutOfRangeException(nameof(shape), shape, null)
        };
    }
}

public static class ResultScorer
{
    public static int GetScore(RoundResult result)
    {
        return result switch
        {
            RoundResult.Loss => 0,
            RoundResult.Draw => 3,
            RoundResult.Win => 6,
            _ => throw new ArgumentOutOfRangeException(nameof(result), result, null)
        };
    }
}

public static class RoundCalculator
{
    public static RoundResult GetProtagonistResult(Shape protagonistShape, Shape antagonistShape)
    {
        return protagonistShape switch
        {
            Shape.Rock => antagonistShape switch
            {
                Shape.Rock => RoundResult.Draw,
                Shape.Paper => RoundResult.Loss,
                Shape.Scissors => RoundResult.Win,
                _ => throw new ArgumentOutOfRangeException(nameof(antagonistShape), antagonistShape, null)
            },
            Shape.Paper => antagonistShape switch
            {
                Shape.Rock => RoundResult.Win,
                Shape.Paper => RoundResult.Draw,
                Shape.Scissors => RoundResult.Loss,
                _ => throw new ArgumentOutOfRangeException(nameof(antagonistShape), antagonistShape, null)
            },
            Shape.Scissors => antagonistShape switch
            {
                Shape.Rock => RoundResult.Loss,
                Shape.Paper => RoundResult.Win,
                Shape.Scissors => RoundResult.Draw,
                _ => throw new ArgumentOutOfRangeException(nameof(antagonistShape), antagonistShape, null)
            },
            _ => throw new ArgumentOutOfRangeException(nameof(protagonistShape), protagonistShape, null)
        };
    }
    
    
    public static Shape GetProtagonistShapeToAchieveResult(RoundResult protagonistResult, Shape antagonistShape)
    {
        return protagonistResult switch
        {
            RoundResult.Loss => antagonistShape switch
            {
                Shape.Rock => Shape.Scissors,
                Shape.Paper => Shape.Rock,
                Shape.Scissors => Shape.Paper,
                _ => throw new ArgumentOutOfRangeException(nameof(antagonistShape), antagonistShape, null)
            },
            RoundResult.Draw => antagonistShape switch
            {
                Shape.Rock => Shape.Rock,
                Shape.Paper => Shape.Paper,
                Shape.Scissors => Shape.Scissors,
                _ => throw new ArgumentOutOfRangeException(nameof(antagonistShape), antagonistShape, null)
            },
            RoundResult.Win => antagonistShape switch
            {
                Shape.Rock => Shape.Paper,
                Shape.Paper => Shape.Scissors,
                Shape.Scissors => Shape.Rock,
                _ => throw new ArgumentOutOfRangeException(nameof(antagonistShape), antagonistShape, null)
            },
            _ => throw new ArgumentOutOfRangeException(nameof(protagonistResult), protagonistResult, null)
        };
    }
}

public enum Shape
{
    Rock,
    Paper,
    Scissors
}

public enum RoundResult
{
    Loss,
    Draw,
    Win
}
