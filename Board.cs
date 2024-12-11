using System;
using System.Collections.Generic;
using System.Linq;

namespace oom2048;

public class Board
{
    // board size is defined by the game rules, no need to make it configurable
    private const int Size = 4;
    private const uint BaseValue = 2;

    // move tiles operation is implemented only for one direction - from left to right
    // move tiles in other direction is equivalent to iteration over the array in the different order
    // this is orders of iterations for each direction
    private static readonly int[] IndexesToShiftRight = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
    private static readonly int[] IndexesToShiftDown = [0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15];
    private static readonly int[] IndexesToShiftLeft = IndexesToShiftRight.Reverse().ToArray();
    private static readonly int[] IndexesToShiftUp = IndexesToShiftDown.Reverse().ToArray();

    // store 2D board as a 1D array
    private readonly uint[] _cells;
    public uint[] Cells => _cells;

    public Board()
    {
        _cells = new uint[Size * Size];
        Reset();
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

    public MoveResult MoveRight()
    {
        return ShiftRightAndMergeBoard(IndexesToShiftRight);
    }

    public MoveResult MoveLeft()
    {
        return ShiftRightAndMergeBoard(IndexesToShiftLeft);
    }

    public MoveResult MoveUp()
    {
        return ShiftRightAndMergeBoard(IndexesToShiftUp);
    }

    public MoveResult MoveDown()
    {
        return ShiftRightAndMergeBoard(IndexesToShiftDown);
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
        return Printer.ArrayToMatrixString(_cells, Size);
    }
}

public record struct MoveResult(bool HasMoved, uint Score)
{
    public static MoveResult None => new(false, 0);
}
