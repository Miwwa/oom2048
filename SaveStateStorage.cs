using System.IO;

namespace oom2048;

/// <summary>
/// Saving player data is a complex problem by itself and out of scope of this project.
/// This is simplified version saving the best score in a file alongside with application executable file. This approach has many issues related to OS-specific features.
/// Serialization, versioning, errors checking also not covered in this solution.
/// </summary>
public static class SaveStateStorage
{
    private const string BestScoreFileName = "best_score.txt";

    public static void SaveBestScore(uint score)
    {
        File.WriteAllText(BestScoreFileName, score.ToString());
    }

    public static uint LoadBestScore()
    {
        if (!File.Exists(BestScoreFileName))
        {
            return 0;
        }
        var str = File.ReadAllText(BestScoreFileName);
        return uint.Parse(str);
    }
}
