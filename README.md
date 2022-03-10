# Basic Unique Sudoku Generator and Solver #

## Generate a Sudoku puzzle
Generage a Sudoku puzzle, each puzzle will be unique e.g,
```
var sudoku = new Sudoku();
var puzzle = sudoku.Generate();
sudoku.Print(puzzle);
>>> 000060309000000000080401000200000000076800200000045070000752900002908610900000000
```
## Solve a Sudoku puzzle
Solve a Sudoku puzzle using Brute Force with Backtracking algorithm.

```
var sudoku = new Sudoku();
var puzzle = sudoku.Generate();
sudoku.Solve(puzzle);
sudoku.Print(puzzle);
>>> 421567389567389421389421567245176893176893245893245176614752938752938614938614752
```

### License
MIT
