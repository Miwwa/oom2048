namespace oom2048;

/// <summary>
/// Use to change iteration order
/// </summary>
/// <param name="cells">Original array</param>
/// <param name="indexes">new iteration order</param>
/// <typeparam name="T"></typeparam>
public readonly ref struct BoardRef<T>(T[] cells, int[] indexes)
{
    public T this[int index]
    {
        get => cells[indexes[index]];
        set => cells[indexes[index]] = value;
    }
}
