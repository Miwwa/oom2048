using Xunit;

namespace oom2048.Tests;

public class IndexedSpanTest
{
    [Fact]
    public void NormalOrderIndexing()
    {
        uint[] original = [1, 10, 20, 30, 40, 50, 60, 70];
        int[] indexes = [0, 1, 2, 3, 4, 5, 6, 7];
        IndexedSpan row = new IndexedSpan(original, indexes);
        Assert.True(row[0] == 1);
        Assert.True(row[1] == 10);
        Assert.True(row[2] == 20);
        Assert.True(row[3] == 30);
        Assert.True(row[4] == 40);
        Assert.True(row[5] == 50);
        Assert.True(row[6] == 60);
        Assert.True(row[7] == 70);

        row[0] = 100;
        Assert.True(original[0] == 100);
    }

    [Fact]
    public void ReverseOrderIndexing()
    {
        uint[] original = [1, 10, 20, 30, 40, 50, 60, 70];
        int[] indexes = [7, 6, 5, 4, 3, 2, 1, 0];
        IndexedSpan row = new IndexedSpan(original, indexes);
        Assert.True(row[0] == 70);
        Assert.True(row[1] == 60);
        Assert.True(row[2] == 50);
        Assert.True(row[3] == 40);
        Assert.True(row[4] == 30);
        Assert.True(row[5] == 20);
        Assert.True(row[6] == 10);
        Assert.True(row[7] == 1);

        row[0] = 100;
        Assert.True(original[7] == 100);
    }

    [Fact]
    public void RandomOrderIndexing()
    {
        uint[] original = [1, 10, 20, 30, 40, 50, 60, 70];
        int[] indexes = [3, 2, 1, 0, 7, 6, 5, 4];
        IndexedSpan row = new IndexedSpan(original, indexes);
        Assert.True(row[0] == 30);
        Assert.True(row[1] == 20);
        Assert.True(row[2] == 10);
        Assert.True(row[3] == 1);
        Assert.True(row[4] == 70);
        Assert.True(row[5] == 60);
        Assert.True(row[6] == 50);
        Assert.True(row[7] == 40);

        row[0] = 100;
        Assert.True(original[3] == 100);
    }
}

