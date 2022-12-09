namespace AdventOfCode22CSharpTests;

public class Day08TreeTopTreehouseTests_File
{
    [TestCase("Day08_Input1.txt", ExpectedResult = 1818)]
    public int TreeHeightAnalyser_GetNumVisibleTrees_ReturnsExpectedInt(string filename)
    {
        var input = FileEmbedHelper.GetInputFromFile(filename);
        return TreeHeightAnalyser.GetNumVisibleTrees(input);
    }

    [TestCase("Day08_Input1.txt", ExpectedResult = 368368)]
    public int TreeHeightAnalyser_GetHighestScenicScore_ReturnsExpectedInt(string filename)
    {
        var input = FileEmbedHelper.GetInputFromFile(filename);
        return TreeHeightAnalyser.GetHighestScenicScore(input);
    }
}
public class Day08TreeTopTreehouseTests
{
    [TestCase("30373\n25512\n65332\n33549\n35390", ExpectedResult = 21)]
    public int TreeHeightAnalyser_GetNumVisibleTrees_ReturnsExpectedInt(string input)
    {
        return TreeHeightAnalyser.GetNumVisibleTrees(input);
    }
    
    [TestCase("30373\n25512\n65332\n33549\n35390", ExpectedResult = 8)]
    public int TreeHeightAnalyser_GetHighestScenicScore_ReturnsExpectedInt(string input)
    {
        return TreeHeightAnalyser.GetHighestScenicScore(input);
    }
}

public static class TreeHeightAnalyser
{
    public static int GetHighestScenicScore(string treeData)
    {
        var thm = GetTreeHeightMatrix(treeData);
        var rows = thm.Rows.ToArray().AsSpan();
        var columns = thm.Columns.ToArray().AsSpan();

        var highestScenicScore = 0;
        
        for (var y = 1; y < thm.NumRows-1; y++)
        {
            for (var x = 1; x < thm.NumColumns-1; x++)
            {
                var scenicScoreElements = GetScenicScoreElementsForTree(columns, rows, x, y);
                var scenicScore = scenicScoreElements.Aggregate(1, (a, b) => a * b);
                if (scenicScore > highestScenicScore)
                {
                    highestScenicScore = scenicScore;
                }
            }
        }
        
        // var scenicScoreElements = GetScenicScoreElementsForTree(columns, rows, 2, 3);
        // var scenicScore = scenicScoreElements.Aggregate(1, (a, b) => a * b);

        return highestScenicScore;
    }

    private static IEnumerable<int> GetScenicScoreElementsForTree(Span<int[]> columns, Span<int[]> rows, int x, int y)
    {
        var scenicScore = new List<int>();
        var treeHeight = rows[y][x];

        var aboveScore = GetScenicScoreForDirection(treeHeight, columns[x][..y].Reverse().ToArray());
        scenicScore.Add(aboveScore);
        if (aboveScore == 0)
        {
            return new[] { 0 };
        }
        
        var belowScore = GetScenicScoreForDirection(treeHeight, columns[x][(y + 1)..]);
        scenicScore.Add(belowScore);
        if (belowScore == 0)
        {
            return new[] { 0 };
        }

        var leftScore = GetScenicScoreForDirection(treeHeight, rows[y][..x].Reverse().ToArray());
        scenicScore.Add(leftScore);
        if (leftScore == 0)
        {
            return new[] { 0 };
        }
        
        var rightScore = GetScenicScoreForDirection(treeHeight, rows[y][(x + 1)..]);
        scenicScore.Add(rightScore);
        if (rightScore == 0)
        {
            return new[] { 0 };
        }

        return scenicScore;
    }

    private static int GetScenicScoreForDirection(int treeHeight, IReadOnlyList<int> trees)
    {
        for (var i = 0; i < trees.Count; i++)
        {
            var comparisonTreeHeight = trees[i];
            if (comparisonTreeHeight >= treeHeight)
            {
                return i + 1;
            }
        }
        
        return trees.Count;
    }

    public static int GetNumVisibleTrees(string treeData)
    {
        var thm = GetTreeHeightMatrix(treeData);
        var treesVisible = GetExteriorPerimeterTreeCount(thm.NumColumns, thm.NumRows);
        var rows = thm.Rows.ToArray().AsSpan();
        var columns = thm.Columns.ToArray().AsSpan();
        
        for (var y = 1; y < thm.NumRows-1; y++)
        {
            for (var x = 1; x < thm.NumColumns-1; x++)
            {
                var treeHeight = rows[y][x];
                
                var above = columns[x][..y];
                var below = columns[x][(y + 1)..];
                var left = rows[y][..x];
                var right = rows[y][(x + 1)..];

                if (above.Max() < treeHeight || left.Max() < treeHeight || right.Max() < treeHeight|| below.Max() < treeHeight)
                {
                    treesVisible++;
                }
            }
        }

        return treesVisible;
    }

    private static int GetExteriorPerimeterTreeCount(int numColumns, int numRows)
    {
        // rectangle perimeter - 4 corners that are double-counted
        return numColumns + numColumns + numRows + numRows - 4;
    }

    private static TreeHeightMatrix GetTreeHeightMatrix(string treeData)
    {
        var rows = treeData.Trim().TrimEnd('\n').Split('\n').Select(row => row.Select(tree => int.Parse(tree.ToString())).ToArray()).ToArray();
        var numColumns = rows[0].Length;
        var numRows = rows.Length;
        var columns = Enumerable.Range(1, numColumns)
            .Select(_ => 
                Enumerable.Range(1, numRows)
                    .Select(_ => -1)
                    .ToArray())
            .ToArray();
        for(var x = 0; x < numColumns; x++)
        {
            for(var y = 0; y < numRows; y++)
            {
                columns[x][y] = int.Parse(rows[y][x].ToString());
            }
        }
        
        return new TreeHeightMatrix(columns,rows);
    }
}

public class TreeHeightMatrix
{
    public TreeHeightMatrix(IEnumerable<int[]> columns, IEnumerable<int[]> rows)
    {
        Columns = columns;
        Rows = rows;
    }
    public int NumColumns => Columns.Count();
    public int NumRows => Rows.Count();
    
    public IEnumerable<int[]> Columns { get; }
    public IEnumerable<int[]> Rows { get; }
}