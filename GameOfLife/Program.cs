using System;
using System.Linq;
using System.Text;

namespace TestTask
{
    public static class Consts
    {
        public static char LiveChar = '0';
        public static char DeadChar = '.';
    }
    public static class GameOfLife
    {
        public static void Main()
        {
            Console.WriteLine("Game of Life — press Enter to start...");
            Console.ReadLine();

            const int size = 25;
            var grid = SeedGlider(CreateEmptyGrid(size));

            for (int gen = 1; gen <= 200; gen++)
            {
                Console.Clear();
                Console.WriteLine($"Generation: {gen}\n");
                Console.WriteLine(GridToString(grid));
                Console.WriteLine("\nPress Enter to continue...");
                grid = ComputeNextState(grid);
                Console.ReadLine();
            }
        }

        public static bool[,] CreateEmptyGrid(int size) => new bool[size, size];

        public static bool[,] SeedGlider(bool[,] grid)
        {
            var mid = grid.GetLength(0) / 2;

            var positions = new (int r, int c)[]
            {
                (mid, mid + 1),
                (mid + 1, mid + 2),
                (mid + 2, mid),
                (mid + 2, mid + 1),
                (mid + 2, mid + 2)
            };

            foreach (var pos in positions)
            {
                grid[pos.r, pos.c] = true;
            }

            return grid;
        }

        public static bool[,] ComputeNextState(bool[,] current)
        {
            var size = current.GetLength(0);
            var next = new bool[size, size];

            var directions = new (int dr, int dc)[]
            {
                (-1, -1), (-1, 0), (-1, 1), (0, -1),
                (0, 1), (1, -1), (1, 0), (1, 1)
            };

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    var liveNeighbors = directions.Count(dir =>
                    {
                        var (r, c) = (row + dir.dr, col + dir.dc);
                        return r >= 0 && c >= 0 && r < size && c < size && current[r, c];
                    });

                    next[row, col] = current[row, col]
                        ? liveNeighbors == 2 || liveNeighbors == 3
                        : liveNeighbors == 3;
                }
            }

            return next;
        }

        public static string GridToString(bool[,] grid)
        {
            var size = grid.GetLength(0);

            return string.Join("\n", Enumerable.Range(0, size)
                .Select(row => new string(
                    Enumerable.Range(0, size)
                        .Select(col => grid[row, col] ? Consts.LiveChar : Consts.DeadChar)
                        .ToArray()
                )));
        }
    }
}