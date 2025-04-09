using TestTask;

namespace GameOfLive.Tests
{
    [TestFixture]
    public class GameOfLifeTests
    {
        [Test]
        public void CreateEmptyGrid_ShouldReturnEmptyGrid()
        {
            var grid = GameOfLife.CreateEmptyGrid(5);
            Assert.AreEqual(5, grid.GetLength(0));
            Assert.AreEqual(5, grid.GetLength(1));
            foreach (var cell in grid)
                Assert.IsFalse(cell);
        }

        [Test]
        public void SeedGlider_ShouldSeedCorrectPattern()
        {
            var grid = GameOfLife.CreateEmptyGrid(5);
            GameOfLife.SeedGlider(grid);

            Assert.IsTrue(grid[2, 3]);
            Assert.IsTrue(grid[3, 4]);
            Assert.IsTrue(grid[4, 2]);
            Assert.IsTrue(grid[4, 3]);
            Assert.IsTrue(grid[4, 4]);
        }

        [Test]
        public void ComputeNextState_BlockPattern_ShouldRemainStable()
        {
            var grid = new bool[,]
            {
                { true, true },
                { true, true }
            };

            var next = GameOfLife.ComputeNextState(grid);

            Assert.AreEqual(grid, next);
        }

        [Test]
        public void ComputeNextState_DeadGrid_ShouldRemainDead()
        {
            var grid = GameOfLife.CreateEmptyGrid(3);
            var next = GameOfLife.ComputeNextState(grid);

            foreach (var cell in next)
                Assert.IsFalse(cell);
        }

        [Test]
        public void GridToString_ShouldReturnCorrectRepresentation()
        {
            var grid = new bool[,]
            {
                { true, false },
                { false, true }
            };

            var result = GameOfLife.GridToString(grid);

            var expected = "O.\n.O";

            Assert.AreEqual(expected, result);
        }
    }
}
