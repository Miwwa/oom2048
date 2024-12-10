using System;
using System.Collections.Generic;

namespace oom2048;

public class Board
{
    // board size is defined by the game rules, no need to make it configurable
    private const int BoardSize = 4;
    private const uint BaseValue = 2;

    // store 2D board as a 1D array
    private readonly uint[] _cells;

    public Board()
    {
        _cells = new uint[BoardSize * BoardSize];
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

        int randomIndex = new Random().Next(0, emptyCells.Count);
        int randomCell = emptyCells[randomIndex];
        _cells[randomCell] = BaseValue;
        Console.WriteLine($"Added random tile at {randomCell}");
        return true;
    }

    public MoveResult MoveRight()
    {
        throw new NotImplementedException();
    }

    public MoveResult MoveLeft()
    {
        throw new NotImplementedException();
    }

    public MoveResult MoveUp()
    {
        throw new NotImplementedException();
    }

    public MoveResult MoveDown()
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return _cells.ToMatrixString();
    }
}

public record struct MoveResult(bool HasMoved, uint Score)
{
    public static MoveResult None => new(false, 0);
}
