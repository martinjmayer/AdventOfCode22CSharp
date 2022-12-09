namespace AdventOfCode22CSharpTests;

public class Day09RopeBridgeTests_File
{
    [TestCase("Day09_Input1.txt", ExpectedResult = 5710)]
    public int GetNumberOfTailPositions_1_ReturnsExpectedInt(string filename)
    {
        var input = FileEmbedHelper.GetInputFromFile(filename);
        return TailTracker.GetNumberOfTailPositions(input, 2);
    }
    
    [TestCase("Day09_Input1.txt", ExpectedResult = 2259)]
    public int GetNumberOfTailPositions_10_ReturnsExpectedInt(string filename)
    {
        var input = FileEmbedHelper.GetInputFromFile(filename);
        return TailTracker.GetNumberOfTailPositions(input, 10);
    }
}

// Rewrite the following code to run faster whilst preserving the tests

public class Day09RopeBridgeTests
{
    [TestCase("R 4\nU 4\nL 3\nD 1\nR 4\nD 1\nL 5\nR 2", ExpectedResult = 13)]
    public int GetNumberOfTailPositions_1_ReturnsExpectedInt(string input)
    {
        return TailTracker.GetNumberOfTailPositions(input, 2);
    }
    
    [TestCase("R 5\nU 8\nL 8\nD 3\nR 17\nD 10\nL 25\nU 20", ExpectedResult = 36)]
    [TestCase("R 4\nU 4\nL 3\nD 1\nR 4\nD 1\nL 5\nR 2", ExpectedResult = 1)]
    public int GetNumberOfTailPositions_10_ReturnsExpectedInt(string input)
    {
        return TailTracker.GetNumberOfTailPositions(input, 10);
    }
}

public static class TailTracker
{
    public static int GetNumberOfTailPositions(string data, int numberOfNodes)
    {
        var movements = data.TrimEnd('\n').Split('\n').SelectMany(GetDirectionCoordinate);

        var nodes = Enumerable.Range(1, numberOfNodes).Select(_ => new Coordinate()).ToArray();
        var positions = new HashSet<string>();
        foreach (var move in movements)
        { 
            nodes[0] += move;
            for (var i = 1; i < numberOfNodes; i++)
            {
                var leader = nodes[i - 1];
                var follower = nodes[i];
                
                follower = MoveNextNode(leader, follower);
                nodes[i - 1] = leader;
                nodes[i] = follower;
            }

            positions.Add(nodes.Last().GetIdentifier());
        }
        
        return positions.Count;
    }

    private static Coordinate MoveNextNode(Coordinate leader, Coordinate follower)
    {
        var diff = leader - follower;
        if (IsTouching(diff))
        {
            return follower;
        }
        follower.X += Math.Sign(diff.X);
        follower.Y += Math.Sign(diff.Y);

        return follower;
    }

    private static bool IsTouching(Coordinate diff)
    {
        return Math.Abs(diff.X) <= 1 && Math.Abs(diff.Y) <= 1;
    }

    private static IEnumerable<Coordinate> GetDirectionCoordinate(string movementString)
    {
        var numMoves = int.Parse(movementString[2..]);
        return Enumerable.Range(1, numMoves).Select(_ => CreateCoordinate(movementString[0]));
    }

    private static Coordinate CreateCoordinate(char direction)
    {
        return direction switch
        {
            'U' => new Coordinate(0, -1),
            'D' => new Coordinate(0, 1),
            'L' => new Coordinate(-1, 0),
            'R' => new Coordinate(1, 0),
            _ => throw new Exception($"Unknown direction: {direction}")
        };
    }
}


public struct Coordinate
{
    public Coordinate() : this(0,0){}
    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X;
    public int Y;

    public static Coordinate operator+ (Coordinate a, Coordinate b) {
        var coord = new Coordinate(a.X + b.X, a.Y + b.Y);
        return coord;
    }
    public static Coordinate operator- (Coordinate a, Coordinate b) {
        var coord = new Coordinate(a.X - b.X, a.Y - b.Y);
        return coord;
    }
    
    public string GetIdentifier()
    {
        return $"{X}_{Y}";
    }

    public override string ToString()
    {
        return GetIdentifier();
    }
}