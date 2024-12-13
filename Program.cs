using System;
using oom2048;

var game = new Game(SaveStateStorage.LoadBestScore());
game.OnBestScoreChanged += SaveStateStorage.SaveBestScore;

try
{
    game.Run();
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Unhandled exception:\n{ex}");
    Console.Error.WriteLine("Press any key to exit...");
    Console.ReadKey(true);
    Environment.Exit(1);
}
