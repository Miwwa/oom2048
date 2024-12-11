using System.Text;

namespace oom2048;

public static class Printer
{
    public static string ArrayToMatrixString(uint[] cells, int size = 4, bool colorize = false)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < size; i++)
        {
            sb.Append("|----|----|----|----|\n");
            for (int j = 0; j < size; j++)
            {
                uint value = cells[i * size + j];
                sb.Append("|");
                sb.Append(value.ToString().PadLeft(4));
            }

            sb.Append("|\n");
        }

        sb.Append("|----|----|----|----|\n");
        return sb.ToString();
    }
}
