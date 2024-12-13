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
}
