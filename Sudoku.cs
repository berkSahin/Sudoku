using System;
using System.Linq;

namespace SudokuLibrary
{
    public class Sudoku
    {
        #region Fields

        private Random _rand = new Random();

        #endregion Fields

        #region Public Methods

        public int[,] Generate()
        {
            var grid = new int[9, 9];

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    grid[i, j] = 0;

            Solve(grid);
            MakeUnique(grid);

            return grid;
        }

        public void Solve(int[,] grid)
        {
            var guessArray = Enumerable.Range(1, 9).OrderBy(o => _rand.Next()).ToArray();
            BactTracking(grid, guessArray);
        }

        public void Print(int[,] grid)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    Console.Write(grid[i, j]);
        }

        #endregion Public Methods

        #region Private Methods

        #region BackTrack

        private bool BactTracking(int[,] grid, int[] guessArray)
        {
            int row = 0, col = 0;

            if (!FindEmptyLocation(grid, ref row, ref col))
                return true;

            for (int num = 0; num < 9; num++)
            {
                if (IsSafe(grid, row, col, guessArray[num]))
                {
                    grid[row, col] = guessArray[num];

                    if (BactTracking(grid, guessArray))
                        return true;

                    grid[row, col] = 0;
                }
            }

            return false;
        }

        private bool FindEmptyLocation(int[,] grid, ref int row, ref int col)
        {
            for (row = 0; row < 9; row++)
                for (col = 0; col < 9; col++)
                    if (grid[row, col] == 0)
                        return true;
            return false;
        }

        private bool UsedInRow(int[,] grid, int row, int value)
        {
            for (int i = 0; i < 9; i++)
                if (grid[row, i] == value)
                    return true;
            return false;
        }

        private bool UsedInCol(int[,] grid, int col, int value)
        {
            for (int i = 0; i < 9; i++)
                if (grid[i, col] == value)
                    return true;
            return false;
        }

        private bool UsedInBox(int[,] grid, int boxStartRow, int boxStartCol, int value)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (grid[i + boxStartRow, j + boxStartCol] == value)
                        return true;
            return false;
        }

        private bool IsSafe(int[,] grid, int row, int col, int value)
        {
            return !UsedInRow(grid, row, value) && !UsedInCol(grid, col, value) && !UsedInBox(grid, row - row % 3, col - col % 3, value);
        }

        #endregion BackTrack

        #region Unique

        private void MakeUnique(int[,] grid)
        {
            var randomIndexes = Enumerable.Range(0, 81).OrderBy(o => _rand.Next()).ToArray();
            var guessArray = Enumerable.Range(1, 9).OrderBy(o => _rand.Next()).ToArray();

            for (int i = 0; i < 81; i++)
            {
                int x = randomIndexes[i] / 9;
                int y = randomIndexes[i] % 9;
                int temp = grid[x, y];
                grid[x, y] = 0;

                int check = 0;
                CheckUniqueness(grid, guessArray, ref check);
                if (check != 1)
                {
                    grid[x, y] = temp;
                }
            }
        }

        private void CheckUniqueness(int[,] grid, int[] guessArray, ref int number)
        {
            int row = 0, col = 0;

            if (!FindEmptyLocation(grid, ref row, ref col))
            {
                number++;
                return;
            }

            for (int i = 0; i < 9 && number < 2; i++)
            {
                if (IsSafe(grid, row, col, guessArray[i]))
                {
                    grid[row, col] = guessArray[i];
                    CheckUniqueness(grid, guessArray, ref number);
                }

                grid[row, col] = 0;
            }
        }

        #endregion Unique

        #endregion Private Methods
    }
}