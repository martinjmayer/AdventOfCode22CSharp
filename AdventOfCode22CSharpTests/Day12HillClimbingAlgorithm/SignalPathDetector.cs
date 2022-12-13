namespace AdventOfCode22CSharpTests.Day12HillClimbingAlgorithm;

public static class SignalPathDetector
{
    public static int GetFewestStepsToBestSignal(string input)
    {
        var elevationGrid = new ElevationGrid();
        elevationGrid.Initialize(input.TrimEnd('\n').Split('\n'));
        return GetFewestStepsToBestSignalWithDijkstra(elevationGrid.Start, elevationGrid.Target).Count - 1;
    }

    public static int GetFewestStepsToBestSignalFromAnyStartPoint(string input)
    {
        var elevationGrid = new ElevationGrid();
        elevationGrid.Initialize(input.TrimEnd('\n').Split('\n'));
        return elevationGrid.GridData.Select(row => row.Where(tile => tile != elevationGrid.Target && tile.Elevation == 'a')
            .Select(start => GetFewestStepsToBestSignalWithDijkstra(start, elevationGrid.Target).Count - 1)
            .Where(distance => distance > 0).Min()).Min();
    }

    private static List<ElevationGrid.Tile> GetFewestStepsToBestSignalWithDijkstra(ElevationGrid.Tile source, ElevationGrid.Tile? target)
    {
        var dist = new Dictionary<ElevationGrid.Tile, int>();
        var prev = new Dictionary<ElevationGrid.Tile, ElevationGrid.Tile>();
        var contents = new HashSet<ElevationGrid.Tile>();
        dist[source] = 0;
        var queue = new PriorityQueue<ElevationGrid.Tile, int>();
        queue.Enqueue(source, dist[source]);
        contents.Add(source);
        while (queue.Count > 0)
        {
            var tile = queue.Dequeue();
            contents.Remove(tile);
            if (tile == target)
                break;
            foreach (var neighbouringTile in tile.Neighbours)
            {
                var alt = dist[tile] + 1;
                if (alt >= (dist.ContainsKey(neighbouringTile) ? dist[neighbouringTile] : int.MaxValue))
                {
                    continue;
                }
                dist[neighbouringTile] = alt;
                prev[neighbouringTile] = tile;
                if (contents.Contains(neighbouringTile))
                {
                    continue;
                }
                queue.Enqueue(neighbouringTile, alt);
                contents.Add(neighbouringTile);
            }
        }

        var path = new List<ElevationGrid.Tile>();
        if (target is not null && !prev.ContainsKey(target) && target != source)
        {
            return path;
        }
        
        while (target is not null)
        {
            path.Add(target);
            target = prev.ContainsKey(target) ? prev[target] : null;
        }

        return path;
    }

    public class ElevationGrid
    {
        private const bool DebugToConsole = false;
        
        public Tile[][] GridData;
        private (int x, int y) GridSize { get; set; }
        public Tile Start { get; private set; }
        public Tile Target { get; private set; }

        public void Initialize(string[] input)
        {
            GridSize = (input[0].Length, input.Length);
            GridData = new Tile[GridSize.y][];
            for (var y = 0; y < GridSize.y; y++)
            {
                GridData[y] = new Tile[GridSize.x];
                for (var x = 0; x < GridSize.x; x++)
                {
                    var newTile = new Tile(x, y, input[y][x]);
                    if (DebugToConsole)
                    {
                        Console.WriteLine($"x:{x},y:{y}");
                    }
                    GridData[y][x] = newTile;
                    switch (newTile.Elevation)
                    {
                        case 'S':
                            newTile.Elevation = 'a';
                            Start = newTile;
                            break;
                        case 'E':
                            newTile.Elevation = 'z';
                            Target = newTile;
                            break;
                    }
                }
            }

            foreach (var row in GridData)
            {
                foreach (var tile in row)
                {
                    FindNeighbours(tile);
                }
            }
        }
        
        private void FindNeighbours(Tile tile)
        {
            tile.Neighbours
                = (from Direction dir in Enum.GetValues(typeof(Direction))
                    select GetNeighbourInDir(tile, dir) into neighbourInDir
                    where neighbourInDir is not null select neighbourInDir)
                .ToArray();
        }

        private Tile? GetNeighbourInDir(Tile tile, Direction dir)
        {
            var (offsetX, offsetY) = DirToOffset(dir);
            (int newX, int newY) coords = (tile.X + offsetX, tile.Y + offsetY);
            var neighbouringTile = GetTile(coords.newX, coords.newY);
            if (neighbouringTile is null) return null;
            return neighbouringTile.Elevation <= tile.Elevation + 1 ? neighbouringTile : null;
        }

        private Tile? GetTile(int x, int y)
        {
            if (x >= 0 && x < GridSize.x && y >= 0 && y < GridSize.y)
            {
                return GridData[y][x];
            }

            if (DebugToConsole)
            {
                Console.WriteLine($"Coordinates x,y are outside the grid: x:{x},y:{y}");
            }
            return null;
        }

        public class Tile
        {
            public readonly int X, Y;
            public char Elevation;
            public Tile?[] Neighbours;

            public Tile(int x, int y, char elevation)
            {
                X = x;
                Y = y;
                Neighbours = Array.Empty<Tile>();
                Elevation = elevation;
            }

            public override int GetHashCode()
            {
                return X * 1000 + Y;
            }
        }
    }

    private static (int x, int y) DirToOffset(Direction dir)
    {
        return dir switch
        {
            Direction.N => (0, -1),
            Direction.E => (1, 0),
            Direction.S => (0, 1),
            Direction.W => (-1, 0),
            _ => throw new ArgumentException($"Unknown Direction: {dir}")
        };
    }

    private enum Direction
    {
        N,
        E,
        S,
        W
    }
}