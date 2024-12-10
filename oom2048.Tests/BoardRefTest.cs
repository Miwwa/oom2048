using Xunit;

namespace oom2048.Tests;

public class BoardRefTest
{
    [Fact]
    public void NormalOrder()
    {
        uint[] board = [1, 10, 20, 30, 40, 50, 60, 70];
        int[] indexes = [0, 1, 2, 3, 4, 5, 6, 7];
        BoardRef<uint> boardRef = new BoardRef<uint>(board, indexes);
        Assert.True(boardRef[0] == 1);
        Assert.True(boardRef[1] == 10);
        Assert.True(boardRef[2] == 20);
        Assert.True(boardRef[3] == 30);
        Assert.True(boardRef[4] == 40);
        Assert.True(boardRef[5] == 50);
        Assert.True(boardRef[6] == 60);
        Assert.True(boardRef[7] == 70);

        boardRef[0] = 100;
        Assert.True(board[0] == 100);
    }

    [Fact]
    public void ReverseOrder()
    {
        uint[] board = [1, 10, 20, 30, 40, 50, 60, 70];
        int[] indexes = [7, 6, 5, 4, 3, 2, 1, 0];
        BoardRef<uint> boardRef = new BoardRef<uint>(board, indexes);
        Assert.True(boardRef[0] == 70);
        Assert.True(boardRef[1] == 60);
        Assert.True(boardRef[2] == 50);
        Assert.True(boardRef[3] == 40);
        Assert.True(boardRef[4] == 30);
        Assert.True(boardRef[5] == 20);
        Assert.True(boardRef[6] == 10);
        Assert.True(boardRef[7] == 1);

        boardRef[0] = 100;
        Assert.True(board[7] == 100);
    }

    [Fact]
    public void RandomOrder()
    {
        uint[] board = [1, 10, 20, 30, 40, 50, 60, 70];
        int[] indexes = [3, 2, 1, 0, 7, 6, 5, 4];
        BoardRef<uint> boardRef = new BoardRef<uint>(board, indexes);
        Assert.True(boardRef[0] == 30);
        Assert.True(boardRef[1] == 20);
        Assert.True(boardRef[2] == 10);
        Assert.True(boardRef[3] == 1);
        Assert.True(boardRef[4] == 70);
        Assert.True(boardRef[5] == 60);
        Assert.True(boardRef[6] == 50);
        Assert.True(boardRef[7] == 40);

        boardRef[0] = 100;
        Assert.True(board[3] == 100);
    }
}
