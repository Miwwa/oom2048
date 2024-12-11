namespace oom2048;

/// <summary>
/// Use indexes array to change iteration order over original array
/// </summary>
/// <param name="cells">Original array</param>
/// <param name="indexes">New iteration order</param>
public readonly ref struct IndexedSpan(uint[] cells, int[] indexes)
{
    public int Length => indexes.Length;

    public uint this[int index]
    {
        get => cells[indexes[index]];
        set => cells[indexes[index]] = value;
    }

    public MoveResult ShiftRightAndMergeRow()
    {
        bool hasMoved = false;
        uint score = 0;

        // merge cells with same value from right to left
        int lastNotZero = Length - 1;
        for (int i = Length - 2; i >= 0; i--)
        {
            if (this[i] == 0)
            {
                continue;
            }

            if (this[i] == this[lastNotZero])
            {
                this[lastNotZero] *= 2;
                this[i] = 0;
                score += this[lastNotZero];
                hasMoved = true;
            }

            lastNotZero = i;
        }

        // move non-zero cells to the right
        int lastZero = Length - 1;
        for (int i = Length - 1; i >= 0; i--)
        {
            if (this[i] == 0)
            {
                continue;
            }

            if (i != lastZero)
            {
                this[lastZero] = this[i];
                this[i] = 0;
                hasMoved = true;
            }

            lastZero--;
        }

        return new MoveResult(hasMoved, score);
    }
}
