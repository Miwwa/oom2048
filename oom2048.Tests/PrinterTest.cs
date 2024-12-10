using Xunit;

namespace oom2048.Tests;

public class PrinterTests
{
    [Fact]
    public void ToMatrixString_ShouldReturnCorrectString_For4x4Array()
    {
        uint[] cells = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16];

        string expected = "|----|----|----|----|\n" +
                          "|   1|   2|   3|   4|\n" +
                          "|----|----|----|----|\n" +
                          "|   5|   6|   7|   8|\n" +
                          "|----|----|----|----|\n" +
                          "|   9|  10|  11|  12|\n" +
                          "|----|----|----|----|\n" +
                          "|  13|  14|  15|  16|\n" +
                          "|----|----|----|----|\n";

        string result = Printer.ArrayToMatrixString(cells);

        Assert.Equal(expected, result);
    }
}
