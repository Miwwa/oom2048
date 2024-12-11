using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace oom2048;

public class Board
{
    // board size is defined by the game rules, no need to make it configurable
    private const int Size = 4;
    private const uint BaseValue = 2;
    private const uint MaxValue = 2048;

    // move tiles operation is implemented only for one direction - from left to right
    // move tiles in other direction is equivalent to iteration over the array in the different order
    // this is orders of iterations for each direction
    private static readonly int[] RowIndexes = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
    private static readonly int[] ColumnIndexes = [0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15];
    private static readonly int[] RowIndexesReversed = RowIndexes.Reverse().ToArray();
    private static readonly int[] ColumnIndexesReversed = ColumnIndexes.Reverse().ToArray();

    // private static readonly int[] RowIndexes = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
    // private static readonly int[] ColumnIndexes = [0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15];
    // private static readonly int[] RowIndexesReversed = RowIndexes.Reverse().ToArray();
    // private static readonly int[] ColumnIndexesReversed = ColumnIndexes.Reverse().ToArray();

    // store 2D board as a 1D array
    private readonly uint[] _cells;
    public uint[] Cells => _cells;

    public Board()
    {
        _cells = new uint[Size * Size];
        Reset();
    }

    public Board(uint[] cells)
    {
        _cells = cells;
    }

    public void Reset()
    {
        Array.Fill<uint>(_cells, 0);
        // add two random tiles at start of the game
        TryAddRandomTile();
        TryAddRandomTile();
    }

    public bool TryAddRandomTile()
    {
        List<int> emptyCells = new();
        for (int i = 0; i < _cells.Length; i++)
        {
            if (_cells[i] == 0)
            {
                emptyCells.Add(i);
            }
        }

        if (emptyCells.Count == 0)
        {
            return false;
        }

        int randomIndex = Random.Shared.Next(0, emptyCells.Count);
        int randomCell = emptyCells[randomIndex];
        _cells[randomCell] = BaseValue;
        return true;
    }

    public bool HasMaxValue()
    {
        return Array.Exists(_cells, x => x == MaxValue);
    }

    public bool CanMakeMove()
    {
        bool hasEmptyCell = Array.Exists(_cells, x => x == 0);
        if (hasEmptyCell)
        {
            return true;
        }

        for (int i = 0; i < Size; i++)
        {
            int[] rowIndexes = new int[Size];
            int[] columnIndexes = new int[Size];
            for (int j = 0; j < Size; j++)
            {
                int index = i * Size + j;
                rowIndexes[j] = RowIndexes[index];
                columnIndexes[j] = ColumnIndexes[index];
            }

            var row = new IndexedSpan(_cells, rowIndexes);
            var column = new IndexedSpan(_cells, columnIndexes);

            for (int j = 0; j < Size - 2; j++)
            {
                // if two adjacent cells have the same non-zero value we can merge them and continue the game
                // this function is called only after shifting elements, so we don't worry about empty cells in between non-zero values
                if ((row[j] != 0 && row[j] == row[j + 1]) || (column[j] != 0 && column[j] == column[j + 1]))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public MoveResult MoveRight()
    {
        return ShiftRightAndMergeBoard(RowIndexes);
    }

    public MoveResult MoveLeft()
    {
        return ShiftRightAndMergeBoard(RowIndexesReversed);
    }

    public MoveResult MoveUp()
    {
        return ShiftRightAndMergeBoard(ColumnIndexesReversed);
    }

    public MoveResult MoveDown()
    {
        return ShiftRightAndMergeBoard(ColumnIndexes);
    }

    /// <summary>
    /// Shift and merge elements in place
    /// </summary>
    /// <param name="directionIndex"></param>
    /// <returns>Move summary data</returns>
    private MoveResult ShiftRightAndMergeBoard(int[] directionIndex)
    {
        bool hasMoved = false;
        uint score = 0;

        for (int i = 0; i < Size; i++)
        {
            // for each row create an IndexedSpan with shifted indexes
            int[] rowIndexes = new int[Size];
            for (int j = 0; j < Size; j++)
            {
                rowIndexes[j] = directionIndex[i * Size + j];
            }

            var row = new IndexedSpan(_cells, rowIndexes);
            // shift and merge each row and combine results
            var result = row.ShiftRightAndMergeRow();
            hasMoved |= result.HasMoved;
            score += result.Score;
        }

        return new MoveResult(hasMoved, score);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < Size; i++)
        {
            sb.Append("|----|----|----|----|\n");
            for (int j = 0; j < Size; j++)
            {
                uint value = _cells[i * Size + j];
                sb.Append("|");
                sb.Append(value.ToString().PadLeft(4));
            }

            sb.Append("|\n");
        }

        sb.Append("|----|----|----|----|\n");
        return sb.ToString();
    }

    public void PrintToConsole()
    {
        for (int i = 0; i < Size; i++)
        {
            Console.Write("|----|----|----|----|\n");
            for (int j = 0; j < Size; j++)
            {
                uint value = _cells[i * Size + j];
                Console.Write("|");
                Console.ForegroundColor = GetColorForValue(value);
                Console.Write(value.ToString().PadLeft(4));
                Console.ResetColor();
            }

            Console.Write("|\n");
        }

        Console.Write("|----|----|----|----|\n");
    }

    private static ConsoleColor GetColorForValue(uint value)
    {
        return value switch
        {
            2 => ConsoleColor.Cyan,
            4 => ConsoleColor.Green,
            8 => ConsoleColor.Yellow,
            16 => ConsoleColor.Magenta,
            32 => ConsoleColor.Blue,
            64 => ConsoleColor.DarkRed,
            128 => ConsoleColor.DarkYellow,
            256 => ConsoleColor.DarkGreen,
            512 => ConsoleColor.DarkCyan,
            1024 => ConsoleColor.DarkMagenta,
            2048 => ConsoleColor.Red,
            _ => ConsoleColor.Gray,
        };
    }
}

public record struct MoveResult(bool HasMoved, uint Score)
{
    public static MoveResult None => new(false, 0);
}
