using Xunit;

namespace oom2048.Tests;

public class BoardTest
{
    [Theory]
    [InlineData(new uint[] { 0, 2, 4, 8, 2048, 1024, 512, 256, 128, 64, 32, 16, 8, 4, 2, 1 }, true)]
    [InlineData(new uint[] { 0, 2, 4, 8, 1024, 1024, 512, 256, 128, 64, 32, 16, 8, 4, 2, 1 }, false)]
    void BoardState_ShouldWin(uint[] cells, bool expected)
    {
        var board = new Board(cells);
        bool isWin = board.HasMaxValue();
        Assert.Equal(expected, isWin);
    }

    [Theory]
    [InlineData(new uint[]
    {
        0, 2, 4, 8,
        1024, 1024, 512, 256,
        128, 64, 32, 16,
        8, 4, 2, 1
    }, true)]
    [InlineData(new uint[]
    {
        2, 2, 2, 2,
        2, 2, 2, 2,
        2, 2, 2, 2,
        2, 2, 2, 2
    }, true)]
    [InlineData(new uint[]
    {
        2, 4, 2, 4,
        4, 2, 4, 2,
        2, 4, 2, 4,
        4, 2, 4, 2
    }, false)]
    [InlineData(new uint[]
    {
        2, 4, 8, 2,
        4, 8, 2, 4,
        8, 16, 4, 8,
        16, 32, 8, 64
    }, false)]
    void BoardState_CanMakeMove(uint[] cells, bool expected)
    {
        var board = new Board(cells);
        bool canMakeMove = board.CanMakeMove();
        Assert.Equal(expected, canMakeMove);
    }

    [Fact]
    void BoardMove_ShouldMoveRight()
    {
        var board = new Board([
            2, 0, 0, 0,
            0, 2, 0, 0,
            0, 0, 2, 0,
            0, 0, 0, 2
        ]);
        uint[] expected =
        [
            0, 0, 0, 2,
            0, 0, 0, 2,
            0, 0, 0, 2,
            0, 0, 0, 2,
        ];

        board.MoveRight();
        Assert.Equal(expected, board.Cells);
    }

    [Fact]
    void BoardMove_ShouldMoveLeft()
    {
        var board = new Board([
            2, 0, 0, 0,
            0, 2, 0, 0,
            0, 0, 2, 0,
            0, 0, 0, 2
        ]);
        uint[] expected =
        [
            2, 0, 0, 0,
            2, 0, 0, 0,
            2, 0, 0, 0,
            2, 0, 0, 0,
        ];

        board.MoveLeft();
        Assert.Equal(expected, board.Cells);
    }

    [Fact]
    void BoardMove_ShouldMoveUp()
    {
        var board = new Board([
            2, 0, 0, 0,
            0, 2, 0, 0,
            0, 0, 2, 0,
            0, 0, 0, 2
        ]);
        uint[] expected =
        [
            2, 2, 2, 2,
            0, 0, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 0,
        ];

        board.MoveUp();
        Assert.Equal(expected, board.Cells);
    }

    [Fact]
    void BoardMove_ShouldMoveDown()
    {
        var board = new Board([
            2, 0, 0, 0,
            0, 2, 0, 0,
            0, 0, 2, 0,
            0, 0, 0, 2
        ]);
        uint[] expected =
        [
            0, 0, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 0,
            2, 2, 2, 2,
        ];

        board.MoveDown();
        Assert.Equal(expected, board.Cells);
    }

    [Fact]
    public void BoardState_ShouldConvertToFormattedString()
    {
        var board = new Board([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16]);
        string expected = "|----|----|----|----|\n" +
                          "|   1|   2|   3|   4|\n" +
                          "|----|----|----|----|\n" +
                          "|   5|   6|   7|   8|\n" +
                          "|----|----|----|----|\n" +
                          "|   9|  10|  11|  12|\n" +
                          "|----|----|----|----|\n" +
                          "|  13|  14|  15|  16|\n" +
                          "|----|----|----|----|\n";

        string result = board.ToString();
        Assert.Equal(expected, result);
    }
}
